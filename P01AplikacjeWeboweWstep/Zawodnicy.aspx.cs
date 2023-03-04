using P01AplikacjaZawodnicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace P01AplikacjeWeboweWstep
{
    public partial class Zawodnicy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnWczytaj_Click(object sender, EventArgs e)
        {
            string connectionString =
                "Data Source=.;Initial Catalog=Zawodnicy;User ID=sa;Password=alx";

            ManagerZawodnikow mz = new ManagerZawodnikow(SposobPolaczenia.BazaDanych, connectionString);
            mz.WczytajZawodnikow();
            ZawodnikVM[] zawodnicy = mz.TablicaZawodnikow;

            lbDane.DataSource = zawodnicy;
            lbDane.DataTextField = "ImieNazwisko"; // inaczej nazywa się te pole 
            lbDane.DataBind(); // trzeba uruchomić metodę dataBind
            // która ostatecznie łączy dane z kontrolką
            // tego nie trzeba było robić w aplikacji okienkowej 
        }
    }
}