<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="..\Site.Master" CodeBehind="Svar.aspx.cs" Inherits="InfoDugnad2.Innlogget.Svar" %>
<%@ Import Namespace="InfoDugnad2.Utilities" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <% if (Answer != null)
           { %>
        <h3>Besvart spørsmål: <%: Answer.RequestInfo %></h3>
        <p><i>OBS! Etter at svaret nå er sett så vil det slettes.</i></p>
        <p>
            Svar:<br/>
            <asp:TextBox runat="server" ID="TextBoxAnswer" Enabled="False" TextMode="MultiLine" Columns="60" Rows="10"></asp:TextBox>
        </p>
        <p>Spørsmål stilt <%: Answer.RequestedDate.Friendly() %></p>
        <p>Spørsmål besvart <%: Answer.AnsweredDate.Friendly() %></p>
        <p>
            <asp:HiddenField runat="server" ID="AnswerId"/>
            <asp:Button runat="server" Text="Still samme spørsmål på nytt/forkast svaret" OnClick="AskAgain"/> 
            <asp:Button runat="server" Text="Rapporter upassende svar" OnClick="Complain"/> 
        </p>
        <% }else{ %>
            <h3>404</h3>
        <%} %>
        <a href="/Innlogget/MinSide">tilbake</a>
    </div>

</asp:Content>
