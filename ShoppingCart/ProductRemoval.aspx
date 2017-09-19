<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductRemoval.aspx.cs" Inherits="ShoppingCart.ProductRemoval" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <p>
            <asp:Label ID="Label3" runat="server" Text="Enter Product Id"></asp:Label>
            <asp:TextBox ID="txt_PId" runat="server" OnTextChanged="TextBox1_TextChanged1"></asp:TextBox>
            <asp:Button ID="btn_remove" runat="server" OnClick="Button1_Click" Text="Remove" />
        </p>
        <p>
            <asp:Label ID="Label2" runat="server" Text="Products available in inventory"></asp:Label>
        </p>
    </form>
</body>
</html>
