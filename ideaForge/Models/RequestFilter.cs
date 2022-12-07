using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ideaForge.Models
{
    public enum Status
    {
        Ongoing,
        Upcoming,
        Pending,
        Rejected,
        Completed,
        Cancelled,
        EndFlight,
        AutoCancelled,
        Accepted
    }
    public class RequestFilter
    {
        public List<Status> SelectedStatus { get; set; }
        public string SearchLocation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
