using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Patterns.Adapter.DevOpsAdapter
{
    public interface IDevOps
    {
        void Build();

        void Test();

        void Analyse();

        void Deploy();

        void GetSources();

        void GetPackages();

        void GetUtilities();
    }
}
