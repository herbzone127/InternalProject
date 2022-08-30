using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ideaForge.Models
{
    public class ReportDetailsToExcel
    {
        public int BookingId { get; set; }
        public string UserName { get; set; }
        public string ContactNo { get; set; }
        public string IFDock { get; set; }
        public string DateTime { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Status { get; set; }

    }
}
