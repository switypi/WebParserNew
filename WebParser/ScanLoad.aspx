<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ScanLoad.aspx.cs" Inherits="WebParser.ScanLoad" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css">
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.1/jquery-ui.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#MainContent_txtDate").datepicker({ changeMonth: true, changeYear: true });
        })

    </script>
    <script type="text/javascript">

        function SetForNewScan() {
            document.getElementById("dvNewScan").style.visibility = 'visible';
            document.getElementById("dvAdditionalScan").style.visibility = 'hidden';
            document.getElementById("ChkAddScan").checked = false;


        }
        function GetAdditionalSacn() {

            document.getElementById("dvNewScan").style.visibility = 'hidden';
            document.getElementById("dvAdditionalScan").style.visibility = 'visible';
            document.getElementById("chkNewScan").checked = false;
            $.ajax({
                type: "post",
                url: "ScanLoad.aspx/GetRecords",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    OnSuccess(result.d);
                },
                error: function (xhr, status, error) {
                    OnFailure(error);
                }
            });
        }
        function OnSuccess(dateTime) {
            if (dateTime) {
               
                if (dateTime.length > 0) {
                    $("#MainContent_grdScanList").append("<tr><th>ScanID</th><th> Scan Name</th>  <th>Scan Date</th><th> Client Name</th></tr>");
                    for (var i = 0; i < dateTime.length; i++) {

                        $("#MainContent_grdScanList").append("<tr><td>" +
                        dateTime[i].ScanID + "</td> <td>" +
                        dateTime[i].ScanName + "</td> <td>" +
                         dateTime[i].ScanDate + "</td> <td>" +
                        dateTime[i].ClientName + "</td></tr>");
                    }
                }

            }
        }
        function OnFailure(error) {
            alert(error);
        }

    </script>

    <div>
        <table>
            <tr>
                <td>
                    <span>
                        <asp:Label ID="Label2" runat="server">New Scan</asp:Label>
                        <input type="checkbox" checked="checked" id="chkNewScan" onclick="SetForNewScan()" />

                    </span>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server">Additional Scan</asp:Label>
                    <input type="checkbox" id="ChkAddScan" onclick="GetAdditionalSacn()" />
                </td>
            </tr>


        </table>
    </div>

    <div id="dvNewScan" style="width: 20%">
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
                    <asp:FileUpload ID="fileUpLoad" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ControlToValidate="fileUpLoad" CssClass="field-validation-error" ErrorMessage="The file is required." />

                </li>
                <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click"
                    Style="width: 85px" />
                <h2>
                    <asp:Label ID="lblmessage" runat="server" />
                </h2>

            </ol>
        </fieldset>
    </div>


    <div id="dvAdditionalScan" style="vertical-align:top">
        <asp:GridView runat="server" BorderColor="SteelBlue" ID="grdScanList" OnLoad="grdScanList_Load">
            <Columns>
                <asp:BoundField HeaderText="Scan Id" DataField="ScanID" />
                <asp:BoundField HeaderText="Scan Name" DataField="ScanName" />
                <asp:BoundField HeaderText="Scan Date" DataField="ScanDate" />
                <asp:BoundField HeaderText="Client Name" DataField="ClientName" />
            </Columns>
          <%--  <EmptyDataTemplate>No records found.</EmptyDataTemplate>--%>
        </asp:GridView>
    </div>







</asp:Content>
