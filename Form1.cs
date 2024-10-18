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

namespace LabWhile_6
{
    public partial class Form1 : Form
    {
        readonly string patternVar = @"[a-zA-Z]+[0-9]+";
        readonly string patternConst = @"(true)";
        readonly string patternIsWhile = @"^while\s*\(\s*([a-zA-Z]+[0-9]+)\s*([\+\-\*\/])\s*([a-zA-Z]+[0-9]+)\s*:=\s*(true)\s*\)\s*\{((\s*([a-zA-Z]+[0-9]+)\s*([\+\-\*\/])\s*([a-zA-Z]+[0-9]+)\s*)*;)*\s*\}$";

        private List<string> Tokens { get; set; }

        public Form1()
        {
            InitializeComponent();
            //textBoxUserString.Text = "while(as22+ee4:=true){as22+ee4;}";
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            string temp = textBoxUserString.Text.Trim();
            
            bool isWhile = Regex.IsMatch(temp, patternIsWhile);

            if(isWhile)
            {
                textBoxUserString.BackColor = Color.LimeGreen;

                MatchCollection matchesVar = Regex.Matches(temp, patternVar);
                MatchCollection matchesConst = Regex.Matches(temp, patternConst);

                Result(matchesVar, matchesConst);

                listViewMain.Items.Clear();
                
                int addressCounter = 1; // Счётчик для адреса токенов

                if (Tokens != null)
                {
                    foreach (var token in Tokens)
                    {
                        ListViewItem item = new ListViewItem(addressCounter.ToString()); // columnAddress
                        item.SubItems.Add(token); // columnName
                        if (addressCounter == 1)
                        {
                            item.SubItems.Add("константа символов"); // columnDescription
                        }
                        item.SubItems.Add("целочисленная переменная"); // columnDescription
                        listViewMain.Items.Add(item);
                        addressCounter++;
                    }
                }
            }
            else
            {
                textBoxUserString.BackColor = Color.Red;
                listViewMain.Items.Clear();
                MessageBox.Show("Uncorrect string","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void Result(MatchCollection matchesVar, MatchCollection matchesConst)
        {
            List<string> result = new List<string>();

            foreach (Match match in matchesConst)
            {
                result.Add(match.Value);
            }
            foreach (Match match in matchesVar)
            {
                result.Add(match.Value);
            }
            Tokens = result;
        }


    }
}
