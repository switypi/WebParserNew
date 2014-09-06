using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.DAL.Model
{
    [DataContract]
    public class ImportXMLDataDTO
    {
        [DataMember]
        public int ScanId { get; set; }
        [DataMember]
        public int SubScanId { get; set; }
        [DataMember]
        public string ReportHost { get; set; }
        [DataMember]
        public string Port { get; set; }
        [DataMember]
        public string PlugId { get; set; }
        [DataMember]
        public string Compliance { get; set; }
        [DataMember]
        public string ComplianceResult { get; set; }
        [DataMember]
        public string ComplianceActualValue { get; set; }
        [DataMember]
        public string ComplianceCheckID { get; set; }
        [DataMember]
        public string ComplianceOutPut { get; set; }
        [DataMember]
        public string CompliancePolicyValue { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string ExploitAvailable { get; set; }
        [DataMember]
        public string ExploitabilityEase { get; set; }
        [DataMember]
        public string RiskFactor { get; set; }
        [DataMember]
        public string SeeLAlso { get; set; }
        [DataMember]
        public string Solution { get; set; }
        [DataMember]
        public string Synopsis { get; set; }
        [DataMember]
        public bool PluginOutputReportable { get; set; }
        [DataMember]
        public string ExploitedByMalware { get; set; }
        [DataMember]
        public string PluginOutput { get; set; }
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public DateTime ScanDate { get; set; }
        [DataMember]
        public string ScanName { get; set; }

        [DataMember]
        public bool IsAdditionalScan { get; set; }

    }


}
