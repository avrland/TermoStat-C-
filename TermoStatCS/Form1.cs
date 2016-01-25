using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
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

        //funkcja wybierająca ścieżkę do pobieranego pliku
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Plik logów|*.log";
            openFileDialog1.Title = "Wskaż ścieżkę do zapisania logów.";
            openFileDialog1.ShowDialog();
            strfilename = openFileDialog1.InitialDirectory + openFileDialog1.FileName;
            textBox1.Text = strfilename;
            button2.Enabled = true; //odblokuj przycisk do pobrania logow;
        }

        //funkcja pobierająca plik z serwera
        private void button2_Click(object sender, EventArgs e)
        {
            using (WebClient wc = new WebClient())
            {
                progressBar1.Visible = true;
                wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                wc.DownloadFileAsync(new System.Uri(textBox2.Text),
                @textBox1.Text);
            }
        }

        //event procesu pobierania
        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        //event zakończonego pobierania
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            progressBar1.Visible = false; //wygaś pasek postępu
            label2.Text = "Pobrano logi."; //powiadom, że pobrano logi
            label2.ForeColor = Color.Green;
            button3.Enabled = true;
        }

        //wybór zakresu wyświetlanego wykresu (:d)
        private void button5_Click(object sender, EventArgs e)
        {

        }

        //wyświetlanie wykresu
        private void button3_Click(object sender, EventArgs e)
        {
            plik = new readfile(strfilename);
            label2.Text = plik.otworz_plik();
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
            plik.zamknij_plik();
        }

        //funkcja czyszczaca wykres
        private void button4_Click(object sender, EventArgs e)
        {
            label2.Text = "Wyczyszczono wykres.";
            label2.ForeColor = Color.Blue;
            chart1.Series["Series1"].Points.Clear();
        }
    }
}
