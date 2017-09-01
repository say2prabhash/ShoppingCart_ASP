using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class Inventory : System.Web.UI.Page
    {
        Dictionary<string, int> items = new Dictionary<string, int>();
        protected void Page_Load(object sender, EventArgs e)
        {
            items =(Dictionary<string,int>) HttpContext.Current.Session["itemsOrdered"];
            int item1 = int.Parse(lbl_quantityItem1.Text) - items[lbl_item1.Text];
            int item2 = int.Parse(lbl_quantityItem2.Text) - items[lbl_item2.Text];
            lbl_quantityItem1.Text = item1.ToString();
            lbl_quantityItem2.Text = item2.ToString();
        }
    }
}