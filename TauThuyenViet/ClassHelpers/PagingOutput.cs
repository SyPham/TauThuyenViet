using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TauThuyenViet.ClassHelpers
{
    public class PagingOutput
    {
        public int Page { get; set; }
        public string PageFirstUrl { get; set; }
        public string PageLastUrl { get; set; }
        public string PageBackUrl { get; set; }
        public string PageNextUrl { get; set; }
    }
}
