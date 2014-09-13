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
        public int PluginId { get; set; }
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


    }
}
