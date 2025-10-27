using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONApp.Model
{
    public class Usage
    {


        public string? CustomerId { get; set; } = string.Empty;
        public string aPI_Calls { get; set; } = string.Empty;
        public double Storage_GB { get; set; } = 0;
        public double? Compute_Minutes { get; set; } = 0;


    }
}
