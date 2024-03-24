using Domain.Core.Entities;
using Domain.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class InMemoryReportRepo : IReportRepository
    {
        private List<Report> y = new List<Report>();

        public void Create(Report newReport)
        {
            y.Add(newReport);
        }

        public void Delete(int id)
        {
            var x = from report in y where report.Id == id select report;
            y.Remove(x.First());
        }

        public List<Report> GetAll()
        {
            return y;
        }

        public Report GetOne(int id)
        {
            var x = from report in y where report.Id == id select report;
            return x.First();
        }

        public void Update(int id, Report update)
        {
            var x = y.FirstOrDefault(x => x.Id == id);
            x = update;
        }

    }
}
