using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Rego_Assignment
{
    public partial class ErrorBox : Form
    {
        public ErrorBox(bool darkMode)
        {
            InitializeComponent();
            

            if (darkMode)
            {
                this.BackColor = System.Drawing.ColorTranslator.FromHtml("#182039");
                this.errorMessage.BackColor = System.Drawing.ColorTranslator.FromHtml("#182039");
                this.errorMessage.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            }
            else
            {
                this.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                this.errorMessage.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                this.errorMessage.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6e6e6e");
            }

            //resize();
        }

        private void ErrorBox_Resize(object sender, EventArgs e)
        {
            resize();
        }

        private void resize()
        {
            this.errorMessage.Location = new System.Drawing.Point(this.Width / 2 - this.errorMessage.Width / 2 - 5, 20);
            this.okButton.Location = new System.Drawing.Point(this.Width / 2 - 70, this.Height - 120);
        }

        public void setText(List<String> incorrectValueMessages, List<String> incorrectSelections, List<String> errors)
        {
            //Console.WriteLine("THis did actually happen");
            string text = "";
            if (incorrectValueMessages.Count > 0)
            {
                if (Regex.Match(incorrectValueMessages[0], "^[aeioAEIO]").Success)
                {
                    text += "Please enter an ";
                }
                else
                {
                    text += "Please enter a ";
                }
                
                for (int i = 0; i < incorrectValueMessages.Count; i++)
                {
                    text += incorrectValueMessages[i];

                    if (i < incorrectValueMessages.Count - 2)
                        text += ", ";

                    if (i == incorrectValueMessages.Count - 2)
                        text += " and ";
                }
            }

            if (text != "") text += "\n";

            if (incorrectSelections.Count > 0)
            {
                if (Regex.Match(incorrectSelections[0], "^[aeioAEIO]").Success)
                {
                    text += "Please select an ";
                }
                else
                {
                    text += "Please select a ";
                }
                for (int i = 0; i < incorrectSelections.Count; i++)
                {
                    text += incorrectSelections[i];

                    if (i < incorrectSelections.Count - 2)
                        text += ", ";

                    if (i == incorrectSelections.Count - 2)
                        text += " and ";
                }
            }

            if (text != "") text += "\n";

            for (int i = 0; i < errors.Count; i++)
            {
                text += errors[i] + "\n";
            }

            this.errorMessage.Text = text;

            //this.MinimumSize = new System.Drawing.Size(500, errorMessage.Height + 50);
            //this.MaximumSize = new System.Drawing.Size(600, errorMessage.Height + 150);
        }
    }
}
