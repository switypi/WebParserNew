using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.DAL.Model
{
    [DataContract]
    public class MasterPluginDTO
    {
        [DataMember]
        public double PluginId { get; set; }
        [DataMember]
        public string Synopsis { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public bool Reportable { get; set; }
        [DataMember]
        public string PluginOutPut { get; set; }
        [DataMember]
        public bool PluginOutPutReportable { get; set; }
        [DataMember]
        public string Riskfactor { get; set; }
        [DataMember]
        public string Category1 { get; set; }
        [DataMember]
        public string Category2 { get; set; }
        [DataMember]
        public string Category3 { get; set; }
        [DataMember]
        public string Solution { get; set; }
        [DataMember]
        public string ComplianceCheckID { get; set; }
    }
}
