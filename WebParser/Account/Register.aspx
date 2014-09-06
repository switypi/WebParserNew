<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebParser.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Use the form below to create a new account.</h2>
    </hgroup>
    <div>
        <fieldset>
            <legend>Registration Form</legend>
            <ol>
                <li>
                    <asp:Label ID="Label1" runat="server" AssociatedControlID="UserName">User name</asp:Label>
                    <asp:TextBox runat="server" ID="UserName" />
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="UserName" ID="RegularExpressionValidator1" ValidationExpression="^[\s\S]{4,8}$" runat="server" ErrorMessage="Minimum 5 and Maximum 8 characters required."></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName"
                        CssClass="field-validation-error" ErrorMessage="The user name field is required." />
                </li>

                <li>
                    <asp:Label ID="Label3" runat="server" AssociatedControlID="Password">Password</asp:Label>
                    <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Password"
                        CssClass="field-validation-error" ErrorMessage="The password field is required." />
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="Password" ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{4,8}$" runat="server" ErrorMessage="Minimum 5 and Maximum 8 characters required."></asp:RegularExpressionValidator>
                </li>
                <li>
                    <asp:Label ID="Label4" runat="server" AssociatedControlID="ConfirmPassword">Confirm password</asp:Label>
                    <asp:TextBox runat="server" ID="ConfirmPassword" MaxLength="12" TextMode="Password" />

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ConfirmPassword"
                        CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                        CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
                </li>
                <li>
                    <span>
                        <asp:Label ID="Label2" runat="server">Is Admin</asp:Label>
                        <asp:CheckBox ID="chktnAdmin" runat="server" />
                    </span>
                </li>
            </ol>

        </fieldset>
    </div>
    <div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Register" />
    </div>


    <asp:Label ID="lblMessage" Visible="false" runat="server"></asp:Label>
</asp:Content>
