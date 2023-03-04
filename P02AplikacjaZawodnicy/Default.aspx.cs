using P01AplikacjaZawodnicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace P02AplikacjaZawodnicy
{
    public partial class Default : System.Web.UI.Page
    {
        public ZawodnikVM[] Zawodnicy;
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString =
              "Data Source=.;Initial Catalog=Zawodnicy;User ID=sa;Password=alx";

            ManagerZawodnikow mz = new ManagerZawodnikow(SposobPolaczenia.BazaDanych, connectionString);
            mz.WczytajZawodnikow();
            ZawodnikVM[] zawodnicy = mz.TablicaZawodnikow;
            Zawodnicy = zawodnicy;
        }
    }
}