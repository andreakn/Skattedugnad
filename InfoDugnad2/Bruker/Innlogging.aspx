<%@ Page Title="Spør for meg" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Innlogging.aspx.cs" Inherits="InfoDugnad2.Account.Login" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    
    <div class="jumbotron">
        <i>"Spør for meg"</i> er et nettsted hvor du kan legge inn spørsmål som du av ulike grunner ikke ønsker å stille selv<br/>
        for eksempel hvis du ikke ønsker å legge igjen spor ift at du har søkt deg frem til informasjonen.<br/><br/>
        Nettstedet er en slags dugnad hvor man får igjen for å hjelpe andre i form av rett til å stille egne spørsmål som igjen vil besvares av andre.<br/>
        <br/>
        Hvis man for eksempel lurer på hva naboens oppføring i skattelistene er så bør man stille spørsmål på formen "fornavn etternavn, ca alder, kommune" <br/>
        slik at den som skal søke for deg har nok info å gå etter.<br/><br/>
        Vær snille med hverandre. Troll blir utestengt
    </div>
    
    <div class="form-horizontal">

        <asp:PlaceHolder runat="server" Visible="False" ID="phError">
            <div class="alert alert-danger">
                <asp:Literal runat="server" ID="LiteralError"></asp:Literal>
            </div>
        </asp:PlaceHolder>

        <div class="form-group">
            <label class="col-md-4 control-label" for="textinput"></label>
            <div class="col-md-4">
                <label class="col-md-4 control-label" for="textinput">Logg inn</label>
            </div>
        </div>

        <div class="form-group">
            <label class="col-md-4 control-label" for="textinput">Brukernavn</label>
            <div class="col-md-4">
                <asp:TextBox runat="server" ID="UserName" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName" CssClass="field-validation-error" ErrorMessage="OBS" />
            </div>
        </div>

        <div class="form-group">
            <label class="col-md-4 control-label" for="passwordinput">Passord</label>
            <div class="col-md-4">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="OBS" />
            </div>
        </div>

        <div class="form-group">
            <label class="col-md-4 control-label" for="singlebutton"></label>
            <div class="col-md-4">
                <asp:Button ID="Button1" runat="server" OnClick="Login_Click" Text="Logg inn" CssClass="btn btn-primary" />
            </div>
        </div>

        <div class="form-group">
            <label class="col-md-4 control-label" for="singlebutton"></label>
            <div class="col-md-4">
                <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Registrer ny bruker</asp:HyperLink>
            </div>
        </div>
    </div>

</asp:Content>
