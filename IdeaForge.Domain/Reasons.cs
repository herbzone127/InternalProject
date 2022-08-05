using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Domain
{
    public class ReasonsResponse
    {
        public string messaage { get; set; }
        public bool status { get; set; }
        public List<Reason> userData { get; set; }
    }

    public class Reason
    {
        public int id { get; set; }
        public bool isActive { get; set; }
        public string reasonDescription { get; set; }
    }

}
