<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="WebParser.Admin" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <%-- <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Your app description page.</h2>
    </hgroup>--%>
    


    <table style="width: 100%;">
        <tr>
            <td style="float: right">
                <asp:Label ID="Label1" Text="Select Scan" runat="server"></asp:Label></br>
                <asp:DropDownList ID="drpScanList" runat="server" Height="25px" Width="150px"></asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label2" Text="Select Option" runat="server"></asp:Label></br>
                <asp:DropDownList ID="drpOptionList"   Height="25px" OnSelectedIndexChanged="drpOptionList_SelectedIndexChanged" AutoPostBack="true" runat="server">
                    <asp:ListItem Selected="True" Text="Generate “New Plugin Data – Regular Scan" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Generate New Plugin Data – Compliance Scan" Value="2"></asp:ListItem>
                    <asp:ListItem Text="PluginOutput Variance Report-1 – Regular Scan" Value="3"></asp:ListItem>
                    <asp:ListItem Text="PluginOutput Variance Report-2 – Compliance Scan" Value="4"></asp:ListItem>
                    <asp:ListItem Text="New Plugin Data - Regular Scan file" Value="5"></asp:ListItem>
                    <asp:ListItem Text="New Plugin Data - Compliance Scan file" Value="6"></asp:ListItem>
                    <asp:ListItem Text="PluginOutput Variance Report-1”" Value="7"></asp:ListItem>
                    <asp:ListItem Text="PluginOutput Variance Report-2" Value="8"></asp:ListItem>
                </asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td style=" float:left; margin-left:14%" colspan="2">
                <asp:Panel Visible="false" ID="pnlUpload"  runat="server">

                    <asp:Label ID="lblUploadNewXmlFIle" Text="Upload Excel" runat="server"></asp:Label>
                    <br>
                    <ajaxToolkit:AsyncFileUpload ID="fileUpload1" runat="server"  Height="30pt" />

                </asp:Panel>
            </td>
            <td></td>
        </tr>
        <tr>
            <td style="float:left;margin-left:14%" colspan="2">
                <asp:Button ID="btnGenerateExcel" Visible="true" Text="Generate" runat="server" OnClick="btnGenerateExcel_Click" />
                <asp:Button ID="btnUpload" Text="Upload" Visible="false" runat="server" OnClick="btnUpload_Click" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td style="float: right;margin-top:10px">
                <asp:Label Text="No records" runat="server" Font-Bold="true" Font-Size="Large" Visible="false" ID="lblNoRecords"></asp:Label>
            </td>
        </tr>
    </table>

    <div>
    </div>




</asp:Content>
