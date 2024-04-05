using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Entities
{
    public interface IExport
    {

        // Strategy Interface for Strategy pattern 
        public void Export(string data);
    }
}
