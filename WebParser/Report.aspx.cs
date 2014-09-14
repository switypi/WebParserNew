using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebParser.DAL.Model;

namespace WebParser
{
    public partial class Report : System.Web.UI.Page
    {
        private XLWorkbook excelworkBook;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var obj = new WebParser.DAL.DataFunction.OperationFunctions();
                var listOfData = obj.GetScanIds();
                drpScanList.DataSource = listOfData;
                drpScanList.DataTextField = "ScanName";
                drpScanList.DataValueField = "ScanId";
                drpScanList.DataBind();

            }
            if (bool.Parse(Session["IsAdmin"].ToString()))
            {
                HyperLink scnLoad = this.Master.FindControl("hypScn") as HyperLink;

                HyperLink rptLink = this.Master.FindControl("hypRpt") as HyperLink;

                HyperLink admn = this.Master.FindControl("hypAdmin") as HyperLink;
                rptLink.Visible = true;
                scnLoad.Visible = true;
                admn.Visible = true;
            }
            else
            {
                HyperLink scnLoad = this.Master.FindControl("hypScn") as HyperLink;

                HyperLink rptLink = this.Master.FindControl("hypRpt") as HyperLink;

                HyperLink admn = this.Master.FindControl("hypAdmin") as HyperLink;
                rptLink.Visible = true;
                scnLoad.Visible = true;
                admn.Visible = false;
            }
        }

        protected void drpOptionList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnGenerateExcel_Click(object sender, EventArgs e)
        {

            GenerateExcel(drpOptionList.SelectedValue);
        }
        private void GenerateExcel(string val)
        {
            var obj = new WebParser.DAL.DataFunction.OperationFunctions();
            List<WebParser.DAL.Model.CurrScanDTO> List = new List<CurrScanDTO>();
            switch (int.Parse(val))
            {
                case 1:
                    List = obj.GenerateRegularScanReport(int.Parse(drpScanList.SelectedValue));
                    if (List.Count > 0)
                        ShowExcelFile(List, "GenerateRegularScanReport");
                    else
                        lblNoRecords.Visible = true;
                    break;
                case 2:
                    List = obj.GenerateComplianceScanReport(int.Parse(drpScanList.SelectedValue));
                    if (List.Count > 0)
                        ShowExcelFile(List, "Generate Compliance Scan Report");
                    else
                        lblNoRecords.Visible = true;
                    break;
                case 3:
                    // obj.GenerateVulnerabilityScanReport(int.Parse(drpScanList.SelectedValue));
                    //if (List.Count > 0)
                    //    ShowExcelFile(List, "Generate Vulnerabilities Not Reported-1");
                    //else
                    //    lblNoRecords.Visible = true;
                    break;
                case 4:
                    List = obj.NewPluginOutputVarianceSecond(int.Parse(drpScanList.SelectedValue));
                    if (List.Count > 0)
                        ShowExcelFile(List, "PluginOutputVariance-2");
                    else
                        lblNoRecords.Visible = true;
                    break;
                default:


                    break;

            }

        }

        private void ShowExcelFile(List<WebParser.DAL.Model.CurrScanDTO> List, string fileName)
        {
            excelworkBook = new XLWorkbook();
            var worksheet = excelworkBook.Worksheets.Add("Sample Sheet");

            foreach (var item in List.ElementAt(0).GetType().GetProperties())
            {
                var indexOfItem = List.ElementAt(0).GetType().GetProperties().ToList().IndexOf(item);
                worksheet.Cell(1, indexOfItem + 1).Value = item.Name;
            }

            //Print Data;
            foreach (var itemRow in List)
            {
                var rowIndex = List.IndexOf(itemRow) + 1;
                foreach (var col in itemRow.GetType().GetProperties())
                {
                    var indexOfItem = List.ElementAt(0).GetType().GetProperties().ToList().IndexOf(col);
                    var value = typeof(CurrScanDTO).GetProperty(col.Name).GetValue(itemRow, null);

                    worksheet.Cell(rowIndex + 1, indexOfItem + 1).Value = value == null ? null : (value.ToString().Count() >= 32000 ? value.ToString().Substring(0, 32000) : value);
                    if (col.Name == "PluginId")
                    {
                        worksheet.Cell(rowIndex + 1, indexOfItem + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        worksheet.Cell(rowIndex + 1, indexOfItem + 1).DataType = XLCellValues.Number;
                        worksheet.Cell(rowIndex + 1, indexOfItem + 1).Style.NumberFormat.NumberFormatId = 1;

                    }
                }
            }
            // Prepare the response
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=" + fileName + ".xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                excelworkBook.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }
    }
}