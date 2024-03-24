using Domain.Core.Entities;
using Domain.Core.Entities.Sprint;
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

        public Report NewReport(ISprint sprint)
        {
            return new Report(sprint);
        }

        public void Decorate(Report report, string ftr, string hdr)
        {
            report.Ftr = ftr;
            report.Hdr = hdr;
        }

        //Implements Strategy Pattern
        public void SetStrategy(Report report, IExport export)
        {
            report.Export = export;
        }

        public void Export(Report report)
        {
            if (report.Export != null) {
                report.Export.Export(Generate(report));
            }
            else
            {
                Console.WriteLine("Please select a export method");
            }
        }

        // FR - 09
        public string Generate(Report report)
        {
            string x = "";
            x += report.Hdr;
            x += "\nScrummaster : " + report.Sprint.Scrummaster.Name;
            x += "\nDevs : ";
            foreach (User dev in report.Sprint.Users)
            {
                x += dev.Name;
            }
            x += report.Ftr;

            return x;
        }
    }
}
