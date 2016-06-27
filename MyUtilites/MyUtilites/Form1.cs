using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyUtilites
{
    public partial class MainForm : Form
    {
        int count = 0;
        Random rnd; // класс по рандомному числу
        char[] special_chars = new char[] {'%', '*', ')', '?', '#', '$','^', '&', '~' };
        Dictionary<string, double> metrica;

        public MainForm()
        {
            InitializeComponent();
            rnd = new Random();
            metrica = new Dictionary<string, double>();
            metrica.Add("mm",1);
            metrica.Add("cm", 10);
            metrica.Add("dm", 100);
            metrica.Add("m", 1000);
            metrica.Add("km", 1000000);
            metrica.Add("mile", 1609344);
            
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа мои утилиты, содержит ряд небольших программ, которые могут пригодиться в жизни. \nАвтор Аквенов Ю.В.","О программе");
        }

        private void btnIncrement_Click(object sender, EventArgs e)
        {
            count++;
            lblCount.Text = count.ToString();
        }

        private void btnDecriment_Click(object sender, EventArgs e)
        {
            count--;
            lblCount.Text = count.ToString();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            count = 0;
            lblCount.Text = Convert.ToString(count);
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            int n;
            n = rnd.Next(Convert.ToInt32(numericUpDown1.Value),Convert.ToInt32(numericUpDown2.Value)+1);
            lblRandom.Text = Convert.ToString(n);
            if (cbRandom.Checked)
            {
                int i = 0;
                while (tbRandom.Text.IndexOf(n.ToString()) != -1)
                {
                    n = rnd.Next(Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value) + 1);
                    i++;
                    if (i > 1000) break;
                }
               if(i<=1000) tbRandom.AppendText(n + "\n");

            }
            else
            { tbRandom.AppendText(n + "\n"); }
        }

        private void btnRandomClear_Click(object sender, EventArgs e)
        {
            tbRandom.Clear();
        }

        private void btnRandomCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbRandom.Text);
        }

        private void tsmiInsertDate_Click(object sender, EventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToShortDateString()+"\n");
        }

        private void tsmiInsertTime_Click(object sender, EventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToShortTimeString() + "\n");

        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            try
            {
                rtbNotepad.SaveFile("notepad.txt");
            }
            catch
            {
                MessageBox.Show("Ошибка при сохранении");
            }
        }
        void loadNotepad()
        {
            try
            {
                rtbNotepad.LoadFile("notepad.txt");
            }
            catch
            {
                MessageBox.Show("Ошибка при загрузке");
            }
        }
        private void tsmiLoad_Click(object sender, EventArgs e)
        {
            loadNotepad();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            loadNotepad();
            clbPasswordElements.SetItemChecked(0, true);
            clbPasswordElements.SetItemChecked(1, true);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (clbPasswordElements.CheckedItems.Count == 0) return;
            string password = "";
            for (int i=0;i<nudPassLength.Value;i++)
            {
                int n = rnd.Next(0, clbPasswordElements.CheckedItems.Count);
                string s = clbPasswordElements.CheckedItems[n].ToString();
                switch (s)
                {
                    case "Цифры": password += rnd.Next(10).ToString();
                        break;
                    case "Пропписные буквы":
                        password += Convert.ToChar(rnd.Next(65, 88));
                        break;
                    case "Строчные буквы":
                        password += Convert.ToChar(rnd.Next(97, 122));
                        break;
                    default:
                        password += special_chars[rnd.Next(special_chars.Length)]; 
                        break;

                }
                
            }
            tbPassword.Text = password;
            Clipboard.SetText(password);
        }

        private void btnConverter_Click(object sender, EventArgs e)
        {
            double m1 = metrica[cbFrom.Text];
            double m2 = metrica[cbTo.Text];
            double n = Convert.ToDouble(tbFrom.Text);
            tbTo.Text=(n*m1/m2).ToString();
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            string temp = cbFrom.Text;
            cbFrom.Text = cbTo.Text;
            cbTo.Text = temp;
        }

        private void cbMetric_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbMetric.Text)
            {
                case "длина":
                    metrica.Clear();
                    metrica.Add("mm", 1);
                    metrica.Add("cm", 10);
                    metrica.Add("dm", 100);
                    metrica.Add("m", 1000);
                    metrica.Add("km", 1000000);
                    metrica.Add("mile", 1609344);
                    cbFrom.Items.Clear();
                    cbFrom.Items.Add("mm");
                    cbFrom.Items.Add("cm");
                    cbFrom.Items.Add("dm");
                    cbFrom.Items.Add("m");
                    cbFrom.Items.Add("km");
                    cbFrom.Items.Add("mile");
                    cbTo.Items.Clear();
                    cbTo.Items.Add("mm");
                    cbTo.Items.Add("cm");
                    cbTo.Items.Add("dm");
                    cbTo.Items.Add("m");
                    cbTo.Items.Add("km");
                    cbTo.Items.Add("mile");

                    cbFrom.Text = "mm";
                    cbTo.Text = "mm";
                    break;
                case "вес":
                    metrica.Clear();
                    metrica.Add("g", 1);
                    metrica.Add("kg", 1000);
                    metrica.Add("t", 1000000);
                    metrica.Add("lb", 453.6);
                    metrica.Add("oz", 283);
                    cbFrom.Items.Clear();
                    cbFrom.Items.Add("g");
                    cbFrom.Items.Add("kg");
                    cbFrom.Items.Add("t");
                    cbFrom.Items.Add("lb");
                    cbFrom.Items.Add("oz");
                    cbTo.Items.Clear();
                    cbTo.Items.Add("g");
                    cbTo.Items.Add("kg");
                    cbTo.Items.Add("t");
                    cbTo.Items.Add("lb");
                    cbTo.Items.Add("oz");

                    cbFrom.Text = "g";
                    cbTo.Text = "g";
                    break;
                default:
                    break;
            }
        }
    }
}
