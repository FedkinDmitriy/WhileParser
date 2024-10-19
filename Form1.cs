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


        /// <summary>
        /// хеш-таблица для идентификаторов и описания
        /// </summary>
        private Token[] _hashTable = new Token[1000];

        public Form1()
        {
            InitializeComponent();
            // Установка максимального значения для numericUpDown1
            numericUpDown1.Maximum = _hashTable.Length - 1;
           // textBoxUserString.Text = "while(as22+ee4:=true){Xs22+e44;}";
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            ClearHashTableAndListView();

            string temp = textBoxUserString.Text.Trim();
            
            bool isWhile = Regex.IsMatch(temp, patternIsWhile);

            if(isWhile)
            {
                textBoxUserString.BackColor = Color.LimeGreen;

                MatchCollection matchesVar = Regex.Matches(temp, patternVar);
                MatchCollection matchesConst = Regex.Matches(temp, patternConst);

                CreateHashTable(matchesVar, matchesConst);

               // listViewMain.Items.Clear();

                

                if(_hashTable.Length > 0)
                {
                    for(int i=0; i < _hashTable.Length; i++)
                    {
                        if (_hashTable[i] != null)
                        {
                            ListViewItem item = new ListViewItem((i+1).ToString());
                            item.SubItems.Add(_hashTable[i].Name);
                            item.SubItems.Add(_hashTable[i].Description);
                            listViewMain.Items.Add(item);
                        }
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


        /// <summary>
        /// Метод для очистки массива хеш-таблицы и ListView
        /// </summary>
        private void ClearHashTableAndListView()
        {            
            listViewMain.Items.Clear();
            _hashTable = new Token[1000]; // Заново создаем пустой массив того же размера
        }

        private void CreateHashTable(MatchCollection matchesVar, MatchCollection matchesConst)
        {          
            foreach (Match match in matchesConst)
            {
                string description = "константа символов";
                string name = match.Value;
                //вычисляем индекс для вставки
                int index = (match.Value.GetHashCode() == int.MinValue) ? 0 : Math.Abs(match.Value.GetHashCode()) % _hashTable.Length;
                Insert(index,name,description);
            }
            foreach (Match match in matchesVar)
            {
                string description = "целочисленная переменная";
                string name = match.Value;
                //вычисляем индекс для вставки
                int index = (match.Value.GetHashCode() == int.MinValue) ? 0 : Math.Abs(match.Value.GetHashCode()) % _hashTable.Length;
                Insert(index,name,description);
            }
        }


        Token CheckByAddress(int index)
        {
            if (_hashTable[index] != null)
            {
                return _hashTable[index];
            }
            return null;
        }

        /// <summary>
        /// вставка в таблицу
        /// </summary>
        /// <param name="index">индекс вставки</param>
        /// <param name="name">название идентификатора</param>
        /// <param name="description">описание идентификатора</param>
        /// <returns></returns>
        bool Insert(int index, string name, string description)
        {
            if (_hashTable[index] == null)
            {
                _hashTable[index] = new Token(name,description);
                return true;
            }
            // Линейное пробирование при коллизии (один из методов свободной адресации)
            int startIndex = index;
            while (_hashTable[index] != null)
            { 
                index = (index + 1) % _hashTable.Length; // Увеличиваем индекс на 1 с возвратом к началу при выходе за пределы массива
                if (index == startIndex)
                {
                    return false; // Таблица заполнена, вставка не удалась
                }
            }
            // Вставляем элемент в первую свободную ячейку
            _hashTable[index] = new Token(name, description);
            return true;
        }

        // обаботка проверки по адресу
        private void button1_Click(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(numericUpDown1.Value);
            Token token = CheckByAddress(index - 1);
            if(token != null)
            {
                MessageBox.Show(token.ToString());
            }
            else
            {
                MessageBox.Show("NotFound","Error 404",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
