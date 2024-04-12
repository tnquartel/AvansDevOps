using Domain.Services.Patterns.Adapter.DevOpsAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Adapters.DevOps
{
    public class DevOpsAdapter : IDevOpsAdapter
    {

        private Azure devops;

        public DevOpsAdapter()
        {
            this.devops = new Azure();
        }

        public void Build()
        {
            this.devops.Builder();
        }

        public bool Test(bool passed)
        {
            return this.devops.Tester(passed);
        }

        public void Analyse()
        {
            this.devops.Analyser();
        }

        public void Deploy()
        {
            this.devops.Deployer();
        }

        public void GetPackages()
        {
            this.devops.PackagesGetter();
        }

        public void GetSources()
        {
            this.devops.SourcesGetter();
        }

        public void GetUtilities()
        {
            this.devops.UtilitiesGetter();
        }
    }
}
