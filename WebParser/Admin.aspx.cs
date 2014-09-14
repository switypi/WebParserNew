using ClosedXML.Excel;
using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
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
            if (int.Parse(drpOptionList.SelectedValue) > 4)
            {
                pnlUpload.Visible = true;
                btnUpload.Visible = true;
                btnGenerateExcel.Visible = false;
            }
            else
            {
                pnlUpload.Visible = false;
                btnUpload.Visible = false;
                btnGenerateExcel.Visible = true;
            }

        }

        private void GenerateExcel(string val)
        {
            var obj = new WebParser.DAL.DataFunction.OperationFunctions();
            List<WebParser.DAL.Model.CurrScanDTO> List = new List<CurrScanDTO>();
            switch (int.Parse(val))
            {
                case 1:
                    List = obj.NewRegularScan(int.Parse(drpScanList.SelectedValue));
                    if (List.Count > 0)
                        ShowExcelFile(List, "NewPluginData");
                    else
                        lblNoRecords.Visible = true;
                    break;
                case 2:
                    List = obj.NewComplianceData(int.Parse(drpScanList.SelectedValue));
                    if (List.Count > 0)
                        ShowExcelFile(List, "NewComplianceData");
                    else
                        lblNoRecords.Visible = true;
                    break;
                case 3:
                    List = obj.NewPluginOutputVarianceFirst(int.Parse(drpScanList.SelectedValue));
                    if (List.Count > 0)
                        ShowExcelFile(List, "PluginOutputVariance-1");
                    else
                        lblNoRecords.Visible = true;
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

        private void ValidateExcelReport(string val)
        {

        }

        protected void btnGenerateExcel_Click(object sender, EventArgs e)
        {
            GenerateExcel(drpOptionList.SelectedValue);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            List<string> columnNmaes;
            DataSet result;
            List<MasterPluginDTO> ItemList;
            List<MasterComplianceDTO> complianceDTO;
            List<int> pluginIds;
            switch (int.Parse(drpOptionList.SelectedValue))
            {

                case 5:
                    {
                        columnNmaes = new List<string>();
                        columnNmaes.Add("PluginID");
                        columnNmaes.Add("Synopsis");
                        columnNmaes.Add("Description");
                        columnNmaes.Add("ExploitAvailable");
                        columnNmaes.Add("ExploitabilityEase");
                        columnNmaes.Add("ExploitedByMalwar");
                        columnNmaes.Add("RiskFactor");
                        columnNmaes.Add("Solution");
                        columnNmaes.Add("SeeAlso");
                        columnNmaes.Add("PluginOutput");

                        result = ValidateColumnInSheet(columnNmaes);
                        ItemList = result.Tables[0].ToList<MasterPluginDTO>();
                        pluginIds = ItemList.Select(c => c.PluginId).ToList();
                        pluginIds = pluginIds.Distinct().ToList();
                        ItemList = ItemList.Where(c => pluginIds.Contains(c.PluginId)).GroupBy(c => c.PluginId).Select(v => v.First()).ToList();
                        var obj = new WebParser.DAL.DataFunction.OperationFunctions();
                        obj.UpdateMasterPluginData(ItemList);

                    }
                    break;
                case 6:
                    columnNmaes = new List<string>();
                    columnNmaes.Add("PluginID");
                    columnNmaes.Add("ComplianceCheckID");
                    columnNmaes.Add("Description");
                    columnNmaes.Add("RiskFactor");
                    columnNmaes.Add("PluginOutput");

                    result = ValidateColumnInSheet(columnNmaes);
                    complianceDTO = result.Tables[0].ToList<MasterComplianceDTO>();
                    pluginIds = complianceDTO.Select(c => c.PluginId).ToList();
                    pluginIds = pluginIds.Distinct().ToList();
                    complianceDTO = complianceDTO.Where(c => pluginIds.Contains(c.PluginId)).GroupBy(c => new { c.ComplianceCheckID, c.PluginId }).Select(v => v.First()).ToList();
                    var objCon = new WebParser.DAL.DataFunction.OperationFunctions();
                    objCon.UpdateMasterCompliance(complianceDTO);


                    break;
                case 7:
                    columnNmaes = new List<string>();
                    columnNmaes.Add("PluginID");
                    columnNmaes.Add("Synopsis");
                    columnNmaes.Add("Description");
                    columnNmaes.Add("PluginOutput");
                    result = ValidateColumnInSheet(columnNmaes);

                    ItemList = result.Tables[0].AsEnumerable().Select(s => new MasterPluginDTO()
                         {
                             PluginOutPutReportable = s.Field<bool>("PluginOutputReportable"),
                             PluginId = s.Field<int>("PluginId"),
                         }).ToList();


                    pluginIds = ItemList.Select(c => c.PluginId).ToList();
                    pluginIds = pluginIds.Distinct().ToList();
                    ItemList = ItemList.Where(c => pluginIds.Contains(c.PluginId)).GroupBy(c => c.PluginId).Select(v => v.First()).ToList();
                    var objdataCon = new WebParser.DAL.DataFunction.OperationFunctions();
                    objdataCon.UpdatePluginVariance1(ItemList);
                    break;
                case 8:
                    columnNmaes = new List<string>();
                    columnNmaes.Add("PluginID");
                    columnNmaes.Add("ComplianceCheckID");
                    columnNmaes.Add("Description");
                    columnNmaes.Add("PluginOutput");
                    result = ValidateColumnInSheet(columnNmaes);
                    ItemList = result.Tables[0].AsEnumerable().Select(s => new MasterPluginDTO()
                        {
                            PluginOutPutReportable = s.Field<bool>("PluginOutputReportable"),
                            ComplianceCheckID = s.Field<string>("ComplianceCheckID"),
                            PluginId = s.Field<int>("PluginId"),
                        }).ToList();


                    pluginIds = ItemList.Select(c => c.PluginId).ToList();
                    pluginIds = pluginIds.Distinct().ToList();
                    ItemList = ItemList.Where(c => pluginIds.Contains(c.PluginId)).GroupBy(c => new { c.ComplianceCheckID, c.PluginId }).Select(v => v.First()).ToList();
                    var objdataConection = new WebParser.DAL.DataFunction.OperationFunctions();
                    objdataConection.UpdatePluginVariance2(ItemList);
                    break;
            }
        }

        private DataSet ValidateColumnInSheet(List<string> columnNmaes)
        {
            FileStream stream = File.Open(fileUpload1.PostedFile.FileName, FileMode.Open, FileAccess.Read);

            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            excelReader.IsFirstRowAsColumnNames = true;
            DataSet result = excelReader.AsDataSet();
            foreach (string col in columnNmaes)
            {
                int index = columnNmaes.IndexOf(col);
                if (!(result.Tables[0].Columns[index].ColumnName.Equals(col, StringComparison.InvariantCultureIgnoreCase)))
                {
                    lblNoRecords.Text = "Mismatch found.";
                    lblNoRecords.Visible = true;
                    break;
                }
            }
            excelReader.Close();

            return result;
        }


    }
}