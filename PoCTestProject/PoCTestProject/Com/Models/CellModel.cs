using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoCTestProject.Com.Models
{
    class CellModel
    {
        public string cellName { get; set; }
        public string totalRequirements { get; set; }
        public string totalVulnerabilities { get; set; }
        public string redRequirements { get; set; }
        public string redVulnerabilities { get; set; }
        public string amberRequirements { get; set; }
        public string amberVulnerabilities { get; set; }
        public string greenRequirements { get; set; }
        public string greenVulnerabilities { get; set; }
        public string noneRequirements { get; set; }
        public string noneVulnerabilities { get; set; }
        public string unassignedRequirements { get; set; }
        public string unassignedVulnerabilities { get; set; }
    }
}
