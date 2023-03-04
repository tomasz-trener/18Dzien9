using P01AplikacjaZawodnicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace P01AplikacjeWeboweWstep
{
    public partial class ZawodnicyWstawki : System.Web.UI.Page
    {
        public ZawodnikVM[] Zawodnicy;
        // publiczne pola są widoczne we stawkach
        public string TabelkaHTML;

        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString =
               "Data Source=.;Initial Catalog=Zawodnicy;User ID=sa;Password=alx";

            ManagerZawodnikow mz = new ManagerZawodnikow(SposobPolaczenia.BazaDanych, connectionString);
            mz.WczytajZawodnikow();
            ZawodnikVM[] zawodnicy = mz.TablicaZawodnikow;
            Zawodnicy = zawodnicy;

            string tableka = "<table>";
            foreach (var z in  zawodnicy)
            {
                tableka += "<tr>" + "<td>" + z.Imie;
                tableka += "</td><td>";
                tableka += z.Nazwisko;
                tableka += "</td><td>";
                tableka += z.Kraj;
                tableka += "</td><tr>";
            }
            tableka += "</table>";
            TabelkaHTML = tableka;


        }
    }
}