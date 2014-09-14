using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.DAL.Model
{
    [DataContract]
    public class MasterComplianceDTO
    {
        [DataMember]
        public int PluginId { get; set; }
        [DataMember]
        public string ComplianceCheckID { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public bool Reportable { get; set; }

        [DataMember]
        public string Riskfactor { get; set; }
        [DataMember]
        public string Category1 { get; set; }
        [DataMember]
        public string Category2 { get; set; }
        [DataMember]
        public string Category3 { get; set; }
    }
}
