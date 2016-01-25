using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermoStatCS
{
    public struct wynik
    {
        internal int rok;
        internal int miesiac;
        internal int dzien;
        internal int godzina;
        internal int minuta;
        internal double temperatura;
    };
    public struct wyniktxt
    {
        internal string rok;
        internal string miesiac;
        internal string dzien;
        internal string godzina;
        internal string minuta;
        internal string temperatura;
    };

    public class readfile{

        private string FILE_NAME;
        private StreamReader sr;
        //Form1 formatka = new Form1();        
        public readfile(string test){
            FILE_NAME = test;
        }
        public string otworz_plik()
        {
            if (!File.Exists(FILE_NAME)){
                return "Brak pliku!";
            }
            else {
                sr = new StreamReader(FILE_NAME);
                return "Plik załadowano!";
            }
        }
        public void zamknij_plik()
        {
            sr.Close();
        }
        public wynik odczytaj(){
            wynik ODCZYT = new wynik();
            wyniktxt ODCZYTtxt = new wyniktxt();
            ODCZYT.rok = 0;
            ODCZYT.miesiac = 0;
            ODCZYT.dzien = 0;
            ODCZYT.godzina = 0;
            ODCZYT.minuta = 0;
            ODCZYT.temperatura = 0;
            string linia;
            if ((linia = sr.ReadLine()) == null) return ODCZYT;
            int dlugosc = linia.Length;
            for(int i=0; i< dlugosc; i++)
            {
                if (i < 4) ODCZYTtxt.rok += linia[i];
                if (i < 6 && i > 3) ODCZYTtxt.miesiac += linia[i];
                if (i < 8 && i > 5) ODCZYTtxt.dzien += linia[i];
                if (i < 10 && i > 7) ODCZYTtxt.godzina += linia[i];
                if (i < 12 && i > 9) ODCZYTtxt.minuta += linia[i];
                if( i > 11) ODCZYTtxt.temperatura += linia[i];
            }
            ODCZYT.rok = Int32.Parse(ODCZYTtxt.rok);
            ODCZYT.miesiac = Int32.Parse(ODCZYTtxt.miesiac);
            ODCZYT.dzien = Int32.Parse(ODCZYTtxt.dzien);
            ODCZYT.godzina = Int32.Parse(ODCZYTtxt.godzina);
            ODCZYT.minuta = Int32.Parse(ODCZYTtxt.minuta);
            ODCZYT.temperatura = Double.Parse(ODCZYTtxt.temperatura, System.Globalization.CultureInfo.InvariantCulture);
            return ODCZYT;
        }
      }

    
}
