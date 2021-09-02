using Car_Rego_Assignment.Properties;
using System.Data.SQLite;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;
using System.Timers;

namespace Car_Rego_Assignment
{
    partial class Form1
    {
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /// 

        HomePage homePage;
        InputScreen1 inputScreen1;
        InputScreen2 inputScreen2;
        ResultsScreen resultsScreen;
        HistoryPage historyPage;
        Timer timer;
        private void InitializeComponent()
        {
            timer = new System.Timers.Timer(200);

            homePage = new HomePage(this);
            inputScreen1 = new InputScreen1(this);
            inputScreen2 = new InputScreen2(this);
            resultsScreen = new ResultsScreen(this);
            historyPage = new HistoryPage(this);

            this.SuspendLayout();

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1253, 632);

            this.Name = "CostCalc";
            this.Text = "Registration Cost Calculator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.SizeChanged += (sender, args) => {
                timer.Stop();
                timer.Start();
            };

            timer.Elapsed += (sender, args) => {
                timer.Stop();
                Invoke(new Action(() => { this.historyPage.resizeEnd(this); }));
            };

            //this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.resultsScreen.hideCaret);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private class HomePage
        {
            //public System.Windows.Forms.Button startButton;
            public System.Windows.Forms.Label title;
            public System.Windows.Forms.PictureBox startButton;
            public System.Windows.Forms.PictureBox themeButton;
            public System.Windows.Forms.PictureBox historyButton;
            private bool darkMode;

            public HomePage(Form1 form)
            {
                this.title = new System.Windows.Forms.Label();
                this.startButton = new System.Windows.Forms.PictureBox();
                this.themeButton = new System.Windows.Forms.PictureBox();
                this.historyButton = new System.Windows.Forms.PictureBox();

                init(form);
            }

            public void init(Form1 form)
            {
                //
                // Start Button
                //
                this.startButton.Cursor = System.Windows.Forms.Cursors.Hand;
                this.startButton.BackgroundImage = Resources.StartButton;
                this.startButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.startButton.Location = new System.Drawing.Point(391, 404);
                this.startButton.Name = "pictureBox1";
                this.startButton.Size = new System.Drawing.Size(252, 110);
                this.startButton.TabIndex = 2;
                this.startButton.TabStop = false;
                this.startButton.Click += new System.EventHandler(form.homeButton_Click);
                // 
                // title
                // 
                this.title.AutoSize = true;
                this.title.Font = new System.Drawing.Font("Segoe UI", 50F, FontStyle.Bold);
                this.title.Location = new System.Drawing.Point(100, 99);
                this.title.Name = "title";
                this.title.Size = new System.Drawing.Size(1054, 95);
                this.title.TabIndex = 1;
                this.title.Text = "Car Registration Calculator";
                //
                // Theme Button
                //
                this.themeButton.Cursor = System.Windows.Forms.Cursors.Hand;
                this.themeButton.BackgroundImage = Resources.ToggleTheme;
                this.themeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.themeButton.Size = new System.Drawing.Size(185, 40);
                this.themeButton.TabIndex = 2;
                this.themeButton.TabStop = false;
                this.themeButton.Click += new System.EventHandler(form.ToggleTheme);
                this.themeButton.Location = new System.Drawing.Point(10, 10);
                //
                // History Button
                //
                this.historyButton.Cursor = System.Windows.Forms.Cursors.Hand;
                this.historyButton.BackgroundImage = Resources.HistoryButton;
                this.historyButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.historyButton.Size = new System.Drawing.Size(100, 32);
                this.historyButton.Click += new System.EventHandler(form.OpenHistoryPage);

                form.Controls.Add(this.title);
                form.Controls.Add(this.startButton);
                form.Controls.Add(this.themeButton);
                form.Controls.Add(this.historyButton);
            }

            public void hide()
            {
                this.title.Visible = false;
                this.startButton.Visible = false;
                this.themeButton.Visible = false;
                this.historyButton.Visible = false;
            }

            public void show(Form1 form)
            {
                this.title.Visible = true;
                this.startButton.Visible = true;
                this.themeButton.Visible = true;
                this.historyButton.Visible = true;

                form.MinimumSize = new System.Drawing.Size(1100, 650);

                if (this.darkMode)
                {
                    form.BackColor = System.Drawing.ColorTranslator.FromHtml("#182039");
                }
                else
                {
                    form.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");
                }
            }

            public void resize(Form1 form)
            {
                this.title.Location = new System.Drawing.Point(form.Width/2 - 445, 120);
                this.startButton.Location = new System.Drawing.Point(form.Width / 2 - 100, form.Height/2);
                this.historyButton.Location = new System.Drawing.Point(form.Width - 110, 20);
            }

            public void theme(Form1 form, bool darkMode)
            {
                this.darkMode = darkMode;
                if (darkMode)
                {
                    this.title.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    form.BackColor = System.Drawing.ColorTranslator.FromHtml("#182039");
                }
                else
                {
                    this.title.ForeColor = System.Drawing.ColorTranslator.FromHtml("#707070");
                    form.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");
                }
            }
        }
        
        private class InputScreen1
        {
            public System.Windows.Forms.PictureBox backArrow;
            public System.Windows.Forms.PictureBox nextArrow;
            public System.Windows.Forms.Label heading;
            public System.Windows.Forms.Panel headingPanel;

            public System.Windows.Forms.Label ownerLabel;
            public CustomTextBox nameInput;

            public System.Windows.Forms.Label priceLabel;
            public CustomTextBox priceInput;

            public System.Windows.Forms.Label useLabel;
            public System.Windows.Forms.PictureBox personalButton;
            public System.Windows.Forms.PictureBox businessButton;

            public System.Windows.Forms.Label makeLabel;
            public CustomTextBox makeInput;
            public VariableButtonGrid makeGrid;

            public System.Windows.Forms.Label bodyLabel;
            public VariableButtonGrid bodyGrid;

            private bool darkMode;
            public bool? personal = null;

            public InputScreen1(Form1 form)
            {
                this.backArrow = new System.Windows.Forms.PictureBox();
                this.nextArrow = new System.Windows.Forms.PictureBox();
                this.heading = new System.Windows.Forms.Label();
                this.headingPanel = new System.Windows.Forms.Panel();

                this.ownerLabel = new System.Windows.Forms.Label();
                this.priceLabel = new System.Windows.Forms.Label();
                this.useLabel = new System.Windows.Forms.Label();
                this.makeLabel = new System.Windows.Forms.Label();
                this.bodyLabel = new System.Windows.Forms.Label();


                this.makeGrid = new VariableButtonGrid(2, 2, form, "RectangleInput", 185, new System.Drawing.Size(245, 78));

                this.bodyGrid = new VariableButtonGrid(3, 3, form, "RectangleSmallInput", 120, new System.Drawing.Size(163, 78));

                this.personalButton = new System.Windows.Forms.PictureBox();
                this.businessButton = new System.Windows.Forms.PictureBox();

                init(form);
            }
            
            public void init(Form1 form)
            {
                // 
                // backArrow
                // 
                this.backArrow.Cursor = System.Windows.Forms.Cursors.Hand;
                this.backArrow.BackgroundImage = Resources.backArrow;
                this.backArrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.backArrow.Location = new System.Drawing.Point(31, 307);
                this.backArrow.Size = new System.Drawing.Size(51, 74);
                this.backArrow.TabIndex = 4;
                this.backArrow.TabStop = false;
                this.backArrow.Click += new System.EventHandler(form.backArrow_InputPage1_Click);
                // 
                // nextArrow
                // 
                this.nextArrow.Cursor = System.Windows.Forms.Cursors.Hand;
                this.nextArrow.BackgroundImage = Resources.nextArrow;
                this.nextArrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.nextArrow.Size = new System.Drawing.Size(51, 74);
                this.nextArrow.TabIndex = 4;
                this.nextArrow.TabStop = false;
                this.nextArrow.Click += new System.EventHandler(form.nextArrow_Click);
                //
                // heading
                //
                this.heading.AutoSize = true;
                this.heading.Font = new System.Drawing.Font("Segoe UI", 40F);
                this.heading.Location = new System.Drawing.Point(96, 34);
                this.heading.Name = "heading";
                this.heading.Size = new System.Drawing.Size(1003, 76);
                this.heading.TabIndex = 1;
                this.heading.Text = "Please enter the following details";
                this.heading.ForeColor = System.Drawing.ColorTranslator.FromHtml("#4086FF");
                this.heading.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");
                //
                // headingPanel
                //
                this.headingPanel.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");
                this.headingPanel.Controls.Add(this.heading);
                this.headingPanel.Location = new System.Drawing.Point(0, 0);
                this.headingPanel.Name = "panel1";
                //
                // OwnerLabel
                //
                this.ownerLabel.AutoSize = true;
                this.ownerLabel.Font = new System.Drawing.Font("Segoe UI", 20F);
                this.ownerLabel.Location = new System.Drawing.Point(220, 230);
                this.ownerLabel.Name = "heading";
                this.ownerLabel.Size = new System.Drawing.Size(200, 50);
                this.ownerLabel.TabIndex = 1;
                this.ownerLabel.Text = "Owner's Name:";
                this.ownerLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6e6e6e");
                //
                // PriceLabel
                //
                this.priceLabel.AutoSize = true;
                this.priceLabel.Font = new System.Drawing.Font("Segoe UI", 20F);
                this.priceLabel.Location = new System.Drawing.Point(220, 230);
                this.priceLabel.Name = "heading";
                this.priceLabel.Size = new System.Drawing.Size(200, 50);
                this.priceLabel.TabIndex = 1;
                this.priceLabel.Text = "Wholesale Price (AUD):";
                this.priceLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6e6e6e");
                //
                // UseLabel
                //
                this.useLabel.AutoSize = true;
                this.useLabel.Font = new System.Drawing.Font("Segoe UI", 20F);
                this.useLabel.Location = new System.Drawing.Point(220, 230);
                this.useLabel.Name = "heading";
                this.useLabel.Size = new System.Drawing.Size(200, 50);
                this.useLabel.TabIndex = 1;
                this.useLabel.Text = "Use:";
                this.useLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6e6e6e");
                //
                // MakeLabel
                //
                this.makeLabel.AutoSize = true;
                this.makeLabel.Font = new System.Drawing.Font("Segoe UI", 20F);
                this.makeLabel.Location = new System.Drawing.Point(220, 230);
                this.makeLabel.Name = "heading";
                this.makeLabel.Size = new System.Drawing.Size(200, 50);
                this.makeLabel.TabIndex = 1;
                this.makeLabel.Text = "Vehicle Make:";
                this.makeLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6e6e6e");
                //
                // BodyLabel
                //
                this.bodyLabel.AutoSize = true;
                this.bodyLabel.Font = new System.Drawing.Font("Segoe UI", 20F);
                this.bodyLabel.Location = new System.Drawing.Point(220, 230);
                this.bodyLabel.Name = "heading";
                this.bodyLabel.Size = new System.Drawing.Size(200, 50);
                this.bodyLabel.TabIndex = 1;
                this.bodyLabel.Text = "Body Type:";
                this.bodyLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6e6e6e");
                // 
                // Personal Button
                // 
                this.personalButton.Cursor = System.Windows.Forms.Cursors.Hand;
                this.personalButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.personalButton.Size = new System.Drawing.Size(142, 72);
                this.personalButton.TabIndex = 4;
                this.personalButton.TabStop = false;
                this.personalButton.Click += new System.EventHandler(form.personalButton_Click);
                // 
                // Business Button
                // 
                this.businessButton.Cursor = System.Windows.Forms.Cursors.Hand;
                this.businessButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.businessButton.Size = new System.Drawing.Size(142,72);
                this.businessButton.TabIndex = 4;
                this.businessButton.TabStop = false;
                this.businessButton.Click += new System.EventHandler(form.businessButton_Click);

                form.Controls.Add(this.backArrow);
                form.Controls.Add(this.nextArrow);
                form.Controls.Add(this.heading);
                form.Controls.Add(this.headingPanel);
                form.Controls.Add(this.ownerLabel);
                form.Controls.Add(this.priceLabel);
                form.Controls.Add(this.useLabel);
                form.Controls.Add(this.makeLabel);
                form.Controls.Add(this.bodyLabel);
                form.Controls.Add(this.personalButton);
                form.Controls.Add(this.businessButton);

                string[] list = new string[9];
                SQLiteCommand queryMakes = form.connection.CreateCommand();

                queryMakes.CommandText = "SELECT * FROM bodies;";

                SQLiteDataReader reader = queryMakes.ExecuteReader();

                int i = 0;
                while (reader.Read())
                {
                    if (i < list.Length)
                        list[i] = Convert.ToString(reader["bodyStyle"]);
                    //Console.WriteLine(Convert.ToString(reader["make"]));
                    i++;
                }

                list[8] = "Unknown";

                this.bodyGrid.setFontScale(40);
                this.bodyGrid.setText(list);

                this.nameInput = new CustomTextBox(200, 300, 280, 50, form);
                this.nameInput.inputText.TextChanged += (sender, EventArgs) => {
                    int selectionStart = this.nameInput.inputText.SelectionStart;
                    this.nameInput.inputText.Text = Regex.Replace(this.nameInput.inputText.Text, "([^a-zA-Z ,-])", "");
                    this.nameInput.inputText.SelectionStart = selectionStart;
                };
                this.nameInput.inputText.MaxLength = 26;

                this.priceInput = new CustomTextBox(200, 300, 155, 50, form);
                this.priceInput.inputText.TextChanged += (sender, EventArgs) => {
                    int selectionStart = this.priceInput.inputText.SelectionStart;
                    this.priceInput.inputText.Text = Regex.Replace(this.priceInput.inputText.Text, "([^0-9 ,.$])", "");
                    this.priceInput.inputText.SelectionStart = selectionStart;
                };
                this.priceInput.inputText.MaxLength = 13;

                this.makeInput = new CustomTextBox(200, 300, 218, 50, form);
                this.makeInput.inputText.TextChanged += (sender, EventArgs) => { setMakes(this.makeInput.inputText.Text, form); };
            }

            public void hide()
            {
                this.backArrow.Visible = false;
                this.nextArrow.Visible = false;
                this.heading.Visible = false;
                this.headingPanel.Visible = false;
                this.nameInput.Visible = false;
                this.priceInput.Visible = false;
                this.makeInput.Visible = false;

                this.ownerLabel.Visible = false;
                this.priceLabel.Visible = false;
                this.useLabel.Visible = false;
                this.makeLabel.Visible = false;
                this.bodyLabel.Visible = false;
                this.personalButton.Visible = false;
                this.businessButton.Visible = false;

                this.makeGrid.Visible = false;
                this.bodyGrid.Visible = false;
            }

            public void show(Form1 form)
            {
                this.backArrow.Visible = true;
                this.nextArrow.Visible = true;
                this.heading.Visible = true;
                this.headingPanel.Visible = true;
                this.nameInput.Visible = true;
                this.priceInput.Visible = true;
                this.makeInput.Visible = true;

                this.ownerLabel.Visible = true;
                this.priceLabel.Visible = true;
                this.useLabel.Visible = true;
                this.makeLabel.Visible = true;
                this.bodyLabel.Visible = true;
                this.personalButton.Visible = true;
                this.businessButton.Visible = true;

                this.makeGrid.Visible = true;
                this.bodyGrid.Visible = true;

                if (this.darkMode)
                {
                    form.BackColor = System.Drawing.ColorTranslator.FromHtml("#182039");
                }
                else
                {
                    form.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                }
                
            }

            public void resize(Form1 form)
            {
                this.backArrow.Location = new System.Drawing.Point(42, form.Height/2 - 40);
                this.nextArrow.Location = new System.Drawing.Point(form.Width - 100, form.Height/2 - 40);
                this.heading.Location = new System.Drawing.Point(form.Width/2 - 410, 23);
                this.headingPanel.Size = new System.Drawing.Size(form.Width, 120);

                this.ownerLabel.Location = new System.Drawing.Point(form.Width / 2 - 290, form.Height / 2 - 170);
                this.nameInput.move(form.Width / 2 - 300, form.Height / 2 - 120);

                this.priceLabel.Location = new System.Drawing.Point(form.Width / 2 - 330, form.Height / 2 - 20);
                this.priceInput.move(form.Width / 2 - 250, form.Height/2 + 30);

                this.useLabel.Location = new System.Drawing.Point(form.Width / 2 - 300, form.Height / 2 + 130);
                this.personalButton.Location = new System.Drawing.Point(form.Width / 2 - 240, form.Height / 2 + 150);
                this.businessButton.Location = new System.Drawing.Point(form.Width / 2 - 240, form.Height / 2 + 100);

                this.makeLabel.Location = new System.Drawing.Point(form.Width / 2 + 100, form.Height / 2 - 170);
                this.makeInput.move(form.Width / 2 + 108, form.Height / 2 - 120);
                this.makeGrid.move(form.Width / 2 + 0, form.Height / 2 - 80);

                this.bodyLabel.Location = new System.Drawing.Point(form.Width / 2 + 120, form.Height / 2 + 60);
                this.bodyGrid.move(form.Width / 2 + 5, form.Height / 2 + 100);
            }

            public void theme(Form1 form, bool darkMode)
            {
                this.darkMode = darkMode;
                if (darkMode)
                {
                    this.heading.BackColor = System.Drawing.ColorTranslator.FromHtml("#222B49");
                    this.headingPanel.BackColor = System.Drawing.ColorTranslator.FromHtml("#222B49");

                    this.ownerLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    this.priceLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    this.useLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    this.makeLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    this.bodyLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

                    this.heading.ForeColor = System.Drawing.ColorTranslator.FromHtml("#5364FF");

                    this.ownerLabel.BackColor = System.Drawing.ColorTranslator.FromHtml("#182039");
                    this.priceLabel.BackColor = System.Drawing.ColorTranslator.FromHtml("#182039");
                    this.useLabel.BackColor = System.Drawing.ColorTranslator.FromHtml("#182039");
                    this.makeLabel.BackColor = System.Drawing.ColorTranslator.FromHtml("#182039");
                    this.bodyLabel.BackColor = System.Drawing.ColorTranslator.FromHtml("#182039");
                    form.BackColor = System.Drawing.ColorTranslator.FromHtml("#182039");

                    this.nameInput.setImage(Resources.RectangleWide_dark, "#FFFFFF");
                    this.priceInput.setImage(Resources.RectangleShort_dark, "#FFFFFF");
                    this.makeInput.setImage(Resources.RectangleMid_dark, "#FFFFFF");

                    this.personalButton.BackgroundImage = Resources.PersonalButton_dark;
                    this.businessButton.BackgroundImage = Resources.BussinessButton_dark;

                    this.makeGrid.setTheme(true);
                    this.bodyGrid.setTheme(true);

                    if (this.personal.HasValue)
                    {
                        if ((bool)this.personal)
                        {
                            this.personalButton.BackgroundImage = Resources.PersonalButton_select;
                        }
                        else
                        {
                            this.businessButton.BackgroundImage = Resources.BussinessButton_select;
                        }
                    }
                }
                else
                {
                    this.makeGrid.setTheme(false);
                    this.bodyGrid.setTheme(false);

                    this.heading.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");
                    this.headingPanel.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");

                    this.ownerLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6D6D6D");
                    this.priceLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6D6D6D");
                    this.useLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6D6D6D");
                    this.makeLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6D6D6D");
                    this.bodyLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6D6D6D");

                    this.heading.ForeColor = System.Drawing.ColorTranslator.FromHtml("#5364FF");

                    this.ownerLabel.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    this.priceLabel.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    this.useLabel.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    this.makeLabel.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    this.bodyLabel.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    form.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

                    this.nameInput.setImage(Resources.RectangleWide_light, "#707070");
                    this.priceInput.setImage(Resources.RectangleShort_light, "#707070");
                    this.makeInput.setImage(Resources.RectangleMid_light, "#707070");


                    this.personalButton.BackgroundImage = Resources.PersonalButton_light;
                    this.businessButton.BackgroundImage = Resources.BussinessButton_light;
                    if (this.personal.HasValue)
                    {
                        if (this.personal.Value)
                        {
                            this.personalButton.BackgroundImage = Resources.PersonalButton_select;
                        }
                        else
                        {
                            this.businessButton.BackgroundImage = Resources.BussinessButton_select;
                        }
                    }
                }
            }

            public void selectUse(bool personal)
            {
                if (this.darkMode)
                {
                    this.businessButton.BackgroundImage = Resources.BussinessButton_dark;
                    this.personalButton.BackgroundImage = Resources.PersonalButton_dark;
                }
                else
                {
                    this.businessButton.BackgroundImage = Resources.BussinessButton_light;
                    this.personalButton.BackgroundImage = Resources.PersonalButton_light;
                }

                bool? oldPersonal = this.personal;
                this.personal = personal;

                if (oldPersonal.HasValue)
                {
                    if (oldPersonal.Value == personal)
                    {
                        this.personal = null;
                        //Console.WriteLine("State 0");
                    }
                    else if (personal)
                    {
                        this.personalButton.BackgroundImage = Resources.PersonalButton_select;
                        //Console.WriteLine("State 1");
                    }
                    else
                    {
                        this.businessButton.BackgroundImage = Resources.BussinessButton_select;
                        //Console.WriteLine("State 2");
                    }
                }
                else
                {
                    if (personal)
                    {
                        this.personalButton.BackgroundImage = Resources.PersonalButton_select;
                        //Console.WriteLine("State 3");
                    }
                    else
                    {
                        this.businessButton.BackgroundImage = Resources.BussinessButton_select;
                        //Console.WriteLine("State 4");
                    }
                }
            }

            public void setMakes(string text, Form1 form)
            {
                //Console.WriteLine("Begin query ----------------------------------");

                text = Regex.Replace(text, "([^a-zA-Z -])", "");

                //if (text == "")
                //    return;

                string[] list = new string[6];
                SQLiteCommand queryMakes = form.connection.CreateCommand();
                
                queryMakes.CommandText = "SELECT * FROM makes WHERE (make LIKE '" + text + "%' or make LIKE'% " + text + "%');";

                SQLiteDataReader reader = queryMakes.ExecuteReader();

                int i = 0;
                while (reader.Read())
                {
                    if (i < 5)
                        list[i] = Convert.ToString(reader["make"]);
                    //Console.WriteLine(Convert.ToString(reader["make"]));
                    i++;
                }

                this.makeGrid.setText(list);
                if (list[0] != null)
                    if (list[0].ToLower() == text.ToLower())
                        this.makeGrid.updateSelected(0);
            }

            public decimal? parsePrice()
            {
                try
                {
                    return Decimal.Parse(Regex.Replace(this.priceInput.inputText.Text, "([^0-9.])", ""));
                }
                catch (Exception)
                {
                    //Do nothing
                }

                return null;
            }

            public string getSelectedMake()
            {
                return this.makeGrid.getSelected();
            }

            public string getSelectedBody()
            {
                return this.bodyGrid.getSelected();
            }
        }

        private class InputScreen2
        {
            public System.Windows.Forms.PictureBox backArrow;

            public System.Windows.Forms.PictureBox upArrow;
            public System.Windows.Forms.PictureBox downArrow;

            public System.Windows.Forms.PictureBox calculateButton;
            public System.Windows.Forms.Label heading;
            public System.Windows.Forms.Panel headingPanel;

            public System.Windows.Forms.Label modelLabel;
            public CustomTextBox modelBox;

            public System.Windows.Forms.Label yearLabel;
            public CustomTextBox yearBox;

            public System.Windows.Forms.Label weightLabel;
            public CustomTextBox weightBox;

            public System.Windows.Forms.Label promptLabel;

            public CustomTextBox searchBar;

            public resultModelContainer container;

            public Timer timer;

            private bool darkMode;
            public InputScreen2(Form1 form)
            {
                timer = new System.Timers.Timer(300);
                container = new resultModelContainer(3, form);
                this.backArrow = new System.Windows.Forms.PictureBox();

                this.upArrow = new System.Windows.Forms.PictureBox();
                this.downArrow = new System.Windows.Forms.PictureBox();

                this.heading = new System.Windows.Forms.Label();
                this.promptLabel = new System.Windows.Forms.Label();
                this.headingPanel = new System.Windows.Forms.Panel();
                this.calculateButton = new System.Windows.Forms.PictureBox();

                this.modelLabel = new System.Windows.Forms.Label();
                this.yearLabel = new System.Windows.Forms.Label();
                this.weightLabel = new System.Windows.Forms.Label();

                this.modelBox = new CustomTextBox(0, 0, 180, 41, form);
                this.yearBox = new CustomTextBox(0, 0, 180, 41, form);
                this.weightBox = new CustomTextBox(0, 0, 180, 41, form);

                this.searchBar = new CustomTextBox(0, 0, 442, 57, form);

                this.searchBar.inputText.TextChanged += (sender, EventArgs) => {
                   // Console.WriteLine("anonymous");
                    timer.Stop();
                    timer.Start();
                    //this.container.updateList(this.searchBar.inputText.Text, form);
                };

                this.timer.Elapsed += (sender, EventArgs) => {
                    timer.Stop();
                    //Console.WriteLine("anonymous");
                    this.container.updateList(this.searchBar.inputText.Text, form);
                };

                this.modelBox.inputText.TextChanged += (sender, EventArgs) => {
                    updateSubmit();

                    if (this.modelBox.inputText.Text != "")
                        this.container.clearSelection(form);
                };

                this.weightBox.inputText.TextChanged += (sender, EventArgs) => {
                    updateSubmit();

                    if (this.weightBox.inputText.Text != "")
                        this.container.clearSelection(form);
                };

                this.yearBox.inputText.TextChanged += (sender, EventArgs) => {
                    updateSubmit();

                    if (this.yearBox.inputText.Text != "")
                        this.container.clearSelection(form);
                };

                this.upArrow.Click += (sender, EventArgs) => {
                    //Console.WriteLine("Pressed up arrow");
                    this.container.moveRange(false);
                };

                this.downArrow.Click += (sender, EventArgs) => {
                    //Console.WriteLine("Pressed down arrow");
                    this.container.moveRange(true);
                };

                this.upArrow.DoubleClick += (sender, EventArgs) => {
                    //Console.WriteLine("Pressed up arrow");
                    this.container.moveRange(false);
                };

                this.downArrow.DoubleClick += (sender, EventArgs) => {
                    //Console.WriteLine("Pressed down arrow");
                    this.container.moveRange(true);
                };

                init(form);
            }

            public void init(Form1 form)
            {
                //
                // Heading Panel
                //
                this.headingPanel.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");
                this.headingPanel.Location = new System.Drawing.Point(0, 0);
                this.headingPanel.Name = "panel1";
                //
                // Heading Text
                //
                this.heading.AutoSize = true;
                this.heading.Font = new System.Drawing.Font("Segoe UI", 40F);
                this.heading.Location = new System.Drawing.Point(96, 34);
                this.heading.Name = "heading";
                this.heading.Size = new System.Drawing.Size(1003, 76);
                this.heading.TabIndex = 1;
                this.heading.Text = "Please select the customer's model";
                this.heading.ForeColor = System.Drawing.ColorTranslator.FromHtml("#4086FF");
                this.heading.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");
                // 
                // backArrow
                // 
                this.backArrow.Cursor = System.Windows.Forms.Cursors.Hand;
                this.backArrow.BackgroundImage = Resources.backArrow;
                this.backArrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.backArrow.Location = new System.Drawing.Point(31, 307);
                this.backArrow.Name = "pictureBox4";
                this.backArrow.Size = new System.Drawing.Size(51, 74);
                this.backArrow.TabIndex = 4;
                this.backArrow.TabStop = false;
                this.backArrow.Click += new System.EventHandler(form.backArrow_InputPage2_Click);
                // 
                // up Arrow
                // 
                this.upArrow.Cursor = System.Windows.Forms.Cursors.Hand;
                Image img = Resources.backArrow;
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                this.upArrow.BackgroundImage = img;
                this.upArrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.upArrow.Size = new System.Drawing.Size(52, 36);
                // 
                // down Arrow
                // 
                this.downArrow.Cursor = System.Windows.Forms.Cursors.Hand;
                Image img2 = Resources.backArrow;
                img2.RotateFlip(RotateFlipType.Rotate90FlipY);
                this.downArrow.BackgroundImage = img2;
                this.downArrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.downArrow.Size = new System.Drawing.Size(52, 36);
                //
                // calculate button
                //
                this.calculateButton.Cursor = System.Windows.Forms.Cursors.Hand;
                this.calculateButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.calculateButton.Size = new System.Drawing.Size(375, 105);
                this.calculateButton.TabIndex = 2;
                this.calculateButton.TabStop = false;
                this.calculateButton.Click += new System.EventHandler(form.calculateResults_Click);
                //
                // model Label
                //
                this.modelLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
                this.modelLabel.Size = new System.Drawing.Size(200, 50);
                this.modelLabel.Text = "Model Name:";
                this.modelLabel.TextAlign = ContentAlignment.MiddleRight;
                //
                // year Label
                //
                this.yearLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
                this.yearLabel.Size = new System.Drawing.Size(200, 50);
                this.yearLabel.Text = "Year:";
                this.yearLabel.TextAlign = ContentAlignment.MiddleRight;
                //
                // weight Label
                //
                this.weightLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
                this.weightLabel.Size = new System.Drawing.Size(200, 50);
                this.weightLabel.Text = "Weight (KG):";
                this.weightLabel.TextAlign = ContentAlignment.MiddleRight;
                //
                // prompt Label
                //
                this.promptLabel.Font = new System.Drawing.Font("Segoe UI", 13F);
                this.promptLabel.Size = new System.Drawing.Size(600, 100);
                this.promptLabel.Text = "Can't find the customer's vehicle?\nEnter the details for another model below:";
                this.promptLabel.TextAlign = ContentAlignment.MiddleCenter;

                form.Controls.Add(this.backArrow);
                form.Controls.Add(this.upArrow);
                form.Controls.Add(this.downArrow);

                form.Controls.Add(this.heading);
                form.Controls.Add(this.headingPanel);
                form.Controls.Add(this.calculateButton);

                form.Controls.Add(this.promptLabel);
                form.Controls.Add(this.modelLabel);
                form.Controls.Add(this.yearLabel);
                form.Controls.Add(this.weightLabel);

                container.updateList("", form);

                this.modelBox.inputText.MaxLength = 17;

                this.weightBox.inputText.TextChanged += (sender, EventArgs) => {
                    int selectionStart = this.weightBox.inputText.SelectionStart;
                    this.weightBox.inputText.Text = Regex.Replace(this.weightBox.inputText.Text, "([^0-9, .])", "");
                    this.weightBox.inputText.SelectionStart = selectionStart;
                };
                this.weightBox.inputText.MaxLength = 8;

                this.yearBox.inputText.TextChanged += (sender, EventArgs) => {
                    int selectionStart = this.yearBox.inputText.SelectionStart;
                    //Console.WriteLine(this.modelBox.inputText.Text);
                    this.yearBox.inputText.Text = Regex.Replace(this.yearBox.inputText.Text, "([^0-9])", "");
                    this.yearBox.inputText.SelectionStart = selectionStart;
                };

                this.yearBox.inputText.MaxLength = 4;

                this.searchBar.backgroundImage.Cursor = System.Windows.Forms.Cursors.Hand;
            }

            public void hide()
            {
                this.backArrow.Visible = false;
                this.upArrow.Visible = false;
                this.downArrow.Visible = false;
                this.heading.Visible = false;
                this.headingPanel.Visible = false;
                this.calculateButton.Visible = false;
                this.promptLabel.Visible = false;

                this.modelLabel.Visible = false;
                this.yearLabel.Visible = false;
                this.weightLabel.Visible = false;

                this.modelBox.Visible = false;
                this.yearBox.Visible = false;
                this.weightBox.Visible = false;
                this.searchBar.Visible = false;

                this.container.setVisibility(false);
            }

            public void show(Form1 form)
            {
                this.backArrow.Visible = true;
                this.upArrow.Visible = true;
                this.downArrow.Visible = true;
                this.calculateButton.Visible = true;
                this.heading.Visible = true;
                this.headingPanel.Visible = true;
                this.promptLabel.Visible = true;

                this.modelLabel.Visible = true;
                this.yearLabel.Visible = true;
                this.weightLabel.Visible = true;

                this.modelBox.Visible = true;
                this.yearBox.Visible = true;
                this.weightBox.Visible = true;
                this.searchBar.Visible = true;

                this.container.setVisibility(true);

                container.draw(darkMode);

                if (this.searchBar.inputText.Text == "")
                    this.searchBar.inputText.Visible = false;

                updateSubmit();
            }

            public void resize(Form1 form)
            {
                this.backArrow.Location = new System.Drawing.Point(42, form.Height/2 - 40);
                this.heading.Location = new System.Drawing.Point(form.Width/2 - 440, 23);
                this.headingPanel.Size = new System.Drawing.Size(form.Width, 120);
                this.calculateButton.Location = new System.Drawing.Point(form.Width / 2 - 148, form.Height - 160);

                this.promptLabel.Location = new System.Drawing.Point(form.Width / 2 - 440, form.Height / 2 - 80);

                this.modelLabel.Location = new System.Drawing.Point(form.Width / 2 - 400, form.Height / 2 + 0);
                this.modelBox.move(form.Width / 2 - 230, form.Height / 2 + 10);

                this.yearLabel.Location = new System.Drawing.Point(form.Width / 2 - 400, form.Height / 2 + 60);
                this.yearBox.move(form.Width / 2 - 230, form.Height / 2 + 70);

                this.weightLabel.Location = new System.Drawing.Point(form.Width / 2 - 400, form.Height / 2 + 120);
                this.weightBox.move(form.Width / 2 - 230, form.Height / 2 + 130);

                this.searchBar.move(form.Width / 2 - 400, form.Height / 2 - 130);

                container.move(form.Width / 2 - 40, form.Height / 2 - 150);

                this.upArrow.Location = new System.Drawing.Point(form.Width / 2 + 200, form.Height / 2 - 190);

                this.downArrow.Location = new System.Drawing.Point(form.Width / 2 + 200, form.Height / 2 + 150);
            }

            public void theme (Form1 form, bool darkMode)
            {
                this.darkMode = darkMode;
                this.container.draw(darkMode);
                if (darkMode)
                {
                    this.heading.BackColor = System.Drawing.ColorTranslator.FromHtml("#222B49");
                    this.headingPanel.BackColor = System.Drawing.ColorTranslator.FromHtml("#222B49");

                    this.heading.ForeColor = System.Drawing.ColorTranslator.FromHtml("#5364FF");

                    this.promptLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

                    this.modelLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    this.modelBox.setImage(Resources.CustomModel_dark ,"#FFFFFF");

                    this.yearLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    this.yearBox.setImage(Resources.CustomModel_dark, "#FFFFFF");

                    this.weightLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    this.weightBox.setImage(Resources.CustomModel_dark, "#FFFFFF");


                    if (this.searchBar.inputText.Text == "")
                    {
                        this.searchBar.setImage(Resources.SearchDefault_dark, "#707070");
                        this.searchBar.inputText.Visible = false;
                    }
                    else
                    {
                        this.searchBar.setImage(Resources.Search_dark, "#FFFFFFF");
                    }

                    this.searchBar.backgroundImage.Click += (sender, EventArgs) => {
                        this.searchBar.inputText.Visible = true;
                        this.searchBar.setImage(Resources.Search_dark, "#FFFFFF");
                        form.ActiveControl = this.searchBar.inputText;
                    };

                    this.searchBar.inputText.LostFocus += (sender, EventArgs) => {
                        if (this.searchBar.inputText.Text == "")
                        {
                            this.searchBar.inputText.Visible = false;
                            this.searchBar.setImage(Resources.SearchDefault_dark, "#707070");
                        }
                    };

                    updateSubmit();
                }
                else
                {
                    this.heading.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");
                    this.headingPanel.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");

                    this.heading.ForeColor = System.Drawing.ColorTranslator.FromHtml("#5364FF");

                    this.promptLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6D6D6D");

                    this.modelLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6D6D6D");
                    this.modelBox.setImage(Resources.CustomModel_light, "#707070");

                    this.yearLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6D6D6D");
                    this.yearBox.setImage(Resources.CustomModel_light, "#707070");

                    this.weightLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6D6D6D");
                    this.weightBox.setImage(Resources.CustomModel_light, "#707070");

                    if (this.searchBar.inputText.Text == "")
                    {
                        this.searchBar.setImage(Resources.SearchDefault_light, "#707070");
                        this.searchBar.inputText.Visible = false;
                    }
                    else
                    {
                        this.searchBar.setImage(Resources.Search_light, "#707070");
                    }

                    this.searchBar.backgroundImage.Click += (sender, EventArgs) => {
                        this.searchBar.inputText.Visible = true;
                        this.searchBar.setImage(Resources.Search_light, "#707070");
                        form.ActiveControl = this.searchBar.inputText;
                    };

                    this.searchBar.inputText.LostFocus += (sender, EventArgs) => {
                        if (this.searchBar.inputText.Text == "")
                        {
                            this.searchBar.setImage(Resources.SearchDefault_light, "#707070");
                            this.searchBar.inputText.Visible = false;
                        }
                    };

                    updateSubmit();
                }
            }

            public void updateSubmit()
            {
                //Console.WriteLine("checked update submit");
                //Console.WriteLine("checked = " + (this.modelBox.inputText.Text != "").ToString() +
                //    ", and " + Regex.Match(this.yearBox.inputText.Text, "[0-9]{4}").Success.ToString() +
                //    ", and " + Regex.Match(this.weightBox.inputText.Text, "[0-9]{3}").Success.ToString());
                bool validCustom = (this.modelBox.inputText.Text != "")
                    && Regex.Match(this.yearBox.inputText.Text, "[0-9]{4}").Success
                    && Regex.Match(this.weightBox.inputText.Text, "[0-9]{3}").Success;

                //Console.WriteLine("checked = " + validCustom.ToString());

                if (this.container.selectionIndex.HasValue || validCustom)
                {
                    //Console.WriteLine("Updated this");
                    this.calculateButton.BackgroundImage = Resources.ResultsButton;
                    this.calculateButton.Refresh();
                    //this.calculateButton.BackgroundImage = Resources.ResultsButton;
                    //Console.WriteLine("a = " + Resources.ResultsButton.GetHashCode() + ", b = " + this.calculateButton.BackgroundImage.GetHashCode());
                }
                else if (this.darkMode)
                {
                    //Console.WriteLine("executed this");
                    this.calculateButton.BackgroundImage = Resources.GreyCaalculateResults_dark;
                }
                else
                {
                    this.calculateButton.BackgroundImage = Resources.GreyCalculateResults_light;
                }

                if (this.container.selectionIndex.HasValue || validCustom)
                    this.calculateButton.BackgroundImage = Resources.ResultsButton;
            }
        }

        private class HistoryPage
        {
            public System.Windows.Forms.PictureBox backArrow;

            public System.Windows.Forms.PictureBox upArrow;
            public System.Windows.Forms.PictureBox downArrow;

            public System.Windows.Forms.Label heading;
            public System.Windows.Forms.Panel headingPanel;

            public resultOwnerContainer container;

            private bool darkMode;
            public HistoryPage(Form1 form)
            {
                container = new resultOwnerContainer(12, form);
                this.backArrow = new System.Windows.Forms.PictureBox();

                this.upArrow = new System.Windows.Forms.PictureBox();
                this.downArrow = new System.Windows.Forms.PictureBox();

                this.heading = new System.Windows.Forms.Label();
                this.headingPanel = new System.Windows.Forms.Panel();

                this.upArrow.Click += (sender, EventArgs) => {
                    //Console.WriteLine("Pressed up arrow");
                    this.container.moveRange(false);
                };

                this.downArrow.Click += (sender, EventArgs) => {
                    //Console.WriteLine("Pressed down arrow");
                    this.container.moveRange(true);
                };

                this.upArrow.DoubleClick += (sender, EventArgs) => {
                    //Console.WriteLine("Pressed up arrow");
                    this.container.moveRange(false);
                };

                this.downArrow.DoubleClick += (sender, EventArgs) => {
                    //Console.WriteLine("Pressed down arrow");
                    this.container.moveRange(true);
                };

                init(form);
            }

            public void init(Form1 form)
            {
                //
                // Heading Panel
                //
                this.headingPanel.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");
                this.headingPanel.Location = new System.Drawing.Point(0, 0);
                this.headingPanel.Name = "panel1";
                //
                // Heading Text
                //
                this.heading.AutoSize = true;
                this.heading.Font = new System.Drawing.Font("Segoe UI", 40F);
                this.heading.Location = new System.Drawing.Point(96, 34);
                this.heading.Name = "heading";
                this.heading.Size = new System.Drawing.Size(1003, 76);
                this.heading.TabIndex = 1;
                this.heading.Text = "History";
                this.heading.ForeColor = System.Drawing.ColorTranslator.FromHtml("#4086FF");
                this.heading.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");
                // 
                // backArrow
                // 
                this.backArrow.Cursor = System.Windows.Forms.Cursors.Hand;
                this.backArrow.BackgroundImage = Resources.backArrow;
                this.backArrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.backArrow.Location = new System.Drawing.Point(31, 307);
                this.backArrow.Name = "pictureBox4";
                this.backArrow.Size = new System.Drawing.Size(51, 74);
                this.backArrow.TabIndex = 4;
                this.backArrow.TabStop = false;
                this.backArrow.Click += new System.EventHandler(form.backArrow_HistoryPage_Click);
                // 
                // up Arrow
                // 
                this.upArrow.Cursor = System.Windows.Forms.Cursors.Hand;
                Image img = Resources.backArrow;
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                this.upArrow.BackgroundImage = img;
                this.upArrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.upArrow.Size = new System.Drawing.Size(52, 36);
                // 
                // down Arrow
                // 
                this.downArrow.Cursor = System.Windows.Forms.Cursors.Hand;
                Image img2 = Resources.backArrow;
                img2.RotateFlip(RotateFlipType.Rotate90FlipY);
                this.downArrow.BackgroundImage = img2;
                this.downArrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.downArrow.Size = new System.Drawing.Size(52, 36);
                
                form.Controls.Add(this.backArrow);
                form.Controls.Add(this.upArrow);
                form.Controls.Add(this.downArrow);

                form.Controls.Add(this.heading);
                form.Controls.Add(this.headingPanel);

                container.updateList(form);

            }

            public void hide()
            {
                this.backArrow.Visible = false;
                this.upArrow.Visible = false;
                this.downArrow.Visible = false;
                this.heading.Visible = false;
                this.headingPanel.Visible = false;

                this.container.setVisibility(false);
            }

            public void resizeEnd(Form1 form)
            {
                byte height = (byte)(form.Height / 120 - 1);
                //Console.WriteLine("height = " + height);

                this.container.changePictureBoxVisibility(height);
            }

            public void show(Form1 form)
            {
                this.backArrow.Visible = true;
                this.upArrow.Visible = true;
                this.downArrow.Visible = true;
                this.heading.Visible = true;
                this.headingPanel.Visible = true;

                this.container.setVisibility(true);

                container.draw(darkMode);

                container.updateList(form);

                if (this.darkMode)
                {
                    form.BackColor = System.Drawing.ColorTranslator.FromHtml("#182039");
                }
                else
                {
                    form.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                }
            }

            public void resize(Form1 form)
            {
                this.backArrow.Location = new System.Drawing.Point(42, form.Height / 2 - 40);
                this.heading.Location = new System.Drawing.Point(form.Width / 2 - 95, 23);
                this.headingPanel.Size = new System.Drawing.Size(form.Width, 120);

                container.move(form.Width / 2 - 430, 140);

                this.upArrow.Location = new System.Drawing.Point(form.Width - 100, form.Height / 2 - 60);

                this.downArrow.Location = new System.Drawing.Point(form.Width - 100, form.Height / 2 + 20);
            }

            public void theme(Form1 form, bool darkMode)
            {
                this.darkMode = darkMode;
                this.container.draw(darkMode);
                if (darkMode)
                {
                    this.heading.BackColor = System.Drawing.ColorTranslator.FromHtml("#222B49");
                    this.headingPanel.BackColor = System.Drawing.ColorTranslator.FromHtml("#222B49");

                    this.heading.ForeColor = System.Drawing.ColorTranslator.FromHtml("#5364FF");
                }
                else
                {
                    this.heading.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");
                    this.headingPanel.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");

                    this.heading.ForeColor = System.Drawing.ColorTranslator.FromHtml("#5364FF");
                }
            }
        }

        private class ResultsScreen
        {
            public System.Windows.Forms.PictureBox backArrow;
            public System.Windows.Forms.Label heading;
            public System.Windows.Forms.Panel headingPanel;
            public System.Windows.Forms.RichTextBox resultsText;
            public System.Windows.Forms.PictureBox homeButton;

            private double[] Datas;
            private string[] colours;
            private bool darkMode;
            public ResultsScreen(Form1 form)
            {
                this.backArrow = new System.Windows.Forms.PictureBox();
                this.homeButton = new System.Windows.Forms.PictureBox();
                this.resultsText = new System.Windows.Forms.RichTextBox();
                this.heading = new System.Windows.Forms.Label();
                this.headingPanel = new System.Windows.Forms.Panel();
                this.Datas = new double[1];
                this.colours = new string[2];
                init(form);
            }

            public void init(Form1 form)
            {
                //
                // Heading Panel
                //
                this.headingPanel.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");
                this.headingPanel.Location = new System.Drawing.Point(0, 0);
                this.headingPanel.Name = "panel1";
                //
                // Heading
                //
                this.heading.AutoSize = true;
                this.heading.Font = new System.Drawing.Font("Segoe UI", 40F);
                this.heading.Location = new System.Drawing.Point(96, 34);
                this.heading.Name = "heading";
                this.heading.Size = new System.Drawing.Size(1003, 76);
                this.heading.TabIndex = 1;
                this.heading.Text = "Results (AUD)";
                this.heading.ForeColor = System.Drawing.ColorTranslator.FromHtml("#4086FF");
                this.heading.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");
                // 
                // backArrow
                // 
                this.backArrow.Cursor = System.Windows.Forms.Cursors.Hand;
                this.backArrow.BackgroundImage = Resources.backArrow;
                this.backArrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.backArrow.Location = new System.Drawing.Point(31, 307);
                this.backArrow.Name = "pictureBox5";
                this.backArrow.Size = new System.Drawing.Size(51, 74);
                this.backArrow.TabIndex = 4;
                this.backArrow.TabStop = false;
                this.backArrow.Click += new System.EventHandler(form.backArrow_ResultsScreen_Click);
                //
                // Results Textbox
                //
                this.resultsText.BorderStyle = System.Windows.Forms.BorderStyle.None;
                this.resultsText.Location = new System.Drawing.Point(909, 409);
                this.resultsText.Name = "richTextBox1";
                this.resultsText.ReadOnly = true;
                this.resultsText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
                this.resultsText.Size = new System.Drawing.Size(800, 400);
                this.resultsText.TabIndex = 6;
                this.resultsText.Text = "Total Reg cst: $69420\nVehicle Tax: #667";
                this.resultsText.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                // 
                // home
                // 
                this.homeButton.Cursor = System.Windows.Forms.Cursors.Hand;
                this.homeButton.BackgroundImage = Resources.Home;
                this.homeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.homeButton.Size = new System.Drawing.Size(207, 99);
                this.homeButton.Click += new System.EventHandler(form.goHomeButton_Click);

                form.Controls.Add(this.homeButton);
                form.Controls.Add(this.backArrow);
                form.Controls.Add(this.heading);
                form.Controls.Add(this.headingPanel);
                form.Controls.Add(this.resultsText);
            }

            public void hide()
            {
                this.backArrow.Visible = false;
                this.heading.Visible = false;
                this.headingPanel.Visible = false;
                this.resultsText.Visible = false;
                this.homeButton.Visible = false;
            }

            public void show(Form1 form)
            {
                this.backArrow.Visible = true;
                this.heading.Visible = true;
                this.headingPanel.Visible = true;
                this.resultsText.Visible = true;
                this.homeButton.Visible = true;
            }

            public void updateText(Form1 form)
            {
                if(Datas.Length > 4)
                    updateText(Datas, form);
            }

            public void updateText(double[] datas, Form1 form)
            {
                Datas = datas;
                String defaultRTF =
@"{\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang1033{\fonttbl{\f0\fnil\fcharset0 Segoe UI;}}
{\colortbl ;$[colours0];$[colours1];}
{\*\generator Riched20 10.0.19041}\viewkind4\uc1 
\pard\sa$[scale]\qc\cf1\f0\fs50
Registration Fee: \cf2$[data1]\cf1\par
Stamp Duty: \cf2$[data2]\cf1\par
Insurance Premium: \cf2$[data3]\cf1\par
Vehicle Tax: \cf2$[data0]\cf1\par
Total Registration Cost: \cf2$[data4]\cf1\par
\b Amount Payable:\b0  \cf2$[data5]\cf1\par
}";
                for(int i = 0; i < datas.Length; i++)
                {
                    defaultRTF = defaultRTF.Replace("$[data" + i + "]", datas[i].ToString("C"));
                }

                for (int i = 0; i < this.colours.Length; i++)
                {
                    defaultRTF = defaultRTF.Replace("$[colours" + i + "]", this.colours[i]);
                }

                defaultRTF = defaultRTF.Replace("$[scale]", (form.Height*0.6 - 200).ToString("N0"));

                this.resultsText.Rtf = defaultRTF;
            }

            public void resize(Form1 form)
            {
                this.backArrow.Location = new System.Drawing.Point(42, form.Height/2 - 40);
                this.heading.Location = new System.Drawing.Point(form.Width/2 - 175, 23);
                this.headingPanel.Size = new System.Drawing.Size(form.Width, 120);
                this.resultsText.Location = new System.Drawing.Point(form.Width/2 - 400, 150);
                this.resultsText.Size = new System.Drawing.Size(800, 2 * form.Height / 3);
                this.homeButton.Location = new System.Drawing.Point(form.Width / 2 - 80, form.Height - 160);
                updateText(form);
            }

            public void theme(Form1 form, bool darkMode)
            {
                this.darkMode = darkMode;
                if (darkMode)
                {
                    this.heading.BackColor = System.Drawing.ColorTranslator.FromHtml("#222B49");
                    this.headingPanel.BackColor = System.Drawing.ColorTranslator.FromHtml("#222B49");
                    this.heading.ForeColor = System.Drawing.ColorTranslator.FromHtml("#5364FF");

                    this.resultsText.BackColor = System.Drawing.ColorTranslator.FromHtml("#182039");
                    this.colours[0] = "\\red255\\green255\\blue255";
                    this.colours[1] = "\\red65\\green135\\blue255";
                }
                else
                {
                    this.heading.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");
                    this.headingPanel.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");
                    this.heading.ForeColor = System.Drawing.ColorTranslator.FromHtml("#5364FF");

                    this.resultsText.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    this.colours[0] = "\\red110\\green110\\blue110";
                    this.colours[1] = "\\red65\\green88\\blue255";
                }
            }
        }

        public class CustomTextBox
        {
            public System.Windows.Forms.PictureBox backgroundImage;
            public System.Windows.Forms.TextBox inputText;
            protected int sizeY;
            protected Form1 form;

            public Boolean Visible
            {
                get { return _isVisible; }
                set
                {
                    _isVisible = value;
                    this.backgroundImage.Visible = value;
                    this.inputText.Visible = value;
                }
            }
            private Boolean _isVisible = true;

            public CustomTextBox(int locationX, int locationY, int sizeX, int sizeY, Form1 form)
            {
                this.form = form;
                this.sizeY = sizeY;

                this.backgroundImage = new System.Windows.Forms.PictureBox();
                this.inputText = new System.Windows.Forms.TextBox();

                this.backgroundImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.backgroundImage.Location = new System.Drawing.Point(locationX - 20, locationY - 6 - sizeY/2);
                this.backgroundImage.Size = new System.Drawing.Size(sizeX + 40, sizeY + 40);

                this.inputText.BorderStyle = System.Windows.Forms.BorderStyle.None;
                this.inputText.Location = new System.Drawing.Point(locationX, locationY);
                this.inputText.Name = "textBox1";
                this.inputText.Size = new System.Drawing.Size(sizeX, sizeY);
                this.inputText.TabIndex = 7;
                this.inputText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                this.inputText.Font = new System.Drawing.Font("Segoe UI", 12F);

                form.Controls.Add(this.inputText);
                form.Controls.Add(this.backgroundImage);
                
            }

            public void move(int locationX, int locationY)
            {
                this.backgroundImage.Location = new System.Drawing.Point(locationX - 18, locationY - this.sizeY/2);
                this.inputText.Location = new System.Drawing.Point(locationX, locationY);
            }

            public void setImage(System.Drawing.Image img, String textHex)
            {
                this.backgroundImage.BackgroundImage = img;
                this.inputText.BackColor = ((System.Drawing.Bitmap)(img)).GetPixel(img.Width/2, img.Height/2);
                this.inputText.ForeColor = System.Drawing.ColorTranslator.FromHtml(textHex);
            }

            public String getText()
            {
                return this.inputText.Text;
            }
        }

        public class VariableButtonGrid
        {
            public System.Windows.Forms.PictureBox[,] buttonArrays;
            string image;
            public string[,] textArrays;
            protected int buttonsX;
            protected int buttonsY;
            public byte? selected;
            private bool darkMode;
            private int offsetX;
            private float scale;

            public Boolean Visible
            {
                get { return _isVisible; }
                set
                {
                    _isVisible = value;
                    for (int i = 0; i < this.buttonsX; i++)
                    {
                        for (int j = 0; j < this.buttonsY; j++)
                        {
                            buttonArrays[i, j].Visible = value;
                        }
                    }
                }
            }
            private Boolean _isVisible = true;

            public VariableButtonGrid(int buttonsX, int buttonsY, Form1 form, string image, int offsetX, Size size)
            {
                this.offsetX = offsetX;
                this.buttonsX = buttonsX;
                this.buttonsY = buttonsY;
                this.image = image;
                this.scale = 60;

                textArrays = new string[buttonsX, buttonsY];
                buttonArrays = new System.Windows.Forms.PictureBox[buttonsX, buttonsY];
                for (int i = 0; i < buttonsX; i++)
                {
                    for (int j = 0; j < buttonsY; j++)
                    {
                        buttonArrays[i, j] = new System.Windows.Forms.PictureBox();
                        buttonArrays[i, j].BackgroundImage = (Image)Resources.ResourceManager.GetObject(image + "_light"); ;
                        buttonArrays[i, j].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                        buttonArrays[i, j].Size = size;
                        byte index = (byte)(i * buttonsX + j);

                        buttonArrays[i, j].Click += (sender, EventArgs) => { this.updateSelected(index); };
                        buttonArrays[i, j].Cursor = System.Windows.Forms.Cursors.Hand;

                        form.Controls.Add(buttonArrays[i, j]);
                    }
                }
            }

            public void setTheme(bool darkMode)
            {
                this.darkMode = darkMode;

                updateText();
            }

            private void reloadImages()
            {
                for (int i = 0; i < buttonsX; i++)
                {
                    for (int j = 0; j < buttonsY; j++)
                    {
                        if (darkMode)
                        {
                            buttonArrays[i, j].BackgroundImage = (Image)Resources.ResourceManager.GetObject(image + "_dark"); ;
                        }
                        else
                        {
                            buttonArrays[i, j].BackgroundImage = (Image)Resources.ResourceManager.GetObject(image + "_light");

                        }

                        if (selected.HasValue)
                        {
                            if (i * buttonsX + j == selected.Value)
                                buttonArrays[i, j].BackgroundImage = (Image)Resources.ResourceManager.GetObject(image + "_select"); ;
                        }
                    }
                }
            }

            public void updateSelected(byte value)
            {
                //Console.WriteLine("value = '" + textArrays[value/buttonsX, value % buttonsX] + "'");

                if (String.IsNullOrEmpty(textArrays[value/buttonsX, value % buttonsX]))
                {
                    //Console.WriteLine("no value");
                    return;
                }
                    

                if (this.selected.HasValue)
                {
                    if (this.selected.Value == value)
                    {
                        this.selected = null;
                    }
                    else
                    {
                        this.selected = value;
                    }
                }
                else
                {
                    this.selected = value;
                }

                updateText();
            }

            public void clearSelection()
            {
                this.selected = null;

                updateText();
            }

            public void move(int X, int Y)
            {
                for (int i = 0; i < this.buttonsX; i++)
                {
                    for (int j = 0; j < this.buttonsY; j++)
                    {
                        buttonArrays[i, j].Location = new System.Drawing.Point((offsetX) * i + X, (78 - 20) * j + Y);
                    }
                }
            }

            public void setText(string[] values)
            {
                clearSelection();
                
                int index = 0;

                for (int i = 0; i < this.buttonsY; i++)
                {
                    for (int j = 0; j < this.buttonsX; j++)
                    {
                        if (index < values.Length)
                        {
                            this.textArrays[j, i] = values[index];
                            //Console.WriteLine(values[index]);
                        }

                        index++;
                    }
                }

                updateText();
            }

            private void updateText()
            {
                reloadImages();

                for (int i = 0; i < this.buttonsX; i++)
                {
                    for (int j = 0; j < this.buttonsY; j++)
                    {
                        Bitmap btmp = (Bitmap)this.buttonArrays[i, j].BackgroundImage;

                        float width = this.buttonArrays[i, j].BackgroundImage.Width;
                        float height = this.buttonArrays[i, j].BackgroundImage.Height;

                        btmp.SetResolution(width/height*100, 100);
                        //Graphics temp = Graphics.FromImage(btmp);
                        btmp.SetResolution(300, 300);
                        using (Graphics graphics = Graphics.FromImage(btmp))
                        {
                            StringFormat sf = new StringFormat();
                            sf.LineAlignment = StringAlignment.Center;
                            sf.Alignment = StringAlignment.Center;
                            
                            Brush brush = (Brush) new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#707070"));

                            if (this.darkMode || selected == i*buttonsX + j)
                                brush = Brushes.White;

                            string write = this.textArrays[i, j];

                            if (!String.IsNullOrEmpty(write))
                                write = write.ToUpper();

                            //graphics.DrawString(write, new Font("Segoe UI", width/scale), brush, width/2, height/2, sf);
                            graphics.DrawString(write, new Font("Segoe UI", width / scale, FontStyle.Bold), brush, width / 2, height / 2, sf);
                            this.buttonArrays[i, j].BackgroundImage = new Bitmap(btmp);
                        }

                        btmp.Dispose();
                    }
                }
            }

            public string getSelected()
            {
                if (selected.HasValue)
                    return textArrays[selected.Value / buttonsX, selected.Value % buttonsX];
                return "null";
            }

            public void setFontScale(float scale)
            {
                this.scale = scale;
            }
        }

        public class resultModel
        {
            string modelName;
            string yearRange;
            string weight;
            string spec;
            bool isBlank;
            bool selected;
            public double weightNum;
            public int ID;

            public resultModel(string modelName, string yearRange, string weight, string spec, double weightNum, int ID)
            {
                this.modelName = modelName;
                this.yearRange = yearRange;
                this.weight = weight;
                this.spec = spec;
                this.weightNum = weightNum;
                this.ID = ID;
            }

            public resultModel()
            {
                this.isBlank = true;
                //makes blank resultsModel
            }

            public bool getIsBlank()
            {
                return isBlank;
            }

            public Bitmap GenerateImage(bool darkMode, bool selected, int index)
            {
                this.selected = selected;
                index++;
                Bitmap btmp;
                Brush brush = Brushes.White;

                if (selected && !isBlank) {
                    btmp = Resources.ModelOption_select;
                }
                else if (darkMode)
                {
                    btmp = Resources.ModelOption_dark;
                }
                else
                {
                    btmp = Resources.ModelOption_light;
                    brush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#707070"));
                }
                
                if(!isBlank)
                    using (Graphics graphics = Graphics.FromImage(btmp))
                    {
                        StringFormat sf = new StringFormat();
                        sf.LineAlignment = StringAlignment.Center;
                        sf.Alignment = StringAlignment.Center;

                        graphics.DrawString(modelName + "; " + yearRange,
                            new Font("Segoe UI", btmp.Width / 35,
                            FontStyle.Bold), brush, 30F, 30F);

                        graphics.DrawString(weight + ", " + spec,
                            new Font("Segoe UI", btmp.Width / 45,
                            FontStyle.Bold), brush, 30F, 70F);

                        graphics.DrawString(index.ToString(),
                            new Font("Segoe UI", btmp.Width / 40,
                            FontStyle.Bold), brush, 730F, 55F);
                    }

                return btmp;
            }

            public void printDetails()
            {
                Console.WriteLine(modelName + "; " + yearRange + ", selected = " + selected.ToString());
            }
        }

        public class resultModelContainer
        {
            private List<resultModel> list = new List<resultModel>();
            private System.Windows.Forms.PictureBox[] boxes;
            private int activeBeginIndex;
            public int? selectionIndex;
            bool darkMode;

            public resultModelContainer(byte displayedModelsCount, Form1 form)
            {
                //initialise elements for drawing
                buildPictureBoxes(displayedModelsCount, form);
            }

            public void buildPictureBoxes(byte displayedModelsCount, Form1 form)
            {
                boxes = new System.Windows.Forms.PictureBox[displayedModelsCount];
                for (byte i = 0; i < displayedModelsCount; i++)
                {
                    boxes[i] = new System.Windows.Forms.PictureBox();
                    boxes[i].Cursor = System.Windows.Forms.Cursors.Hand;
                    boxes[i].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                    boxes[i].Size = new System.Drawing.Size(650, 120);

                    byte index = i;

                    boxes[i].Click += (sender, EventArgs) => {
                        selectInRange(index, form);
                    };

                    form.Controls.Add(boxes[i]);
                }
            }

            public double getSelectionWeight()
            {
                if (selectionIndex.HasValue)
                {
                    return list[selectionIndex.Value].weightNum;
                }
                else
                {
                    return -1;
                }
            }

            public int getSelectionID()
            {
                if (selectionIndex.HasValue)
                {
                    return list[selectionIndex.Value].ID;
                }
                else
                {
                    return -1;
                }
            }

            public void updateList(string searchText, Form1 form)
            {
                activeBeginIndex = 0;

                selectionIndex = null;

                //Console.WriteLine("started updateList");

                SQLiteCommand comm = form.connection.CreateCommand();

                //get values from query using searchText
                /*
                 * add new row rank
                 * increment rank if matching year
                 * increase rank by 2 if match search split by [ ,-.]
                 * ID integer, make, model, yearStart integer, yearEnd integer, bodyStyle, weight integer, specDesc
                 */
                //write to list<resultModel>

                comm.CommandText = "ALTER TABLE carsA DROP COLUMN rank";
                comm.ExecuteNonQuery();

                comm.CommandText = "ALTER TABLE carsA ADD rank int";
                comm.ExecuteNonQuery();

                comm.CommandText = "UPDATE carsA SET rank = 1";
                comm.ExecuteNonQuery();

                string[] splitSearch = Regex.Split(searchText, "[ ,-.&]");

                foreach (string splitVal in splitSearch)
                {
                    string val = splitVal.Trim(new char[] { ' ', '\n', '\r' });

                    try
                    {
                        comm.CommandText = "UPDATE carsA SET rank = rank + 3 WHERE (model LIKE @val or specDesc LIKE @val);";
                        //comm.CommandType = CommandType.Text;
                        comm.Parameters.AddWithValue("@val", val + "%");

                        //Console.WriteLine(comm.CommandText);
                        comm.ExecuteNonQuery();

                        comm.CommandText = "UPDATE carsA SET rank = rank + 1 WHERE (model LIKE @val or specDesc LIKE @val);";
                        comm.Parameters.AddWithValue("@val", "%" + val + "%");
                        //Console.WriteLine(comm.CommandText);

                        comm.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                    }
                    
                    Match match = Regex.Match(val, "(?<=[^0-9]|^)[12][01289][0-9][0-9](?=[^0-9]|$)");

                    if (match.Success)
                    {
                        comm.CommandText = "UPDATE carsA SET rank = rank + 4 WHERE (yearStart <= @year and yearEnd >= @year);";
                        comm.Parameters.AddWithValue("@year", match.Value);
                        comm.ExecuteNonQuery();

                        comm.CommandText = "UPDATE carsA SET rank = rank + 4 WHERE (yearStart <= @year and yearEnd = 'Present');";
                        comm.Parameters.AddWithValue("@year", match.Value);
                        comm.ExecuteNonQuery();
                    }
                }

                //Console.WriteLine("finished updateList queries");

                string bodyType = form.inputScreen1.getSelectedBody();
                comm.CommandText = "SELECT * FROM carsA WHERE (make LIKE @make and bodyStyle LIKE @bodyType and weight <> 'null') ORDER BY rank DESC";

                if (bodyType.ToUpper() == "UNKNOWN")
                    comm.CommandText = "SELECT * FROM carsA WHERE (make LIKE @make and weight <> 'null') ORDER BY rank DESC";
                
                comm.Parameters.AddWithValue("@make", form.inputScreen1.getSelectedMake());
                comm.Parameters.AddWithValue("@bodyType", form.inputScreen1.getSelectedBody());

                SQLiteDataReader rdr;

                try
                {
                    //Console.WriteLine(comm.CommandText);
                    rdr = comm.ExecuteReader();
                }
                catch (Exception)
                {
                    return;
                }
                

                list = new List<resultModel>();

                for (int i = 0; i < 50; i++)
                {
                    if (rdr.Read())
                    {
                        try
                        {
                            string model = rdr.GetString(1) + " " + rdr.GetString(2);

                            string yearEnd = "";

                            try
                            {
                                yearEnd = rdr.GetString(4);
                            } catch (Exception) { }

                            try
                            {
                                yearEnd = rdr.GetInt16(4).ToString();
                            } catch (Exception) { }

                            string yearRange = rdr.GetInt16(3) + "-" + yearEnd;
                            string weight = rdr.GetInt16(6).ToString() + "kg";
                            string specs = rdr.GetString(7);

                            if (yearEnd != "")
                                list.Add(new resultModel(model, yearRange, weight, specs, rdr.GetInt16(6), rdr.GetInt16(0)));

                            //Console.WriteLine(model + yearRange);
                        }
                        catch (Exception e)
                        {
                            //do nothing
                        }
                    }
                }

                //Console.WriteLine("finished updateList");

                draw();
            }

            public void draw(bool darkMode)
            {
                this.darkMode = darkMode;

                draw();
            }

            private void draw()
            {
                //draw each element in boxes (mostly just setting images in boxes)

                //Console.WriteLine("Drawing");

                for (byte i = 0; i < boxes.Length; i++)
                {
                    if (activeBeginIndex + i < list.Count)
                    {
                        boxes[i].BackgroundImage = list[activeBeginIndex + i]
                            .GenerateImage(this.darkMode, activeBeginIndex + i == selectionIndex, activeBeginIndex + i);
                        //Console.WriteLine("activeBeginIndex = " + activeBeginIndex + ", selectionIndex = " + selectionIndex);
                        list[activeBeginIndex + i].printDetails();
                    }
                    else
                    {
                        boxes[i].BackgroundImage = (new resultModel()).GenerateImage(this.darkMode, false, activeBeginIndex + i + 1);
                    }
                    
                }
            }

            public void move(int xPos, int yPos)
            {
                int yOff = 0;
                foreach (System.Windows.Forms.PictureBox box in boxes)
                {
                    box.Location = new System.Drawing.Point(xPos, yPos + yOff);

                    yOff += 100; //this will probably need changing
                }
            }

            public void moveRange(bool? increment)
            {
                int changeSpeed = 2;

                if (increment.HasValue)
                {
                    if (increment.Value)
                    {
                        activeBeginIndex += changeSpeed;
                    }
                    else
                    {
                        activeBeginIndex -= changeSpeed;
                    }
                }
                else
                {
                    activeBeginIndex = 0;
                }

                activeBeginIndex = Math.Min(activeBeginIndex, list.Count - 1);
                activeBeginIndex = Math.Max(activeBeginIndex, 0);

                draw();
            }

            public void selectInRange(byte selected, Form1 form)
            {
                form.inputScreen2.modelBox.inputText.Text = "";
                form.inputScreen2.yearBox.inputText.Text = "";
                form.inputScreen2.weightBox.inputText.Text = "";

                form.inputScreen2.updateSubmit();

                if (selectionIndex.HasValue)
                {
                    if (selectionIndex != activeBeginIndex + selected)
                    {
                        if (selectionIndex.HasValue)
                            if (!list[selectionIndex.Value].getIsBlank())
                                selectionIndex = activeBeginIndex + selected;
                    }
                    else
                    {
                        selectionIndex = null;
                    }
                }
                else
                {
                    selectionIndex = activeBeginIndex + selected;
                }

                form.inputScreen2.updateSubmit();

                draw();
            }

            public void clearSelection(Form1 form)
            {
                selectionIndex = null;

                form.inputScreen2.updateSubmit();

                draw();
            }

            public void setVisibility(bool visible)
            {
                for (int i = 0; i < boxes.Length; i++)
                {
                    boxes[i].Visible = visible;
                }
            }
        }

        public class resultOwner
        {
            DateTime regDate;
            decimal regCost;
            string owner;
            string yearRange;
            string makeModel;
            public int dbID;

            bool isBlank;

            public resultOwner(DateTime regDate, decimal regCost, string owner, string yearRange, string makeModel, int dbID)
            {
                this.dbID = dbID;
                this.regDate = regDate;
                this.regCost = regCost;
                this.owner = owner;
                this.yearRange = yearRange;
                this.makeModel = makeModel;
            }

            public resultOwner()
            {
                this.isBlank = true;
                //makes blank resultsModel
            }

            public bool getIsBlank()
            {
                return isBlank;
            }

            public Bitmap GenerateImage(bool darkMode, int index)
            {
                Bitmap btmp;
                Brush brush = Brushes.White;

                if (darkMode)
                {
                    btmp = Resources.BigBoxHist_dark;
                }
                else
                {
                    btmp = Resources.BigBoxHist_liught;
                    brush = new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#707070"));
                }

                if (!isBlank)
                    using (Graphics graphics = Graphics.FromImage(btmp))
                    {
                        StringFormat sf = new StringFormat();
                        sf.LineAlignment = StringAlignment.Center;
                        sf.Alignment = StringAlignment.Center;

                        graphics.DrawString(regDate.ToString("dd MMM yyyy") + " - " + owner,
                            new Font("Segoe UI", btmp.Width / 50,
                            FontStyle.Bold), brush, 140F, 25F);

                        graphics.DrawString(regCost.ToString("C") + ", " + yearRange + " " + makeModel,
                            new Font("Segoe UI", btmp.Width / 65,
                            FontStyle.Bold), brush, 140F, 75F);

                        graphics.DrawString(index.ToString(),
                            new Font("Segoe UI", btmp.Width / 55,
                            FontStyle.Bold), brush, 60F, 55F);
                    }

                return btmp;
            }

            public void printDetails()
            {
                //Console.WriteLine(modelName + "; " + yearRange);
            }
        }

        public class resultOwnerContainer
        {
            private List<resultOwner> list = new List<resultOwner>();
            private List<System.Windows.Forms.PictureBox> boxes = new List<System.Windows.Forms.PictureBox>();
            private List<System.Windows.Forms.PictureBox> DelButtons = new List<System.Windows.Forms.PictureBox>();
            private int activeBeginIndex;
            bool darkMode;
            bool visibility = false;
            byte visible = 3;

            public resultOwnerContainer(byte displayedModelsCount, Form1 form)
            {
                //initialise elements for drawing

                for (byte i = 0; i < displayedModelsCount; i++)
                {
                    DelButtons.Add(new System.Windows.Forms.PictureBox());
                    DelButtons[i].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                    DelButtons[i].Size = new System.Drawing.Size(45, 49);
                    DelButtons[i].Cursor = System.Windows.Forms.Cursors.Hand;
                    int index = i;

                    DelButtons[i].Click += (obj, EventArgs) => {
                        deleteEntry(index, form);
                    };

                    boxes.Add(new System.Windows.Forms.PictureBox());
                    boxes[i].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                    boxes[i].Size = new System.Drawing.Size(1140, 120);

                    form.Controls.Add(DelButtons[i]);
                    form.Controls.Add(boxes[i]);
                }
            }

            public void changePictureBoxVisibility(byte count)
            {
                this.visible = count;

                draw();

                setVisibility(this.visibility);
            }

            public void updateList(Form1 form)
            {
                activeBeginIndex = 0;

                //Console.WriteLine("started updateList");

                SQLiteCommand comm = form.connection.CreateCommand();

                comm.CommandText = "SELECT * FROM HistoryA ORDER BY ownerName ASC";

                SQLiteDataReader rdr = comm.ExecuteReader();

                list = new List<resultOwner>();

                for (int i = 0; i < 50; i++)
                {
                    if (rdr.Read())
                    {
                        try
                        {
                            SQLiteCommand comm2 = form.connection.CreateCommand();

                            string textCMD = "SELECT * FROM carsA WHERE ID = " + rdr.GetInt16(1).ToString() + "";

                            //Console.WriteLine(textCMD);

                            comm2.CommandText = textCMD;

                            //Console.WriteLine("this0");

                            SQLiteDataReader rdr2 = comm2.ExecuteReader();
                            rdr2.Read();

                            //Console.WriteLine("this1");

                            string model = rdr2.GetString(1) + " " + rdr2.GetString(2);

                            //Console.WriteLine("this2");

                            string yearEnd = "";

                            try
                            {
                                yearEnd = rdr2.GetString(4);
                            }
                            catch (Exception) { }

                            try
                            {
                                yearEnd = rdr2.GetInt16(4).ToString();
                            }
                            catch (Exception) { }

                            string yearRange = rdr2.GetInt16(3) + "-" + yearEnd;

                            //DateTime regDate, decimal regCost, string owner, string yearRange, string makeModel
                            // CREATE TABLE History (ModelID int, RegDate date, OwnerName text, paidAmt decimal(2, 0));

                            //Console.WriteLine("that " + );



                            list.Add(
                                new resultOwner(DateTime.Parse(rdr.GetString(2)), rdr.GetDecimal(4), rdr.GetString(3), yearRange, model, rdr.GetInt16(0))
                            );

                            //Console.WriteLine(model + yearRange);
                        }
                        catch (Exception e)
                        {
                            //Console.WriteLine("thrown");
                        }
                    }
                }

                //Console.WriteLine("finished updateList");

                draw();
            }

            public void deleteEntry(int dbID, Form1 form)
            {
                SQLiteCommand comm = form.connection.CreateCommand();

                int ID = activeBeginIndex + dbID;

                comm.CommandText = "DELETE FROM HistoryA WHERE ID = " + list[ID].dbID.ToString();

                Console.WriteLine(comm.CommandText);
                Console.WriteLine(dbID);

                comm.ExecuteNonQuery();

                updateList(form);
            } 

            public void draw(bool darkMode)
            {
                this.darkMode = darkMode;

                draw();
            }

            private void draw()
            {
                //draw each element in boxes (mostly just setting images in boxes)

                //Console.WriteLine("Drawing");

                for (byte i = 0; i < boxes.Count; i++)
                {
                    if (activeBeginIndex + i < list.Count)
                    {
                        boxes[i].BackgroundImage = list[activeBeginIndex + i]
                            .GenerateImage(this.darkMode, activeBeginIndex + i + 1);
                        //Console.WriteLine("activeBeginIndex = " + activeBeginIndex);
                        list[activeBeginIndex + i].printDetails();
                    }
                    else
                    {
                        boxes[i].BackgroundImage = (new resultOwner()).GenerateImage(this.darkMode, activeBeginIndex + i + 1);
                    }

                    if (this.darkMode)
                    {
                        DelButtons[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#2B344E");
                        DelButtons[i].BackgroundImage = Resources.trash_dark;
                    }
                    else
                    {
                        DelButtons[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                        DelButtons[i].BackgroundImage = Resources.trash_light;
                    }
                }

                setVisibility(this.visibility);
            }

            public void move(int xPos, int yPos)
            {
                int yOff = 0;
                int index = 0;
                foreach (System.Windows.Forms.PictureBox box in boxes)
                {
                    box.Location = new System.Drawing.Point(xPos, yPos + yOff);
                    DelButtons[index].Location = new System.Drawing.Point(xPos + 790, yPos + yOff + 25);
                    if (DelButtons[index].BackgroundImage != null)
                    {
                        DelButtons[index].Visible = true;
                    }
                    else
                    {
                        DelButtons[index].Visible = false;
                    }
                        
                    yOff += 100; //this will probably need changing
                    index++;
                }
            }

            public void moveRange(bool? increment)
            {
                int changeSpeed = 3;

                if (increment.HasValue)
                {
                    if (increment.Value)
                    {
                        activeBeginIndex += changeSpeed;
                    }
                    else
                    {
                        activeBeginIndex -= changeSpeed;
                    }
                }
                else
                {
                    activeBeginIndex = 0;
                }

                activeBeginIndex = Math.Min(activeBeginIndex, list.Count - 1);
                activeBeginIndex = Math.Max(activeBeginIndex, 0);

                draw();
            }

            public void setVisibility(bool visible)
            {
                //Console.WriteLine(this.visible);

                this.visibility = visible;

                Console.WriteLine(this.visible);

                for (int i = 0; i < boxes.Count; i++)
                {
                    if (i < this.visible)
                    {
                        if (list.Count > activeBeginIndex + i)
                        {
                            DelButtons[i].Visible = this.visibility;
                        }
                        else
                        {
                            DelButtons[i].Visible = false;
                        }

                        boxes[i].Visible = visible;
                    }
                    else
                    {
                        DelButtons[i].Visible = false;
                        boxes[i].Visible = false;
                    }
                }
            }
        }
    }
}