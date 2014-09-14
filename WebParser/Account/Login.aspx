<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebParser.Account.Login" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
    </hgroup>

    <style type="text/css">
        .overlay {
            position: fixed;
            z-index: 98;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px;
            background-color: #aaa;
            filter: alpha(opacity=80);
            opacity: 0.8;
        }

        .overlayContent {
            z-index: 99;
            margin: 250px auto;
            width: 80px;
            height: 80px;
        }

            .overlayContent h2 {
                font-size: 18px;
                font-weight: bold;
                color: #000;
            }

            .overlayContent img {
                width: 80px;
                height: 80px;
            }
    </style>

    <asp:UpdatePanel ID="updtlog" runat="server" UpdateMode="Always">
        <ContentTemplate>

            
                <h2>Use a local account to log in.</h2>
               <asp:Panel ID="dvNewScan" runat="server" Style="width: 20%">

                    <fieldset>
                        <legend>Log in Form</legend>
                        <ol>
                            <li>
                                <asp:Label ID="Label1" runat="server" AssociatedControlID="UserName">User name</asp:Label>
                                <span>
                                     <asp:TextBox runat="server" ID="UserName" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ControlToValidate="UserName" CssClass="field-validation-error" ErrorMessage="The user name field is required." />
                                </span>
                               
                            </li>
                            <li>
                                <asp:Label ID="Label2" runat="server" AssociatedControlID="Password">Password</asp:Label>
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="The password field is required." />
                            </li>

                        </ol>
                        <asp:Button ID="Button1" runat="server" OnClick="login_Click" Text="Log in" />
                    </fieldset>
               </asp:Panel>

                <asp:Label ID="lblErrorMessage" Visible="false" runat="server"></asp:Label>

                <p>
                    <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register</asp:HyperLink>
                    if you don't have an account.
       
                </p>
           
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="updProgress"
        AssociatedUpdatePanelID="updtlog"
        runat="server">
         <ProgressTemplate>
            <div class="overlay" />
            <div class="overlayContent">
                <asp:Image ID="Image1" runat="server"   ImageUrl="~/Images/ImgLoader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

</asp:Content>
