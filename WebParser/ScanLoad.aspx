﻿<%@ Page Title="Scan load" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ScanLoad.aspx.cs" Inherits="WebParser.ScanLoad" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" href="http://localhost:1307/code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css">
            <script src="//code.jquery.com/jquery-1.10.2.js"></script>
            <script src="//code.jquery.com/ui/1.11.1/jquery-ui.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#MainContent_txtDate").datepicker({ changeMonth: true, changeYear: true });
        })

    </script>

    <script type="text/javascript">

        function SetForNewScan() {
            document.getElementById("dvNewScan").style.display = 'block';
            document.getElementById("dvAdditionalScan").style.display = 'hidden';
            // document.getElementById("ChkAddScan").checked = false;

        }
        function GetAdditionalSacn() {

            document.getElementById("dvNewScan").style.display = 'hidden';
            document.getElementById("dvAdditionalScan").style.display = 'block';

        }
        function gridRowOnclick(ctrlId) {
            alert(ctrlId)
            var retVal = confirm("Do you want to continue ?");
            if (retVal == true) {
                __doPostBack(ctrlId, 'FROMBTN')
                return true;
            } else {
                return false;
            }


        }

    </script>

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

   
    <asp:UpdatePanel ID="updtMaster" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
        <ContentTemplate>

            <div>
                <table>
                    <tr style="width: 100%">
                        <td align="center">
                            <asp:Label ID="Label2" runat="server">New Scan</asp:Label>
                            <asp:RadioButton runat="server" OnCheckedChanged="NewScan_CheckedChanged" AutoPostBack="true" Checked="true" GroupName="scanGroup" />
                        </td>
                        <td align="center">
                            <asp:Label ID="Label1" runat="server">Additional Scan</asp:Label>
                            <asp:RadioButton runat="server" ID="rdbtnAddtional" AutoPostBack="true" OnCheckedChanged="AddtionalScan_CheckedChanged" GroupName="scanGroup" />
                        </td>
                    </tr>
                </table>
            </div>

            <asp:Panel ID="dvNewScan" runat="server" Style="width: 20%">
                <fieldset>
                    <legend>Registration Form</legend>
                    <ol>
                        <li>
                            <asp:Label ID="lblClientName" Text="Client Name" runat="server"></asp:Label>
                            <asp:TextBox ID="txtClientName" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ControlToValidate="txtClientName" CssClass="field-validation-error" ErrorMessage="The client name field is required." />
                        </li>
                        <li>
                            <asp:Label ID="lblScanName" Text="Scan Name" runat="server"></asp:Label>
                            <asp:TextBox ID="txtNewScanName" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ControlToValidate="txtNewScanName" CssClass="field-validation-error" ErrorMessage="The scan name field is required." />
                        </li>
                        <li>
                            <asp:Label ID="lblScanDate" Text="Scan Date" runat="server"></asp:Label>
                            <asp:TextBox ID="txtDate" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ControlToValidate="txtDate" CssClass="field-validation-error" ErrorMessage="The scan date field is required." />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ControlToValidate="txtDate" ValidationExpression="^(([1-9])|(0[1-9])|(1[0-2]))\/((0[1-9])|([1-31]))\/((19|20)\d\d)$" Display="Dynamic" SetFocusOnError="true" ErrorMessage="invalid date">*</asp:RegularExpressionValidator>
                        </li>
                        <li>
                            <asp:Label ID="lblUploadNewXmlFIle" Text="Upload XMl file" runat="server"></asp:Label>
                            <%-- <ajaxToolkit:AsyncFileUpload ID="fileUpload1" runat="server" />--%>
                            <ajaxToolkit:AsyncFileUpload ID="fileUpload1" runat="server" />
                            <%-- <asp:FileUpload ID="fileUpLoad" runat="server" />--%>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ControlToValidate="fileUpload1" CssClass="field-validation-error" ErrorMessage="The file is required." />--%>

                </li>
                        <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click"
                            Style="width: 85px" />
                        <h2>
                            <asp:Label ID="lblmessage" runat="server" />
                        </h2>
                    </ol>
                </fieldset>
            </asp:Panel>

            <asp:Panel ID="dvAdditionalScan" runat="server">
                <asp:GridView runat="server" GridLines="Vertical" ID="grdScanList"
                    OnRowDataBound="grdScanList_RowDataBound" OnRowEditing="grdScanList_RowEditing"  OnSelectedIndexChanged="grdScanList_SelectedIndexChanged" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CaptionAlign="Left" CellPadding="4" ForeColor="Black">
                    <Columns>
                        <asp:BoundField HeaderText="Scan Id" DataField="ScanID" />
                        <asp:BoundField HeaderText="Scan Name" DataField="ScanName" />
                        <asp:BoundField HeaderText="Scan Date" DataField="ScanDate" />
                        <asp:BoundField HeaderText="Client Name" DataField="ClientName" />
                        
                        <asp:CommandField ShowEditButton="true" EditText="Select" ButtonType="Button" />
                    </Columns>

                </asp:GridView>
            </asp:Panel>



        </ContentTemplate>
    </asp:UpdatePanel>

     <asp:UpdateProgress ID="updProgress"
        AssociatedUpdatePanelID="updtMaster"
        runat="server">
        <ProgressTemplate>
            <div class="overlay" />
            <div class="overlayContent">
                <asp:Image ID="Image1" runat="server"   ImageUrl="~/Images/ImgLoader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
