using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Formats.Asn1.AsnWriter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Snake
{
    public partial class Form2 : Form
    {
        private string userDataFile = "userdata.txt"; // Название файла для сохранения данных
        private int score;

       

        public Form2(int score)
        {
            InitializeComponent();
            this.score = score; // Сохраните значение счета, переданное в качестве параметра, в переменной класса
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = richTextBox1.Text;
            int score2 = score; // Изначально устанавливаем набранные очки пользователя в 0

            string text = richTextBox1.Text;
            string text2 = Convert.ToString(score2);
            MessageBox.Show(text);
            SaveFileDialog open = new SaveFileDialog();

            // открываем окно сохранения
            open.ShowDialog();

            // присваниваем строке путь из открытого нами окна
            string path = open.FileName;

            try
            {
                // создаем файл используя конструкцию using

                using (FileStream fs = File.Create(path))
                {

                    // создаем переменную типа массива байтов
                    // и присваиваем ей метод перевода текста в байты
                    byte[] info = new UTF8Encoding(true).GetBytes(text);
                    byte[] info2 = new UTF8Encoding(true).GetBytes(text2);
                    // производим запись байтов в файл
                    fs.Write(info, 0, info.Length);
                    fs.Write(info2, 0, info2.Length);


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }

            // Отображаем данные пользователя на форме
            UserData data = new UserData(username, score2);
            lblUserData.DataBindings.Clear();
            lblUserData.DataBindings.Add("Text", data, "Info");




        }

        private void UpdateRecord(int score)
        {
            int currentRecord = GetRecord();
            if (score > currentRecord)
            {
                // Обновляем значение рекорда
                using (StreamWriter sw = new StreamWriter("record.txt"))
                {
                    sw.Write(score);
                }
            }
        }

        private int GetRecord()
        {
            int record = 0;
            // Проверяем наличие файла рекорда
            if (File.Exists("record.txt"))
            {
                // Считываем значение рекорда из файла
                string recordText = File.ReadAllText("record.txt");
                int.TryParse(recordText, out record);
            }
            return record;
        }

        public class UserData
        {
            public string Username { get; set; }
            public int Score { get; set; }
            public string Info => $"Имя пользователя: {Username}\nНабранные очки: {Score}";

            public UserData(string username, int score)
            {
                Username = username;
                Score = score;
            }


        }




            
    }

}
