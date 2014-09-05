using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.DAL.Model
{
    [DataContract]
    public class LoginDTO
    {
        [DataMember]
        public bool IsValidLogin { get; set; }
        [DataMember]
        public bool IsAdmin { get; set; }
    }
}
