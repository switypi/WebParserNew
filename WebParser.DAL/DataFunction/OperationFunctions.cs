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
        public bool ImportXmlData(List<ImportXMLDataDTO> inputDTOList)
        {
            int scanId = 0;
            int subScnaID = 0;
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
                context.ScanMasters.Add(master);
                foreach (var item in inputDTOList)
                {
                    CurrScan newItem = CreateCurrentScan(item, scanId, subScnaID);
                    context.CurrScans.Add(newItem);
                }
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
                    //var obj=from item in context.CurrScans
                    //        where item.Compliance ==0;
                    return true;
                }
                else
                    return false;
            }
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

        public List<ScanMasterDTO> GetPreviousScanResult(string userId)
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
    }
}
