using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebParser.DAL.DataModel;
using WebParser.DAL.Model;

namespace WebParser.DAL.DataFunction
{
    public class OperationFunctions
    {
        public ReturnResultDTO ImportXmlData(List<ImportXMLDataDTO> inputDTOList)
        {
            int scanId = 0;
            int subScnaID = 0;
            ReturnResultDTO dtoItem;
            if (!inputDTOList.Any(c => c.IsAdditionalScan))
            {
                //Generate New ScanID;
                scanId = new Random().Next(1000, 10000);
                subScnaID = 1;
            }
            else
            {
                scanId = inputDTOList.First().ScanId;
                subScnaID = inputDTOList.First().SubScanId + 1;
            }
            //Create MasterScan
            ScanMaster master = CreateScanMaster(scanId, subScnaID, inputDTOList.First().UserId, inputDTOList.First().ClientName, inputDTOList.First().ScanDate, inputDTOList.First().ScanName);


            using (var context = new WebParser.DAL.DataModel.WebParserEntities())
            {

                foreach (var item in inputDTOList)
                {
                    CurrScan newItem = CreateCurrentScan(item, scanId, subScnaID);
                    master.CurrScans.Add(newItem);
                    //context.CurrScans.Add(newItem);
                }
                context.ScanMasters.Add(master);
                int value = 0;
                try
                {
                    value = context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }

                if (value > 0)
                {

                    dtoItem = CheckExistingData();
                    dtoItem.IsSuccess = true;

                }
                else
                {
                    dtoItem = new ReturnResultDTO();
                    dtoItem.IsSuccess = false;
                }

            }
            return dtoItem;
        }

        private static CurrScan CreateCurrentScan(ImportXMLDataDTO inputDTO, int scanId, int subScnaID)
        {
            CurrScan newItem = new CurrScan();
            newItem.Compliance = string.IsNullOrEmpty(inputDTO.Compliance) == true ? false : bool.Parse(inputDTO.Compliance);
            newItem.ComplianceActualValue = inputDTO.ComplianceActualValue;
            newItem.ComplianceCheckID = inputDTO.ComplianceCheckID;
            newItem.ComplianceOutput = inputDTO.ComplianceOutPut;
            newItem.CompliancePolicyValue = inputDTO.CompliancePolicyValue;
            newItem.ComplianceResult = inputDTO.ComplianceResult;
            newItem.Description = inputDTO.Description;
            newItem.ExploitabilityEase = inputDTO.ExploitabilityEase;
            newItem.ExploitAvailable = string.IsNullOrEmpty(inputDTO.ExploitAvailable) == true ? false : bool.Parse(inputDTO.ExploitAvailable); ;
            newItem.ExploitedByMalware = string.IsNullOrEmpty(inputDTO.ExploitedByMalware) == true ? false : bool.Parse(inputDTO.ExploitedByMalware);
            newItem.PluginID = string.IsNullOrEmpty(inputDTO.PlugId) == true ? 0 : int.Parse(inputDTO.PlugId);
            newItem.PluginOutput = inputDTO.PluginOutput;
            newItem.PluginOutputReportable = inputDTO.PluginOutputReportable;
            newItem.Port = string.IsNullOrEmpty(inputDTO.Port) == true ? 0 : int.Parse(inputDTO.Port); ;
            newItem.ReportHost = inputDTO.ReportHost;
            newItem.RiskFactor = inputDTO.RiskFactor;
            newItem.ScanID = scanId;
            newItem.SeeAlso = inputDTO.SeeLAlso;
            newItem.Solution = inputDTO.Solution;
            newItem.SubScanID = subScnaID;
            newItem.Synopsis = inputDTO.Synopsis;
            return newItem;
        }

        private ScanMaster CreateScanMaster(int scanID, int subScanID, string userID, string clientName, DateTime scanDate, string scanName)
        {
            ScanMaster master = new ScanMaster();
            master.ClientName = clientName;
            master.ScanDate = scanDate;
            master.ScanId = scanID;
            master.ScanName = scanName;
            master.SubScanId = subScanID;
            master.UserId = userID;
            return master;
        }

        public List<ScanMasterDTO> GetPreviousScanResult(string userId, string scanId = "")
        {
            List<ScanMasterDTO> scanMasterList;

            using (var context = new WebParser.DAL.DataModel.WebParserEntities())
            {
                scanMasterList = (from item1 in context.ScanMasters
                                  where item1.UserId == userId
                                  select item1).ToList().Select(item => new ScanMasterDTO()
                                  {
                                      Id = item.Id,
                                      ClientName = item.ClientName,
                                      ScanDate = item.ScanDate.ToString(),
                                      ScanID = item.ScanId,
                                      ScanName = item.ScanName,
                                      SubScanID = item.SubScanId

                                  }).ToList();
            }
            return scanMasterList;
        }
        public List<ScanMasterDTO> GetsScanResultByScanId(string userId, int scanId)
        {
            List<ScanMasterDTO> scanMasterList;

            using (var context = new WebParser.DAL.DataModel.WebParserEntities())
            {
                scanMasterList = (from item1 in context.ScanMasters
                                  where item1.UserId == userId && item1.ScanId == scanId
                                  select item1).ToList().Select(item => new ScanMasterDTO()
                                  {
                                      Id = item.Id,
                                      ClientName = item.ClientName,
                                      ScanDate = item.ScanDate.ToString(),
                                      ScanID = item.ScanId,
                                      ScanName = item.ScanName,
                                      SubScanID = item.SubScanId

                                  }).ToList();
            }
            return scanMasterList;
        }

        private ReturnResultDTO CheckExistingData()
        {
            ReturnResultDTO dto = new ReturnResultDTO();
            using (var context = new WebParserEntities())
            {
                List<int> plugins = context.MasterPlugins.Select(x => x.PluginID).ToList();
                if (context.CurrScans.Where(c => plugins.Contains(c.PluginID) == false && c.Compliance == null).Count() > 0)
                    dto.Message = "New Plugins found. Please update";
                if (dto.Message == "")
                {
                    List<string> complianceCheckIDList = context.ComplianceMasters.Select(c => c.ComplianceCheckID).ToList();
                    if (context.CurrScans.Where(c => complianceCheckIDList.Contains(c.ComplianceCheckID) == false && c.Compliance == false).Count() > 0)
                        dto.Message = "New Compliance Checks found. Please update";
                }
                if (dto.Message == "")
                {
                    List<MasterPlugin> masterPlugindata = context.MasterPlugins.Where(v => v.PluginOutputReportable == true).ToList();
                    var count = (from item in context.CurrScans
                                 join plg in masterPlugindata on item.PluginID equals plg.PluginID
                                 where item.PluginOutput != plg.PluginOutPut
                                 select item).Count();
                    if (count > 0)
                        dto.Message = "Plugin output variance found.Please review.";
                }
                if (dto.Message == "")
                    dto.Message = "Proceed to report generation";
                return dto;
            }

        }

        public List<ScanMasterDTO> GetScanIds()
        {
            List<ScanMasterDTO> scanMasterList;

            using (var context = new WebParser.DAL.DataModel.WebParserEntities())
            {
                scanMasterList = (from item1 in context.ScanMasters
                                  select item1).ToList().Select(item => new ScanMasterDTO()
                                  {
                                      Id = item.Id,
                                      ClientName = item.ClientName,
                                      ScanDate = item.ScanDate.ToString(),
                                      ScanID = item.ScanId,
                                      ScanName = item.ScanName,
                                      SubScanID = item.SubScanId

                                  }).ToList();
            }
            return scanMasterList;
        }

        public List<CurrScanDTO> NewRegularScan(int scanId)
        {
            List<CurrScanDTO> datalist = new List<CurrScanDTO>();

            using (var context = new WebParser.DAL.DataModel.WebParserEntities())
            {
                List<int> plugins = context.MasterPlugins.Select(x => x.PluginID).ToList();

                datalist = (from item in context.CurrScans.Where(c => plugins.Contains(c.PluginID) == false && c.Compliance == false && c.ScanID == scanId)
                            orderby item.PluginID
                            select new CurrScanDTO()
                            {
                                Description = item.Description,
                                ExploitabilityEase = item.ExploitabilityEase,
                                ExploitAvailable = item.ExploitAvailable,
                                ExploitedByMalware = item.ExploitedByMalware,
                                PluginId = item.PluginID,
                                PluginOutput = item.PluginOutput,
                                RiskFactor = item.RiskFactor,
                                SeeAlso = item.SeeAlso,
                                Solution = item.Solution,
                                Synopsis = item.Synopsis,
                                PluginOutPutReportable = item.PluginOutputReportable,

                            }).ToList();

            }
            return datalist;
        }

        public List<CurrScanDTO> NewComplianceData(int scanId)
        {
            List<CurrScanDTO> datalist = new List<CurrScanDTO>();

            using (var context = new WebParser.DAL.DataModel.WebParserEntities())
            {
                List<string> complianceCheckIDList = context.ComplianceMasters.Select(c => c.ComplianceCheckID).ToList();

                datalist = (from item in context.CurrScans.Where(c => complianceCheckIDList.Contains(c.ComplianceCheckID) == false && c.Compliance != true && c.ScanID == scanId)
                            orderby item.PluginID, item.ComplianceCheckID
                            select new CurrScanDTO()
                            {
                                Description = item.Description,
                                PluginId = item.PluginID,
                                PluginOutput = item.PluginOutput,
                                RiskFactor = item.RiskFactor,
                                ComplianceCheckID = item.ComplianceCheckID

                            }).ToList();

            }
            return datalist;
        }

        public List<CurrScanDTO> NewPluginOutputVarianceFirst(int scanId)
        {

            List<CurrScanDTO> datalist = new List<CurrScanDTO>();

            using (var context = new WebParser.DAL.DataModel.WebParserEntities())
            {
                List<MasterPlugin> masterPlugindata = context.MasterPlugins.Where(v => v.PluginOutputReportable == true).ToList();

                List<int> plgIds = masterPlugindata.Select(c => c.PluginID).ToList();
                List<CurrScan> crsData = context.CurrScans.Where(c => plgIds.Contains(c.PluginID) && c.Compliance == false && c.ScanID == scanId).ToList();

                datalist = (from item in crsData
                            join plg in masterPlugindata on item.PluginID equals plg.PluginID
                            where item.PluginOutput != (plg.PluginOutPut == null ? string.Empty : plg.PluginOutPut)
                            orderby item.PluginID
                            select new CurrScanDTO()
                            {
                                Description = item.Description,
                                PluginId = item.PluginID,
                                PluginOutput = item.PluginOutput,
                                Synopsis = item.Synopsis

                            }).ToList();

            }
            return datalist;
        }

        public List<CurrScanDTO> NewPluginOutputVarianceSecond(int scanId)
        {
            List<CurrScanDTO> datalist = new List<CurrScanDTO>();

            using (var context = new WebParser.DAL.DataModel.WebParserEntities())
            {
                List<MasterPlugin> masterPlugindata = context.MasterPlugins.Where(v => v.PluginOutputReportable == true).ToList();
                List<int> plgIds = masterPlugindata.Select(c => c.PluginID).ToList();
                List<CurrScan> crsData = context.CurrScans.Where(c => plgIds.Contains(c.PluginID) && c.Compliance == false && c.ScanID == scanId).ToList();

                datalist = (from item in crsData
                            join plg in masterPlugindata on item.PluginID equals plg.PluginID
                            where item.PluginOutput != (plg.PluginOutPut == null ? string.Empty : plg.PluginOutPut)
                            orderby item.PluginID
                            select new CurrScanDTO()
                            {
                                Description = item.Description,
                                PluginId = item.PluginID,
                                PluginOutput = item.PluginOutput,
                                ComplianceCheckID = item.ComplianceCheckID

                            }).ToList();

            }
            return datalist;
        }

        public ReturnResultDTO UpdateMasterPluginData(List<MasterPluginDTO> input)
        {
            //List<int> pluginIds=input.Select(c=>c.
            ReturnResultDTO dt = new ReturnResultDTO();
            try
            {
                bool isUpdated = false;
                int plgId = 0;
                using (var context = new WebParserEntities())
                {
                    input.ForEach(c =>
                    {
                        plgId = int.Parse(c.PluginId.ToString());
                        var data = context.MasterPlugins.FirstOrDefault(v => v.PluginID == plgId);
                        if (data != null)
                        {
                            isUpdated = true;
                            data.Description = c.Description;
                            data.Synopsis = c.Synopsis;
                            data.PluginOutPut = c.PluginOutPut;
                            data.RiskFactor = c.Riskfactor;
                            data.PluginOutputReportable = c.PluginOutPutReportable;
                            data.Reportable = c.Reportable;
                            data.Solution = c.Solution;
                        }
                    });
                    if (isUpdated)
                    {
                        context.SaveChanges();
                        dt.Message = "Update successfull.";
                        dt.IsSuccess = true;
                    }
                    else
                    {
                        dt.Message = "No matchin plugin found.";
                        dt.IsSuccess = true;
                    }
                    return dt;

                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ReturnResultDTO UpdateMasterCompliance(List<MasterComplianceDTO> input)
        {
            //List<int> pluginIds=input.Select(c=>c.
            ReturnResultDTO dt = new ReturnResultDTO();
            bool isUpdated = false;
            try
            {
                int plgId = 0;
                using (var context = new WebParserEntities())
                {
                    input.ForEach(c =>
                    {
                        plgId = int.Parse(c.PluginId.ToString());
                        var data = context.ComplianceMasters.FirstOrDefault(v => v.PluginId == plgId);
                        if (data != null)
                        {
                            isUpdated = true;
                            data.Description = c.Description;
                            data.Reportable = c.Reportable;
                            data.RiskFactor = c.Riskfactor;
                            data.Category1 = c.Category1;
                            data.Category2 = c.Category2;

                        }
                    });
                    if (isUpdated)
                    {
                        context.SaveChanges();
                        dt.Message = "Update successfull.";
                        dt.IsSuccess = true;
                    }
                    else
                    {
                        dt.Message = "No matchin plugin found.";
                        dt.IsSuccess = true;
                    }
                    return dt;

                }
            }
            catch (Exception)
            {

                throw;
            }
            //}
        }

        public ReturnResultDTO UpdatePluginVariance1(List<MasterPluginDTO> input)
        {
            //List<int> pluginIds=input.Select(c=>c.
            ReturnResultDTO dt = new ReturnResultDTO();
            bool isUpdated = false;
            try
            {
                int plgId = 0;
                using (var context = new WebParserEntities())
                {
                    input.ForEach(c =>
                    {
                        plgId = int.Parse(c.PluginId.ToString());
                        var data = context.CurrScans.FirstOrDefault(v => v.PluginID == plgId);
                        if (data != null)
                        {
                            isUpdated = true;
                            data.PluginOutputReportable = c.PluginOutPutReportable;

                        }
                    });
                    if (isUpdated)
                    {
                        context.SaveChanges();
                        dt.Message = "Update successfull.";
                        dt.IsSuccess = true;
                    }
                    else
                    {
                        dt.Message = "No matchin plugin found.";
                        dt.IsSuccess = true;
                    }
                    return dt;

                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ReturnResultDTO UpdatePluginVariance2(List<MasterPluginDTO> input)
        {
            //List<int> pluginIds=input.Select(c=>c.
            ReturnResultDTO dt = new ReturnResultDTO();
            bool isUpdated = false;
            int plgId = 0;
            try
            {
                using (var context = new WebParserEntities())
                {
                    input.ForEach(c =>
                    {
                        plgId = int.Parse(c.PluginId.ToString());
                        var data = context.CurrScans.FirstOrDefault(v => v.PluginID == plgId && v.ComplianceCheckID == c.ComplianceCheckID);
                        if (data != null)
                        {
                            isUpdated = true;
                            data.PluginOutputReportable = c.PluginOutPutReportable;

                        }
                    });
                    if (isUpdated)
                    {
                        context.SaveChanges();
                        dt.Message = "Update successfull.";
                        dt.IsSuccess = true;
                    }
                    else
                    {
                        dt.Message = "No matchin plugin found.";
                        dt.IsSuccess = true;
                    }
                    return dt;

                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<CurrScanDTO> GenerateRegularScanReport(int scanId)
        {
            using (var context = new WebParserEntities())
            {
                List<CurrScanDTO> data = (from item in context.MasterPlugins
                                          join curr in context.CurrScans on item.PluginID equals curr.PluginID
                                          where item.Reportable == true && curr.Compliance == false && curr.ScanID == scanId
                                          orderby item.RiskFactor, item.Category1, item.Category2, item.Category3
                                          select new CurrScanDTO()
                                          {
                                              Synopsis=item.Synopsis,
                                              Description=item.Description,
                                              RiskFactor=item.RiskFactor,
                                              Solution=item.Solution,
                                              Port=curr.Port,
                                              ReportHost=curr.ReportHost
                                              
                                          }).ToList();
                return data;
            }
        }

        public List<CurrScanDTO> GenerateComplianceScanReport(int scanId)
        {
            using (var context = new WebParserEntities())
            {
                List<CurrScanDTO> data = (from item in context.ComplianceMasters
                                          join curr in context.CurrScans on item.PluginId equals curr.PluginID
                                          where item.Reportable == true && curr.Compliance ==true && curr.ScanID == scanId && item.ComplianceCheckID==curr.ComplianceCheckID
                                          orderby item.RiskFactor, item.Category1, item.Category2, item.Category3
                                          select new CurrScanDTO()
                                          {
                                              ComplianceCheckID = curr.ComplianceCheckID,

                                              Description = item.Description,
                                              RiskFactor = item.RiskFactor,
                                              Port = curr.Port,
                                              ReportHost = curr.ReportHost,
                                              PluginOutput=curr.PluginOutput,
                                              ComplianceActualValue=curr.ComplianceActualValue,
                                              ComplianceOutPut=curr.ComplianceOutput,
                                              CompliancePolicyValue=curr.CompliancePolicyValue,
                                              ComplianceResult=curr.ComplianceResult

                                          }).ToList();
                return data;
            }
        }

        public void GenerateVulnerabilityScanReport(int scanId)
        {
           
        }

    }
}
