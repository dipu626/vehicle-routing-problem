using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Entities
{
    public class BaseNode
    {
        public int Node { get; set; } = 0;
        public long Load { get; set; } = 0L;
        public long Distance { get; set; } = 0L;
    }
}
