using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;
using Car_Rego_Assignment.Properties;

namespace Car_Rego_Assignment
{
    public partial class Form1 : Form
    {
        public SQLiteConnection connection;
        public bool theme = true;

        public Form1()
        {
            string directory = AppContext.BaseDirectory + @"\cars4.db";
            //Resources.cars4;
            byte[] buf = Resources.cars4;
            //sfile.Read(buf, 0, Convert.ToInt32(sfile.Length));

            if (!File.Exists(directory))
            {
                //File.Delete(@"\cars4.db");

                using (FileStream fs = File.Create("cars4.db"))
                {
                    fs.Write(buf, 0, Convert.ToInt32(buf.Length));
                    fs.Close();
                }
            }

            connection = new SQLiteConnection(@"Data Source=" + directory);
            connection.Open();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            homePage.show(this);
            inputScreen1.hide();
            inputScreen2.hide();
            resultsScreen.hide();
            historyPage.hide();

            homePage.resize(this);
            inputScreen1.resize(this);
            inputScreen2.resize(this);
            resultsScreen.resize(this);
            historyPage.resize(this);

            bool theme = this.theme;
            homePage.theme(this, theme);
            inputScreen1.theme(this, theme);
            inputScreen2.theme(this, theme);
            resultsScreen.theme(this, theme);
            historyPage.theme(this, theme);
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            this.historyPage.resizeEnd(this);
        }

        private void ToggleTheme(object sender, EventArgs e)
        {
            this.theme = !this.theme;

            homePage.theme(this, this.theme);
            inputScreen1.theme(this, this.theme);
            inputScreen2.theme(this, this.theme);
            resultsScreen.theme(this, this.theme);
            historyPage.theme(this, this.theme);
        }

        int activePage = 0;

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (activePage == 0) homePage.resize(this);
            if (activePage == 1) inputScreen1.resize(this);
            if (activePage == 2) inputScreen2.resize(this);
            if (activePage == 3) resultsScreen.resize(this);
            if (activePage == -1) historyPage.resize(this);
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            homePage.hide();
            inputScreen1.show(this);
            inputScreen1.resize(this);

            activePage = 1;
        }

        private void goHomeButton_Click(object sender, EventArgs e)
        {
            resultsScreen.hide();
            homePage.show(this);
            homePage.resize(this);

            activePage = 0;
        }

        private void backArrow_InputPage1_Click(object sender, EventArgs e)
        {
            homePage.show(this);
            inputScreen1.hide();
            homePage.resize(this);

            activePage = 0;
        }

        private void backArrow_HistoryPage_Click(object sender, EventArgs e)
        {
            homePage.show(this);
            historyPage.hide();
            homePage.resize(this);

            activePage = 0;
        }

        private void backArrow_InputPage2_Click(object sender, EventArgs e)
        {
            inputScreen1.show(this);
            inputScreen2.hide();

            inputScreen1.resize(this);
            activePage = 1;
        }

        private void backArrow_ResultsScreen_Click(object sender, EventArgs e)
        {
            inputScreen2.show(this);
            resultsScreen.hide();

            inputScreen2.resize(this);

            activePage = 2;
        }

        private void nextArrow_Click(object sender, EventArgs e)
        {
            ErrorBox box = new ErrorBox(this.theme);
            List<String> errors = new List<String>();

            List<String> input = new List<String>();

            List<String> missing = new List<String>();

            if (Regex.Replace(this.inputScreen1.priceInput.inputText.Text, "([^0-9])", "") == "")
            {
                input.Add("wholesale price");
            }
            else
            {
                decimal? price = this.inputScreen1.parsePrice();

                if (!price.HasValue)
                {
                    input.Add("wholesale price");
                }
                else if (price.Value < 50)
                {
                    errors.Add("Wholesale Price is too small");
                }
            }

            if (!this.inputScreen1.makeGrid.selected.HasValue)
                missing.Add("make");

            if(!this.inputScreen1.makeGrid.selected.HasValue)
                missing.Add("body type");

            if (!this.inputScreen1.personal.HasValue)
                missing.Add("use");

            if (Regex.Replace(this.inputScreen1.nameInput.inputText.Text, "([^a-zA-Z])", "") == "")
                input.Add("owner's name");

            if (errors.Any() || missing.Any())
            {
                box.setText(input, missing, errors);
                box.ShowDialog();
            }
            else
            {
                this.inputScreen1.hide();
                this.inputScreen2.show(this);

                inputScreen2.resize(this);
                activePage = 2;
            }
        }

        private void calculateResults_Click(object sender, EventArgs e)
        {
            bool validCustom = (this.inputScreen2.modelBox.inputText.Text != "")
                    && Regex.Match(this.inputScreen2.yearBox.inputText.Text, "[0-9]{4}").Success
                    && Regex.Match(this.inputScreen2.weightBox.inputText.Text, "[0-9]{3}").Success;

            if (!(this.inputScreen2.container.selectionIndex.HasValue || validCustom))
            {
                ErrorBox box = new ErrorBox(this.theme);
                List<String> errors = new List<String>();
                errors.Add("Please select a model,\nor enter a custom one");
                box.setText(new List<String>(), new List<String>(), errors);
                box.ShowDialog();
            }
            else
            {
                //parse price
                string priceText = this.inputScreen1.priceInput.inputText.Text;

                priceText = Regex.Replace(priceText, "([, $])", "");

                ErrorBox box = new ErrorBox(this.theme);
                List<String> errors = new List<String>();

                double price = 0;

                //test year
                try
                {
                    int year = Int32.Parse(this.inputScreen2.yearBox.inputText.Text);

                    if (year < 1700 || year > 2022)
                        errors.Add("Invalid year; out of range");
                }
                catch (Exception)
                {

                }
                

                double weight = this.inputScreen2.container.getSelectionWeight();

                bool fromDatabase = true;

                if(weight == -1)
                {
                    fromDatabase = false;

                    try
                    {
                        string weightText = this.inputScreen2.weightBox.inputText.Text;
                        weightText = Regex.Replace(weightText, "([ ,])", "");
                        weight = Double.Parse(weightText);
                    }
                    catch (Exception)
                    {
                        errors.Add("No selection found or\nFailed to parse weight");
                    }
                }
                    

                try
                {
                    price = Double.Parse(priceText);
                }
                catch (Exception)
                {
                    errors.Add("Failed to parse price");
                }

                if(!this.inputScreen1.personal.HasValue)
                    errors.Add("No Use selecteed");

                if (errors.Count == 0)
                {
                    activePage = 3;

                    resultsScreen.resize(this);
                    resultsScreen.show(this);
                    inputScreen2.hide();

                    double[] costs = genCosts(this.inputScreen1.personal.Value, weight, price);

                    resultsScreen.updateText(costs, this);

                    if (fromDatabase) writeHistory(costs[5]);
                }
                else
                {
                    box.setText(new List<String>(), new List<String>(), errors);
                    box.ShowDialog();
                }
            }
        }

        private void writeHistory(double payed)
        {
            SQLiteCommand comm = this.connection.CreateCommand();

            comm.CommandText =
@"INSERT INTO HistoryA (ModelID, RegDate, OwnerName, paidAmt) SELECT @model, @date, @owner, @paid WHERE NOT EXISTS (SELECT 1 FROM History WHERE (ModelID = @model and RegDate = @date and OwnerName = @owner))";

            comm.Parameters.AddWithValue("@model", this.inputScreen2.container.getSelectionID());
            comm.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
            comm.Parameters.AddWithValue("@owner", this.inputScreen1.nameInput.inputText.Text);
            comm.Parameters.AddWithValue("@paid", payed);

            comm.ExecuteNonQuery();

            // CREATE TABLE History (ModelID int, RegDate date, OwnerName text, paidAmt decimal(2, 0));

        }

        private void businessButton_Click(object sender, EventArgs e)
        {
            inputScreen1.selectUse(false);
        }

        private void personalButton_Click(object sender, EventArgs e)
        {
            inputScreen1.selectUse(true);
        }

        private void OpenHistoryPage(object sender, EventArgs e)
        {
            historyPage.show(this);
            homePage.hide();

            historyPage.resize(this);

            this.historyPage.resizeEnd(this);

            activePage = -1;
        }

        //Weight in kg
        private double[] genCosts(bool privUse, double weight, double price)
        {
            //Console.WriteLine("weight = " + weight);

            double tax = 0, duty = 0, premium = 0;
            if (weight < 975)
            {
                if (privUse) tax = 191;
                if (!privUse) tax = 308;
            }
            else if (weight < 1154)
            {
                if (privUse) tax = 220;
                if (!privUse) tax = 351;
            }
            else if (weight < 1505)
            {
                if (privUse) tax = 270;
                if (!privUse) tax = 425;
            }
            else
            {
                if (privUse) tax = 411;
                if (!privUse) tax = 425;
            }

            if (privUse) duty = 0.01d * price;
            if (!privUse) duty = 0.03d * price;

            if (privUse) premium = 0.02d * price;
            if (!privUse) premium = 0.05d * price;
            //As Vehicle tax, Registration fee, Stamp duty, Insurance premium, total registration, total payable
            return new double[]{tax, 60, duty, premium, 60 + duty + premium, tax + 60 + duty + premium};
        }
    }
}
