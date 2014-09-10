using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
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
            if (!Page.IsPostBack)
            {
                List<ScanMasterDTO> scanMasters = new List<ScanMasterDTO>();
                grdScanList.DataSource = scanMasters;
                grdScanList.DataBind();

            }
            else
            {
                if (Page.Request.Form["__EVENTARGUMENT"] == "FROMBTN")
                {
                    PerformCustomAction();
                }
            }

        }

        private void PerformCustomAction()
        {
            dvAdditionalScan.Visible = false;
            dvNewScan.Visible = true;
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {

            XDocument myDoc = XDocument.Load(fileUpload1.FileContent);
            XNamespace cm = "http://www.nessus.org/cm";
            var dtl = (from r in myDoc.Descendants("ReportItem")
                       select new ImportXMLDataDTO()
                       {
                           ReportHost = r.Parent.Attribute("name").Value,
                           ClientName = txtClientName.Text,
                           ScanDate = DateTime.Parse(txtDate.Text),
                           ScanName = txtNewScanName.Text,
                           IsAdditionalScan = false,
                           UserId = Session["UserName"] as string,
                           PlugId = r.Attribute("pluginID").Value,
                           Port = r.Attribute("port") == null ? null : r.Attribute("port").Value,
                           Compliance = r.Element("compliance") == null ? null : r.Element("compliance").Value,
                           ComplianceResult = r.Element(cm + "compliance-result") == null ? null : r.Element(cm + "compliance-result").Value,
                           ComplianceActualValue = r.Element(cm + "compliance-actual-value") == null ? null : r.Element(cm + "compliance-actual-value").Value,
                           ComplianceCheckID = r.Element(cm + "compliance-check-id") == null ? null : r.Element(cm + "compliance-check-id").Value,
                           ComplianceOutPut = r.Element(cm + "compliance-output") == null ? null : r.Element(cm + "compliance-output").Value,
                           CompliancePolicyValue = r.Element(cm + "compliance-policy-value") == null ? null : r.Element(cm + "compliance-policy-value").Value,
                           Description = r.Element("description") == null ? null : r.Element("description").Value,
                           ExploitAvailable = r.Element("exploit_available") == null ? null : r.Element("exploit_available").Value,
                           ExploitabilityEase = r.Element("exploitability_ease") == null ? null : r.Element("exploitability_ease").Value,
                           ExploitedByMalware = r.Element("exploited_by_malware") == null ? null : r.Element("exploited_by_malware").Value,
                           RiskFactor = r.Element("risk_factor") == null ? null : r.Element("risk_factor").Value,
                           SeeLAlso = r.Element("see_also") == null ? null : r.Element("see_also").Value,
                           Solution = r.Element("solution") == null ? null : r.Element("solution").Value,
                           Synopsis = r.Element("synopsis") == null ? null : r.Element("synopsis").Value,
                           PluginOutput = r.Element("plugin_output") == null ? null : r.Element("plugin_output").Value,

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


        public List<ScanMasterDTO> GetRecords()
        {
            List<ScanMasterDTO> scanMasters = new List<ScanMasterDTO>();
            var obj = new WebParser.DAL.DataFunction.OperationFunctions();
            scanMasters = obj.GetPreviousScanResult(HttpContext.Current.Session["UserName"] as string);
            //ScanMasterDTO a = new ScanMasterDTO();
            //a.ClientName = "aaa";
            //a.ScanName = "ff";
            //scanMasters.Add(a);
            grdScanList.DataSource = scanMasters;
            grdScanList.DataBind();
            return null;
        }


        protected void grdScanList_Load(object sender, EventArgs e)
        {

        }

        protected void NewScan_CheckedChanged(object sender, EventArgs e)
        {
            dvAdditionalScan.Visible = false;
            dvNewScan.Visible = true;
        }

        protected void AddtionalScan_CheckedChanged(object sender, EventArgs e)
        {
            dvAdditionalScan.Visible = true;
            dvNewScan.Visible = false;
            GetRecords();
        }

        protected void Unnamed_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void grdScanList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void grdScanList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //check the item being bound is actually a DataRow, if it is,
            //wire up the required html events and attach the relevant JavaScripts
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string id = e.Row.Cells[0].Text; // Get the id to be deleted
                //cast the ShowDeleteButton link to linkbutton
                Button lb = (Button)e.Row.Cells[4].Controls[0];
                if (lb != null)
                {
                    //attach the JavaScript function with the ID as the paramter
                    lb.Click += lb_Click;
                    lb.Attributes.Add("onclick", "return gridRowOnclick('" + lb.ClientID + "');");
                }
                //e.Row.Attributes["onclick"] =
                //    "javascript:gridRowOnclick(this);";
            }
        }

        protected void lb_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void btn1_Click(object sender, EventArgs e)
        {

        }

    }
}