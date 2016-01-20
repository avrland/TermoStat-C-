using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace TermoStatCS
{

    public partial class Form1 : Form
    {
        readfile plik;
        public string strfilename;
        public Form1()
        {
            InitializeComponent();
        }
        public string LabelText{
            get { return label2.Text; }
            set { label2.Text = value;}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Plik logów|*.log";
            openFileDialog1.Title = "Wskaż ścieżkę do zapisania logów.";
            openFileDialog1.ShowDialog();
            strfilename = openFileDialog1.InitialDirectory + openFileDialog1.FileName;
            textBox1.Text = strfilename;
            button1.Enabled = true; //odblokuj przycisk do pobrania logow;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            plik = new readfile(strfilename);
            label2.Text = plik.otworz_plik();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // listBox1.Items.Add(plik.odczytaj());
            while (true)
            {
                wynik ODCZYT = plik.odczytaj();
                if (ODCZYT.rok == 0) break;
                //if (ODCZYT.rok == 0) break;
                DateTime czas = new DateTime(ODCZYT.rok, ODCZYT.miesiac,
                ODCZYT.dzien, ODCZYT.godzina, ODCZYT.minuta, 00);
                chart1.Series["Series1"].Points.AddXY(czas, ODCZYT.temperatura);
            }
        }
    }
}
