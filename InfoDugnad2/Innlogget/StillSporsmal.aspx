<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StillSporsmal.aspx.cs" MasterPageFile="..\Site.Master" Inherits="InfoDugnad2.Innlogget.StillSporsmal" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <div class="jumbotron">
        
        <h3>Still et spørsmål</h3>
      <p><i>Husk å gi nok info til at en annen kan finne svaret for deg</i></p>
        <p>
          <asp:TextBox runat="server" ID="TextBoxQuestion" Columns="80" ></asp:TextBox><br/>
        <asp:Button ID="Button1" runat="server" Text="Still spørsmål" OnClick="Ask" CssClass="btn btn-primary"/></p>

    </div>
</asp:Content>
