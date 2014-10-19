<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Innlogging.aspx.cs" Inherits="InfoDugnad2.Account.Login" %>

<%@ Register Src="~/Bruker/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="form-horizontal">
        <asp:Login ID="Login1" runat="server" ViewStateMode="Disabled" RenderOuterTable="false">
            <LayoutTemplate>
                <p class="validation-summary-errors">
                    <asp:Literal runat="server" ID="FailureText" />
                </p>
                <fieldset>
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName" CssClass="field-validation-error" ErrorMessage="The user name field is required." />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-4 control-label" for="passwordinput">Passord</label>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="The password field is required." />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-4 control-label" for="singlebutton"></label>
                        <div class="col-md-4">
                            <asp:Button ID="Button1" runat="server" CommandName="Login" Text="Logg inn" CssClass="btn btn-primary" />
                        </div>
                    </div>

                </fieldset>
            </LayoutTemplate>
        </asp:Login>
                    <div class="form-group">
                        <label class="col-md-4 control-label" for="singlebutton"></label>
                        <div class="col-md-4">
                            <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Registrer ny bruker</asp:HyperLink>
                        </div>
                    </div>
    </div>

</asp:Content>
