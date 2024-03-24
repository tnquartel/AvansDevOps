using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities;

namespace Infrastructure.Services.Exporters
{
    public class PdfExporter : IExport
    {
        public void Export()
        {
            Console.WriteLine("Exported to PDF");
        }
    }
}
