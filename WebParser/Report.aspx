<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="Report.aspx.cs" Inherits="WebParser.Report" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgrup class="title">
        <h1><%: Title %>.</h1>
    </hgrup>


    <asp:UpdatePanel ID="updtlog" runat="server" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnGenerateExcel" />
        </Triggers>
        <ContentTemplate>
            <div style="width: 100%;">

                <span style="margin-left: 11%">
                    <asp:Label ID="Label1" Text="Select Scan" runat="server"></asp:Label>
                    <asp:DropDownList ID="drpScanList" runat="server"></asp:DropDownList>
                </span>

                <span style="margin-left: 11%">
                    <asp:Label ID="Label2" Text="Select Option" runat="server"></asp:Label>
                    <asp:DropDownList ID="drpOptionList" OnSelectedIndexChanged="drpOptionList_SelectedIndexChanged" AutoPostBack="true" runat="server">
                        <asp:ListItem Selected="True" Text="Generate Regular Scan Report" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Generate Compliance Scan Report" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Generate Vulnerabilities Not Reported List" Value="3"></asp:ListItem>

                    </asp:DropDownList>
                </span>
                <div style="margin-left: 11%; margin-top: 2%">
                    <span>
                        <asp:Button ID="btnGenerateExcel" Visible="true" Text="Generate" runat="server" OnClick="btnGenerateExcel_Click" />
                        
                    </span>

                </div>
                <div>
                    <asp:Label Text="No records" runat="server" Visible="false" ID="lblNoRecords"></asp:Label>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

