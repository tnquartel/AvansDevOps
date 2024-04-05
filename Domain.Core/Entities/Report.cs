using Domain.Core.Entities.Sprint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public IExport? Export { get; set; }
        public ISprint Sprint { get; set; }
        public string? Hdr { get; set; }
        public string? Ftr { get; set; }
        public Report(ISprint sprint) {
            Sprint = sprint;
        }
    }
}
