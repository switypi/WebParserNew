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
    public partial class Admin : Page
    {
        private XLWorkbook excelworkBook;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var obj = new WebParser.DAL.DataFunction.OperationFunctions();
                var listOfData = obj.GetScanIds();
                drpOptionList.DataSource = listOfData;
                drpOptionList.DataTextField = "ScanName";
                drpOptionList.DataValueField = "ScanId";
                drpOptionList.DataBind();

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

        private void GenerateExcel(string val)
        {
            var obj = new WebParser.DAL.DataFunction.OperationFunctions();
            List<WebParser.DAL.Model.CurrScanDTO> List = new List<CurrScanDTO>();
            switch (int.Parse(val))
            {
                case 1:
                    List = obj.NewRegularScan(drpOptionList.SelectedValue);

                    break;
                case 2:
                    List = obj.NewComplianceData(drpOptionList.SelectedValue);
                    break;
                case 3:
                    List = obj.NewPluginOutputVarianceFirst(drpOptionList.SelectedValue);
                    break;
                case 4:
                    List = obj.NewPluginOutputVarianceSecond(drpOptionList.SelectedValue);
                    break;
                default:
                    break;

            }
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
                    worksheet.Cell(rowIndex + 1, indexOfItem + 1).Value = value;
                }
            }
            // Prepare the response
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"HelloWorld.xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                excelworkBook.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }

        protected void btnGenerateExcel_Click(object sender, EventArgs e)
        {
            GenerateExcel(drpOptionList.SelectedValue);
        }
    }
}