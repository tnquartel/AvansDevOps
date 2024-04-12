using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Patterns.Adapter.DevOpsAdapter
{
    public interface IDevOpsAdapter
    {
        void Build();

        bool Test(bool passed);

        void Analyse();

        void Deploy();

        void GetSources();

        void GetPackages();

        void GetUtilities();
    }
}
