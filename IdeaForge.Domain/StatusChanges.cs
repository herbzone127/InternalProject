using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Domain
{
    public class StatusChanges
    {
        public string isActivated { get; set; }

    }
    public class StatusChangesResponse
    {
        public string message { get; set; }
        public bool status { get; set; }
    }

}
