using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holidays.Models
{

   public class Holiday
    {
        public int id { get; set; }
        public string date { get; set; }
        public string nameEn { get; set; }
        public string nameFr { get; set; }
        public int federal { get; set; }
        public string observedDate { get; set; }
        public List<Province> provinces { get; set; }

    }
}
