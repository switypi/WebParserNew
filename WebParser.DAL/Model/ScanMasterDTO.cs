using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.DAL.Model
{
    [DataContract]
    public class ScanMasterDTO
    {
        [DataMember]
        public int ScanID { get; set; }
        [DataMember]
        public int SubScanID { get; set; }
        [DataMember]
        public string ScanName { get; set; }
        [DataMember]
        public string ScanDate { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public int Id { get; set; }
    }
}
