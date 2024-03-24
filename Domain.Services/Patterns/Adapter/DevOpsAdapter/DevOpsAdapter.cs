using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Patterns.Adapter.DevOpsAdapter
{
    public class DevOpsAdapter : IDevOps
    {

        private Azure devops;

        public DevOpsAdapter()
        {
            this.devops = new Azure();
        }

        public void Build()
        {
            this.devops.Build();
        }

        public void Test()
        {
            this.devops.Test();
        }

        public void Analyse()
        {
            this.devops.Analyse();
        }

        public void Deploy()
        {
            this.devops.Deploy();
        }

        public void GetPackages()
        {
            this.devops.GetPackages();
        }

        public void GetSources()
        {
            this.devops.GetSources();
        }

        public void GetUtilities()
        {
            this.devops.GetUtilities();
        }
    }
}
