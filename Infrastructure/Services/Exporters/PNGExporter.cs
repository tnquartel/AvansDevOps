using Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Exporters
{
    public class PngExporter : IExport
    {
        public void Export()
        {
            Console.WriteLine("Exported to PNG");
        }
    }
}
