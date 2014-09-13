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
            <asp:DropDownList ID="drpOptionList" OnSelectedIndexChanged="drpOptionList_SelectedIndexChanged" runat="server">
                <asp:ListItem Text="Generate “New Plugin Data – Regular Scan" Value="1"></asp:ListItem>
                <asp:ListItem Text="Generate “New Plugin Data – Compliance Scan" Value="2"></asp:ListItem>
                <asp:ListItem Text="PluginOutput Variance Report-1 – Regular Scan" Value="3"></asp:ListItem>
                <asp:ListItem Text="PluginOutput Variance Report-2 – Compliance Scan" Value="4"></asp:ListItem>
                <asp:ListItem Text="New Plugin Data - Regular Scan file" Value="5"></asp:ListItem>
                <asp:ListItem Text="New Plugin Data - Compliance Scan file" Value="6"></asp:ListItem>
                <asp:ListItem Text="PluginOutput Variance Report-1”" Value="7"></asp:ListItem>
                <asp:ListItem Text="PluginOutput Variance Report-2" Value="8"></asp:ListItem>
            </asp:DropDownList>
        </span>
        <div style="margin-left: 11%;margin-top:2%">
            <asp:Button ID="btnGenerateExcel" Text="Generate" runat="server" OnClick="btnGenerateExcel_Click" />
        </div>

    </div>


</asp:Content>
