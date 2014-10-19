<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registrer.aspx.cs" Inherits="InfoDugnad2.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">



    <asp:CreateUserWizard runat="server" ID="RegisterUser" ViewStateMode="Disabled" OnCreatedUser="RegisterUser_CreatedUser">
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="wizardStepPlaceholder" />
            <asp:PlaceHolder runat="server" ID="navigationPlaceholder" />
        </LayoutTemplate>
        <WizardSteps>
            <asp:CreateUserWizardStep runat="server" ID="RegisterUserWizardStep">
                <ContentTemplate>
                    <div class="form-horizontal">
                        <fieldset>
                            <div class="form-group">
                                <label class="col-md-3 control-label" for="textinput"></label>
                                <div class="col-md-6">
                                <label class="col-md-6 control-label" for="textinput">Registrer ny bruker</label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label" for="textinput">velg brukernavn</label>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="Username" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName"
                                        CssClass="field-validation-error" ErrorMessage="OBS" />
                                </div>
                            </div>

                            <!-- Text input-->
                            <div class="form-group">
                                <label class="col-md-4 control-label" for="textinput">Epostadresse</label>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="Email" TextMode="Email" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Email"
                                        CssClass="field-validation-error" ErrorMessage="OBS." />
                                </div>
                            </div>

                            <!-- Password input-->
                            <div class="form-group">
                                <label class="col-md-4 control-label" for="passwordinput">Passord</label>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Password"
                                        CssClass="field-validation-error" ErrorMessage="The password field is required." />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label" for="passwordinput">Gjenta passord</label>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ConfirmPassword"
                                        CssClass="field-validation-error" Display="Dynamic" ErrorMessage="OBS" />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                                        CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Passordene er ikke like" />
                                </div>
                            </div>

                            <!-- Button -->
                            <div class="form-group">
                                <label class="col-md-4 control-label" for="singlebutton"></label>
                                <div class="col-md-4">
                                    <asp:Button ID="Button1" runat="server" CommandName="MoveNext" Text="Registrer bruker" CssClass="btn btn-primary" />
                                </div>
                            </div>

                        </fieldset>
                    </div>

                </ContentTemplate>
                <CustomNavigationTemplate />
            </asp:CreateUserWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
</asp:Content>
