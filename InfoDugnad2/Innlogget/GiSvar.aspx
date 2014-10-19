<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="..\Site.Master" CodeBehind="GiSvar.aspx.cs" Inherits="InfoDugnad2.Innlogget.GiSvar" %>
<%@ Import Namespace="InfoDugnad2.Utilities" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <% if (RequestedAnswer != null)
       { %>
    <div class="jumbotron">
        <h3><%:RequestedAnswer.RequestInfo %></h3>
      <i>(spørsmålet ble stilt <%:RequestedAnswer.RequestedDate.Friendly() %></i>
        <p>Svar:<br/>
        <asp:HiddenField runat="server" ID="SessionCookie"/>
          <asp:TextBox runat="server" ID="TextBoxAnswerInfo" TextMode="MultiLine" Columns="60" Rows="10"></asp:TextBox><br/>
        <asp:Button runat="server" Text="Svar avgitt" OnClick="RegisterAnswer" CssClass="btn btn-primary"/></p>
        <p>
            Eller:<br/>
        <asp:Button runat="server" CssClass="btn " Text="Fant ikke svar" OnClick="RegisterCantAnswer"/>
        <asp:Button runat="server" CssClass="btn btn-danger" Text="Dette er et upassende spørsmål" OnClick="RegisterComplaint"/>
        <asp:Button runat="server" CssClass="btn " Text="Jeg vil heller svare på et annet spørsmål" OnClick="FindNewRequest"/>
        </p>
        <asp:HiddenField runat="server" ID="QuestionId"/>
    </div>
    <% }
       else
       { %>
    <h3>Beklager</h3>
    <p>fant ingen spørsmål du kunne svare på, prøv igjen senere</p>
    <%} %>
</asp:Content>
