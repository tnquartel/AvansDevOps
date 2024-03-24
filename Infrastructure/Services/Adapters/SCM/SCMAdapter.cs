using Domain.Services.Patterns.Adapter.SCMAdapter;
using Infrastructure.Services.Adapter.SCM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Adapters.SCM
{
    public class SCMAdapter : ISCMAdapter
    {

        private Github scm;

        public SCMAdapter()
        {
            this.scm = new Github();
        }

        public void Commit()
        {
            this.scm.Commit();

        }

        public void Push()
        {
            this.scm.Push();

        }

        public void Pull()
        {
            this.scm.Pull();
        }

        public void Fetch()
        {
            this.scm.Fetch();
        }

        public void Merge()
        {
            this.scm.Merge();
        }

        public void Stage()
        {
            this.scm.Stage();
        }

        public void GetBranch()
        {
            this.scm.GetBranch();
        }

        public void NewBranch(string branchName)
        {
            this.scm.NewBranch(branchName);
        }

    }
}
