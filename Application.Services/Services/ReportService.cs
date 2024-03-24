using Domain.Core.Entities;
using Domain.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Services
{
    public class ReportService : IReportService
    {

        //Implements Strategy Pattern
        public void SetStrategy(Report report, IExport export)
        {
            report.export = export;
        }

        public void Export(Report report)
        {
            if (report.export != null) {
                report.export.Export();
            }
            else
            {
                Console.WriteLine("Please select a export method");
            }
        }
    }
}
