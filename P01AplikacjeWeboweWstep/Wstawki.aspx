<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Wstawki.aspx.cs" Inherits="P01AplikacjeWeboweWstep.Wstawki" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       
       

          <% 
               
              string s = "ala ma kota";

              for (int i = 0; i < 10; i++)
              { %>
                <p><% Response.Write(s); %></p>  
          <%  }

              %>

    </form>
</body>
</html>
