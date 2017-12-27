using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> list = new List<string>();

        public static int Distance(string s1, string s2)
        {
            if ((s1 == null) || (s2 == null)) return -1;
            if ((s1.Length == 0) && (s2.Length == 0)) return 0;
            if (s1.Length == 0) return s2.Length; //if one str is empty return length of another
            if (s2.Length == 0) return s1.Length;

            string str1 = s1.ToUpper();
            string str2 = s2.ToUpper();

            int[,] matrix = new int[str1.Length + 1, str2.Length + 1]; //+1 for(int i=1 ...)

            //initialization of first row and column 
            for (int i = 0; i <= str1.Length; i++) matrix[i, 0] = i;
            for (int j = 0; j <= str2.Length; j++) matrix[0, j] = j;

            //counting Levenshtein distance
            for (int i = 1; i <= str1.Length; i++)
            {
                for (int j = 1; j <= str2.Length; j++)
                {
                    int equalChar = (str1.Substring(i - 1, 1) == str2.Substring(j - 1, 1) ? 0 : 1); //m(s1[i],s2[j])
                    int ins = matrix[i, j - 1] + 1; //insert
                    int del = matrix[i - 1, j] + 1; //delete
                    int subst = matrix[i - 1, j - 1] + equalChar; //substitute

                    matrix[i, j] = Math.Min(Math.Min(ins, del), subst);  //matrix element count as min of 3 variants

                    //additional part for replacing 2 one by one elements 
                    if ((i > 1) && (j > 1) && (str1.Substring(i - 1, 1) == str2.Substring(j - 2, 1)) && (str1.Substring(i - 2, 1) == str2.Substring(j - 1, 1)))
                    {
                        matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + equalChar);
                    }
                }
            }
            return matrix[str1.Length, str2.Length]; //result is in the lower right cell
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            textBox2.Clear();
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "текстовые файлы|*.txt";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                Stopwatch t = new Stopwatch();
                t.Start();
                string text = File.ReadAllText(fd.FileName);         //Чтение файла в виде строки
                char[] separators = new char[] { ' ', '.', ',', '!', '?', '/', '\t', '\n', '—', ')', '(' };           //Разделительные символы для чтения из файла
                string[] textArray = text.Split(separators);
                listBox1.BeginUpdate();
                foreach (string strTemp in textArray)
                {
                    string str = strTemp.Trim();                   //Удаление пробелов в начале и конце строки
                    if (!list.Contains(str) && str.Length != 0) //Добавление строки в список, если строка не содержится в списке
                    {
                        list.Add(str);
                        listBox1.Items.Add(str);
                    }

                }
                listBox1.EndUpdate();
                t.Stop();
                this.textBox2.Text = t.Elapsed.ToString();
                this.textBox3.Text = list.Count.ToString();
            }
            else
            {
                MessageBox.Show("Необходимо выбрать файл");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string word = this.textBox1.Text.Trim();        //Слово для поиска
            string maxD = this.textBox5.Text.Trim();
            if (!string.IsNullOrWhiteSpace(word) && list.Count > 0)     //Если слово для поиска не пусто
            {
                string wordUpper = word.ToUpper();   //Слово для поиска в верхнем регистре
                Stopwatch t = new Stopwatch();
                t.Start();
                listBox2.BeginUpdate();
                foreach (string str in list)
                {
                    int currentWordDist = Distance(str, word);
                    if (currentWordDist <= int.Parse(maxD))
                    {
                        listBox2.Items.Add(str + " (distance " + currentWordDist + ")");
                    }
                }
                if (listBox2.Items.Count == 0)
                {
                    MessageBox.Show("Искомое слово не найдено!");
                }
                listBox2.EndUpdate();
                t.Stop();
                this.textBox4.Text = t.Elapsed.ToString();
                listBox1.SelectedIndex = listBox1.FindStringExact(textBox1.Text);
            }
            else
            {
                MessageBox.Show("Необходимо выбрать файл и ввести слово для поиска");
            }
        }
    }
}
