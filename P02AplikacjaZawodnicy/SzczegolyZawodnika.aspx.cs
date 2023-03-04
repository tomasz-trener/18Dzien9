using P01AplikacjaZawodnicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace P02AplikacjaZawodnicy
{
    public partial class SzczegolyZawodnika : System.Web.UI.Page
    {
        public ZawodnikVM Wyswietlany;

        protected void Page_Load(object sender, EventArgs e)
        {

            string idString = Request["id"];
            int id = Convert.ToInt32(idString);

            string connectionString =
            "Data Source=.;Initial Catalog=Zawodnicy;User ID=sa;Password=alx";

            ManagerZawodnikow mz = new ManagerZawodnikow(SposobPolaczenia.BazaDanych, connectionString);
            mz.WczytajZawodnikow();

            Wyswietlany = mz.PodajZawodnika(id);

              

        }
    }
}