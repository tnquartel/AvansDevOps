using Domain.Core.Entities;
using Domain.Core.Entities.Sprint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Services
{
    public interface IReportService
    {
        public Report NewReport(ISprint sprint);
        public void SetStrategy(Report report, IExport export);
        public void Export(Report report);
        public void Decorate(Report report, string ftr, string hdr);

    }
}
