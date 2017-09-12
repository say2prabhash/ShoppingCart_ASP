using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Generic.Dictionary<string, int>;

namespace ShoppingCart
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        Dictionary<string, int> itemsInCart;
        Label[] price;
        Label[] productName;
        int totalPrice = 0;
        Label orderTotal;
        protected void Page_Load(object sender, EventArgs e)
        {
            itemsInCart = (Dictionary<string,int>) HttpContext.Current.Session["itemList"];
            price = new Label[itemsInCart.Count];
            productName = new Label[itemsInCart.Count];
            KeyCollection keys = itemsInCart.Keys;
            List<string> items = keys.ToList<string>();
            for (int i=1;i<=itemsInCart.Count;i++)
            {

                productName[i-1] = new Label();
                productName[i-1].ID = "lbl_item" + i;
                productName[i - 1].Text = items[i-1];
                productName[i-1].Style["Position"] = "Absolute";
                this.Controls.Add(productName[i-1]);
                this.Controls.Add(new LiteralControl("<br>"));
                price[i - 1] = new Label();
                price[i - 1].ID = "btn_item" + i;
                totalPrice += itemsInCart[items[i - 1]];
                price[i - 1].Text = itemsInCart[items[i-1]].ToString();
                price[i - 1].Style["Position"] = "Absolute";
                this.Controls.Add(price[i - 1]);
                this.Controls.Add(new LiteralControl("<br>"));
                this.Controls.Add(new LiteralControl("<br>"));
            }
            orderTotal = new Label();
            orderTotal.ID = "orderPrice_lbl";
            orderTotal.Text = totalPrice.ToString();
            Label total = new Label();
            total.ID = "Total_lbl";
            total.Text = "Total Price: ";
            this.Controls.Add(total);
            this.Controls.Add(orderTotal);
            ViewState["total"] = orderTotal.Text;
            ViewState["item"] = itemsInCart;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> items = new Dictionary<string, int>();
            if(ViewState["item"]!=null)
            {
                itemsInCart = (Dictionary<string,int>)ViewState["item"];
            }
            for(int i=0;i<itemsInCart.Count;i++)
            {
                int q;
                int.TryParse(price[i].Text, out q);
                items[productName[i].Text] = q;
            }
            HttpContext.Current.Session["itemsOrdered"] = itemsInCart;
            HttpContext.Current.Session["total"] = ViewState["total"].ToString();
            Response.Redirect("OrderGeneration.aspx");
        }
    }
}