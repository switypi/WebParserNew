<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="WebParser.Admin" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Your app description page.</h2>
    </hgroup>
    <div style="width: 100%;">

        <span style="margin-left: 11%">
            <asp:Label Text="Select Scan" runat="server"></asp:Label>
            <asp:DropDownList ID="drpScanList" runat="server"></asp:DropDownList>
        </span>

        <span style="margin-left: 11%">
            <asp:Label Text="Select Option" runat="server"></asp:Label>
            <asp:DropDownList ID="drpOptionList" OnSelectedIndexChanged="drpOptionList_SelectedIndexChanged" AutoPostBack="true" runat="server">
                <asp:ListItem Selected="True" Text="Generate “New Plugin Data – Regular Scan" Value="1"></asp:ListItem>
                <asp:ListItem Text="Generate “New Plugin Data – Compliance Scan" Value="2"></asp:ListItem>
                <asp:ListItem Text="PluginOutput Variance Report-1 – Regular Scan" Value="3"></asp:ListItem>
                <asp:ListItem Text="PluginOutput Variance Report-2 – Compliance Scan" Value="4"></asp:ListItem>
                <asp:ListItem Text="New Plugin Data - Regular Scan file" Value="5"></asp:ListItem>
                <asp:ListItem Text="New Plugin Data - Compliance Scan file" Value="6"></asp:ListItem>
                <asp:ListItem Text="PluginOutput Variance Report-1”" Value="7"></asp:ListItem>
                <asp:ListItem Text="PluginOutput Variance Report-2" Value="8"></asp:ListItem>
            </asp:DropDownList>
        </span>
        <asp:Panel Style="margin-left: 11%; margin-top: 2%" Visible="false" ID="pnlUpload" runat="server">
            <span style="margin-left: 11%">
                <asp:Label ID="lblUploadNewXmlFIle" Text="Upload excel report" runat="server"></asp:Label>
                <ajaxToolkit:AsyncFileUpload ID="fileUpload1" runat="server" />
            </span>
        </asp:Panel>
        <div style="margin-left: 11%; margin-top: 2%">
            <span>
                <asp:Button ID="btnGenerateExcel" Visible="true" Text="Generate" runat="server" OnClick="btnGenerateExcel_Click" />
                <asp:Button ID="btnUpload" Text="Upload" Visible="false" runat="server" OnClick="btnUpload_Click" />
            </span>

        </div>
        <div>
            <asp:Label Text="No records" runat="server" Visible="false" ID="lblNoRecords"></asp:Label>
        </div>

    </div>


</asp:Content>
