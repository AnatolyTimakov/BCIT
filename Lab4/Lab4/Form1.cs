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

namespace Lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> list = new List<string>();
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
            if (!string.IsNullOrWhiteSpace(word) && list.Count > 0)     //Если слово для поиска не пусто
            {
                string wordUpper = word.ToUpper();   //Слово для поиска в верхнем регистре
                Stopwatch t = new Stopwatch();
                t.Start();
                listBox2.BeginUpdate();
                foreach (string str in list)
                {
                    if (str.ToUpper().Contains(wordUpper))
                    {
                        listBox2.Items.Add(str);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
