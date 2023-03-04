<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZawodnicyWstawki.aspx.cs" Inherits="P01AplikacjeWeboweWstep.ZawodnicyWstawki" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
           <%
            for (int i = 0; i < Zawodnicy.Length; i++)
            {%>
                
              <p><%=Zawodnicy[i].ImieNazwisko %> </p>

      <%    }
            %>
     
        <br />

        <p>Tabelka w HTML </p>

        <table>
            <tr style="color:greenyellow">
                <td>Imie</td>
                <td>Nazwisko</td>
                <td>Kraj</td>
            </tr>

            <%
                foreach (var z in Zawodnicy)
                {  %>
 
                  <tr style="color:<%=z.Kolor %>">
                     <td><%= z.Imie %></td>
                     <td><%= z.Nazwisko %></td>
                     <td><%= z.Kraj %></td>
                 </tr>   

              <%}

                %>

        </table>
        <br />
        <%= TabelkaHTML %>



    </form>
</body>
</html>
