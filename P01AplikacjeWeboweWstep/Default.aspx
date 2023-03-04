<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="P01AplikacjeWeboweWstep.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtLiczba1" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtLiczba2" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label1" runat="server" Text="Wynik:"></asp:Label>
            <br />
            <asp:Button ID="btnPolicz" OnClick="btnPolicz_Click" runat="server" Text="Policz" />
            <asp:TextBox ID="txtWynik" runat="server"></asp:TextBox>

        </div>
    </form>
</body>
</html>
