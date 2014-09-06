using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using WebParser.DAL.Model;

namespace WebParser
{
    public partial class ScanLoad : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void btnsave_Click(object sender, EventArgs e)
        {

            XDocument myDoc = XDocument.Load(fileUpLoad.FileContent);
            XNamespace cm = "http://www.nessus.org/cm";
            var dtl = (from r in myDoc.Descendants("ReportHost")
                       select new ImportXMLDataDTO()
                       {
                           ReportHost = r.Attribute("name").Value,
                           ClientName = txtClientName.Text,
                           ScanDate = DateTime.Parse(txtDate.Text),
                           ScanName = txtNewScanName.Text,
                           IsAdditionalScan = false,
                           UserId = Session["UserName"] as string,
                           PlugId = r.Element("ReportItem").Attribute("pluginID").Value,
                           Port = r.Element("ReportItem").Attribute("Port") == null ? null : r.Element("ReportItem").Element("Port").Value,
                           Compliance = r.Element("ReportItem").Element("compliance") == null ? null : r.Element("ReportItem").Element("compliance").Value,
                           ComplianceResult = r.Element("ReportItem").Element(cm + "compliance-result") == null ? null : r.Element("ReportItem").Element(cm + "compliance-result").Value,
                           ComplianceActualValue = r.Element("ReportItem").Element(cm + "compliance-actual-value") == null ? null : r.Element("ReportItem").Element(cm + "compliance-actual-value").Value,
                           ComplianceCheckID = r.Element("ReportItem").Element(cm + "compliance-check-id") == null ? null : r.Element("ReportItem").Element(cm + "compliance-check-id").Value,
                           ComplianceOutPut = r.Element("ReportItem").Element(cm + "compliance-output") == null ? null : r.Element("ReportItem").Element(cm + "compliance-output").Value,
                           CompliancePolicyValue = r.Element("ReportItem").Element(cm + "compliance-policy-value") == null ? null : r.Element("ReportItem").Element(cm + "compliance-policy-value").Value,
                           Description = r.Element("ReportItem").Element("description") == null ? null : r.Element("ReportItem").Element("description").Value,
                           ExploitAvailable = r.Element("ReportItem").Element("exploit_available") == null ? null : r.Element("ReportItem").Element("exploit_available").Value,
                           ExploitabilityEase = r.Element("ReportItem").Element("exploitability_ease") == null ? null : r.Element("ReportItem").Element("exploitability_ease").Value,
                           ExploitedByMalware = r.Element("ReportItem").Element("exploited_by_malware") == null ? null : r.Element("ReportItem").Element("exploited_by_malware").Value,
                           RiskFactor = r.Element("ReportItem").Element("risk_factor") == null ? null : r.Element("ReportItem").Element("risk_factor").Value,
                           SeeLAlso = r.Element("ReportItem").Element("see_also") == null ? null : r.Element("ReportItem").Element("see_also").Value,
                           Solution = r.Element("ReportItem").Element("solution") == null ? null : r.Element("ReportItem").Element("solution").Value,
                           Synopsis = r.Element("ReportItem").Element("synopsis") == null ? null : r.Element("ReportItem").Element("synopsis").Value,
                           PluginOutput = r.Element("ReportItem").Element("plugin_output") == null ? null : r.Element("ReportItem").Element("plugin_output").Value,

                       }).ToList();

            var obj = new WebParser.DAL.DataFunction.OperationFunctions();
            try
            {
                bool retValue = obj.ImportXmlData(dtl);
                if (retValue)
                {
                    lblmessage.Visible = true;
                    lblmessage.Text = "Import successfull.";
                    txtNewScanName.Text = string.Empty;
                    txtDate.Text = string.Empty;
                    txtClientName.Text = string.Empty;

                }
                else
                {
                    lblmessage.Visible = true;
                    lblmessage.Text = "Import failed.";
                }
            }
            catch (Exception ex)
            {
                lblmessage.Visible = true;
                lblmessage.Text = "Import failed.";
                throw;
            }


        }

        [System.Web.Services.WebMethod]
        public static void GetRecords()
        {
            List<ScanMasterDTO> scanMasters = new List<ScanMasterDTO>();
            var obj = new WebParser.DAL.DataFunction.OperationFunctions();
            scanMasters = obj.GetPreviousScanResult(HttpContext.Current.Session["UserName"] as string);

        }

    }
}