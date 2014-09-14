using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.DAL.Model
{
    [DataContract]
    public class CurrScanDTO
    {
        [DataMember]
        public double PluginId { get; set; }
        [DataMember]
        public string Synopsis { get; set; }
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public bool ExploitAvailable { get; set; }
        [DataMember]
        public string ExploitabilityEase { get; set; }
        [DataMember]
        public bool ExploitedByMalware { get; set; }
       
        [DataMember]
        public string RiskFactor { get; set; }
        [DataMember]
        public string Solution { get; set; }
        [DataMember]
        public string SeeAlso { get; set; }
        [DataMember]
        public string PluginOutput { get; set; }
        [DataMember]
        public string ComplianceCheckID { get; set; }
        [DataMember]
        public bool PluginOutPutReportable { get; set; }
        [DataMember]
        public int Port { get; set; }
        [DataMember]
        public string ReportHost { get; set; }
        [DataMember]
        public string ComplianceResult { get; set; }
        [DataMember]
        public string ComplianceActualValue { get; set; }
        [DataMember]
        public string ComplianceOutPut { get; set; }
        [DataMember]
        public string CompliancePolicyValue { get; set; }


    }
}
