//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebParser.DAL.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class CurrScan
    {
        public int ScanID { get; set; }
        public int SubScanID { get; set; }
        public string ReportHost { get; set; }
        public int Port { get; set; }
        public int PluginID { get; set; }
        public bool Compliance { get; set; }
        public string ComplianceResult { get; set; }
        public string ComplianceActualValue { get; set; }
        public string ComplianceCheckID { get; set; }
        public string ComplianceOutput { get; set; }
        public string CompliancePolicyValue { get; set; }
        public string Description { get; set; }
        public bool ExploitAvailable { get; set; }
        public string ExploitabilityEase { get; set; }
        public bool ExploitedByMalware { get; set; }
        public string RiskFactor { get; set; }
        public string SeeAlso { get; set; }
        public string Solution { get; set; }
        public string Synopsis { get; set; }
        public string PluginOutput { get; set; }
        public bool PluginOutputReportable { get; set; }
        public int Id { get; set; }
        public Nullable<int> ScanMasterId { get; set; }
    
        public virtual ScanMaster ScanMaster { get; set; }
    }
}
