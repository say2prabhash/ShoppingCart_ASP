using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        List<string>listOfItems = new List<string>();
        Label[] quantity;
        Label[] label;
        protected void Page_Load(object sender, EventArgs e)
        {
            listOfItems = (List<string>) HttpContext.Current.Session["itemList"];
            quantity = new Label[listOfItems.Count];
            label = new Label[listOfItems.Count];
            for (int i=1;i<=listOfItems.Count;i++)
            {

                label[i-1] = new Label();
                label[i-1].ID = "lbl_item" + i;
                label[i-1].Text = listOfItems[i-1];
                label[i-1].Style["Position"] = "Absolute";
                this.Controls.Add(label[i-1]);
                this.Controls.Add(new LiteralControl("<br>"));
                quantity[i - 1] = new Label();
                quantity[i - 1].ID = "btn_item" + i;
                quantity[i - 1].Text = "1";
                quantity[i - 1].Style["Position"] = "Absolute";
                this.Controls.Add(quantity[i - 1]);
                this.Controls.Add(new LiteralControl("<br>"));
            }
            ViewState["item"] = listOfItems;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> items = new Dictionary<string, int>();
            if(ViewState["item"]!=null)
            {
                listOfItems = (List<string>)ViewState["item"];
            }
            for(int i=0;i<listOfItems.Count;i++)
            {
                int q;
                int.TryParse(quantity[i].Text, out q);
                items[label[i].Text] = q;
            }
            HttpContext.Current.Session["itemsOrdered"] = items;
            Response.Redirect("Inventory.aspx");
        }
    }
}