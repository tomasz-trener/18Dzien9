
using P02AplikacjaZawodnicy;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace P01AplikacjaZawodnicy
{
    public enum SposobPolaczenia
    {
        BazaDanych,
        Plik
    }
    public class ManagerZawodnikow
    {
        private string url;
        private string connString;
        private ZawodnikVM[] tablicaZawodnikow;
        private const string naglowek = "id_zawodnika;id_trenera;imie;nazwisko;kraj;data urodzenia;wzrost;waga";
        ZawodnikVM[] zawodnicyKraju;
        SposobPolaczenia sposobPolaczenia;
         

        /// <summary>
        /// Tworzy nowy menager zawodników zgodnie z wybranym sposoblem połączenia
        /// </summary>
        /// <param name="sposobPolaczenia">Baza danych lub plik</param>
        /// <param name="parametrPolaczenia">Adres polaczenia do bazy lub adres pliku</param>
        /// <exception cref="Exception"></exception>
        public ManagerZawodnikow(SposobPolaczenia sposobPolaczenia, string parametrPolaczenia)
        {
            this.sposobPolaczenia = sposobPolaczenia;
            if (sposobPolaczenia == SposobPolaczenia.Plik)
                url = parametrPolaczenia;
            else if(sposobPolaczenia == SposobPolaczenia.BazaDanych)
                    connString = parametrPolaczenia;
            else
                throw new Exception("nieznany sposób polaczenia");

          
        }

        public ZawodnikVM[] TablicaZawodnikow
        {
            get { return tablicaZawodnikow; }
        }

        //public Zawodnik[] PodajZawodnikow()
        //{
        //    return tablicaZawodnikow;
        //}


        public void WczytajZawodnikow()
        {
            if (sposobPolaczenia == SposobPolaczenia.BazaDanych)
                wczytajZawodnikowZBazy();
            else if (sposobPolaczenia == SposobPolaczenia.Plik)
                wczytajZawodnikowZpliku();
            else
                throw new Exception("nieznany sposób polaczenia");
        }

        private void wczytajZawodnikowZBazy()
        {
            ModelBazyDataContext db = new ModelBazyDataContext(connString);
            tablicaZawodnikow = db.Zawodnik
                .Select(x=>new ZawodnikVM()
                {
                    Imie = x.imie,
                    Nazwisko = x.nazwisko,
                    Kraj = x.kraj,
                    DataUrodzenia = x.data_ur,
                    Waga = x.waga == null? 0 : (int)x.waga,
                    Wzrost = x.wzrost,
                    Id_zawodnika = x.id_zawodnika,
                    Id_trenera = x.id_trenera
                })
                .ToArray();
            // transformujemy zawdonikow bazodanowych na zawonicy viewModel
            // w celu przyblizenia się do wdrożenia 
            // wzroca architektonicznego MVVM 


            // do uzupełnienia 
            // tablicaZawodnikow = ....
        }

        private void wczytajZawodnikowZpliku()
        {
            string dane = new WebClient().DownloadString(url);
            string[] separatory = { "\r\n" };
            string[] wiersze = dane.Split(separatory, StringSplitOptions.RemoveEmptyEntries);
            
            List<ZawodnikVM> zawodnicy = new List<ZawodnikVM>();

            for (int i = 1; i < wiersze.Length; i++) 
            {
                string[]komorki = wiersze[i].Split(';');
                ZawodnikVM z = new ZawodnikVM();

                z.Id_zawodnika = Convert.ToInt32(komorki[0]);

                if (!string.IsNullOrEmpty(komorki[1]))
                    z.Id_trenera = Convert.ToInt32(komorki[1]);

                z.Imie = komorki[2];
                z.Nazwisko = komorki[3];
                z.Kraj = komorki[4];
                z.DataUrodzenia = Convert.ToDateTime(komorki[5]);
                z.Wzrost = Convert.ToInt32(komorki[6]);
                z.Waga = Convert.ToInt32(komorki[7]);
                zawodnicy.Add(z);               
            }
            tablicaZawodnikow = zawodnicy.ToArray();        
        }

      
        public string PodajLiczbeZawodnikowZDanegoKraju(string kraj)
        {
            int ile= tablicaZawodnikow
                .Where(x => x.Kraj== kraj)
                .Count();

            return ile.ToString();
        }

        public GrupaKraju[] PodajSredniWzrostZawodnikowDlaKazdegoKraju()
        {
            return
                tablicaZawodnikow
                    .Where(x => x.Wzrost != null)
                    .GroupBy(x => x.Kraj)
                    .Select(x => new GrupaKraju()
                    {
                        Kraj = x.Key,
                        SredniWzrost = x.Average(y => (int)y.Wzrost)
                    }).ToArray();
        }

        public void ZnajdzZawodnikow(string kraj)
        {
            zawodnicyKraju = tablicaZawodnikow.Where(x => x.Kraj == kraj).ToArray();
        }

        public void ZapiszPlik(string sciezka)
        {
            StringBuilder sb = new StringBuilder(naglowek + Environment.NewLine);
            
                foreach(var w in zawodnicyKraju) 
                {
                    sb.AppendLine(
                        $"{w.Id_zawodnika};{w.Id_trenera};{w.Imie}" +
                        $"{w.Nazwisko};{w.Kraj};{w.DataUrodzenia?.ToString("yyyy-MM-dd")};" + // ten znak zapytania to taki skrótowy if, któy działa tak, że jzezli DataUrodzenia = null to nic nie wypisuj a jeżeli jest != null to użyj metody ToString()
                        $"{w.Wzrost};{w.Waga}"
                        );
                }
                
            File.WriteAllText(sciezka, sb.ToString());

            //ZawodnikVM zv = new ZawodnikVM();
            //string a = zv?.Kraj;

            //if (zv.Kraj == null)
            //    a = "";
            //else
            //    a = zv.Kraj;       
        }

        public void Usun(int id)
        {
            ModelBazyDataContext db = new ModelBazyDataContext(connString);
            var z = db.Zawodnik.FirstOrDefault(x => x.id_zawodnika == id);
            db.Zawodnik.DeleteOnSubmit(z);
            db.SubmitChanges();
        }

        public void Edytuj(ZawodnikVM z)
        {
            ModelBazyDataContext db = new ModelBazyDataContext(connString);
            var doEdycji = db.Zawodnik.FirstOrDefault(x => x.id_zawodnika == z.Id_zawodnika);
            doEdycji.imie = z.Imie;
            doEdycji.nazwisko = z.Nazwisko;
            doEdycji.kraj = z.Kraj;
            doEdycji.data_ur = z.DataUrodzenia;
            doEdycji.waga = z.Waga;
            doEdycji.wzrost = z.Wzrost;
            db.SubmitChanges();
        }

        public void Dodaj(ZawodnikVM z)
        {
            Zawodnik nowy = new Zawodnik();
            nowy.imie = z.Imie;
            nowy.nazwisko = z.Nazwisko;
            nowy.kraj = z.Kraj;
            nowy.data_ur = z.DataUrodzenia;
            nowy.waga = z.Waga;
            nowy.wzrost = z.Wzrost;
            ModelBazyDataContext db = new ModelBazyDataContext(connString);
            db.Zawodnik.InsertOnSubmit(nowy);
            db.SubmitChanges();
        }

        public ZawodnikVM PodajZawodnika(int id)
        {
            return TablicaZawodnikow.FirstOrDefault(x => x.Id_zawodnika == id);
        }
    }
}
