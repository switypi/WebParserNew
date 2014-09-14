<%@ Page Language="C#" AutoEventWireup="true" Title="Reports" MasterPageFile="~/Site.Master" CodeBehind="Report.aspx.cs" Inherits="WebParser.Report" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">





    <asp:UpdatePanel ID="updtlog" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnGenerateExcel" />
        </Triggers>
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td style="float: right">
                        <asp:Label ID="Label1" Text="Select Scan" runat="server"></asp:Label><br />
                        <asp:DropDownList ID="drpScanList" Width="150px" runat="server"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label2" Text="Select Option" runat="server"></asp:Label><br />
                        <asp:DropDownList ID="drpOptionList" OnSelectedIndexChanged="drpOptionList_SelectedIndexChanged" AutoPostBack="true" runat="server">
                            <asp:ListItem Selected="True" Text="Generate Regular Scan Report" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Generate Compliance Scan Report" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Generate Vulnerabilities Not Reported List" Value="3"></asp:ListItem>

                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="float: left; margin-left: 18%" colspan="2">
                        <asp:Button ID="btnGenerateExcel" Visible="true" Text="Generate" runat="server" OnClick="btnGenerateExcel_Click" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="float: right; margin-top: 10px">
                        <asp:Label Text="No records" runat="server" Font-Bold="true" Font-Size="Large" Visible="false" ID="lblNoRecords"></asp:Label>
                    </td>
                </tr>
            </table>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

