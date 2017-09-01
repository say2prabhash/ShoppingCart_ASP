<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="ShoppingCart.UserChoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <p>
            <asp:Label ID="lbl_item1" runat="server" Text="Item1"></asp:Label>
        </p>
        <p>
            <asp:Button ID="btn_item1" runat="server" OnClick="btn_item1_Click" Text="Add to Cart" />
        </p>
        <p>
            <asp:Label ID="lbl_item2" runat="server" Text="Item2"></asp:Label>
        </p>
        <p>
            <asp:Button ID="btn_item2" runat="server" Text="Add To Cart" OnClick="btn_item2_Click" />
        </p>
        <p>
            <asp:Button ID="btn_goToCart" runat="server" OnClick="Button1_Click" Text="Go To Cart" />
        </p>
    </form>
</body>
</html>
