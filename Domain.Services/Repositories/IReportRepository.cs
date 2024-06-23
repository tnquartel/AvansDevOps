using Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Repositories
{
    public interface IReportRepository
    {
        public Report GetOne(int id);
        public List<Report> GetAll();
        public void Update(int id, Report update);
        public void Delete(int id);
        public void Create(Report newReport);
    }
}
