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
            this.scm.CommitChanges();

        }

        public void Push()
        {
            this.scm.PushChanges();

        }

        public void Pull()
        {
            this.scm.PullChanges();
        }

        public void Fetch()
        {
            this.scm.FetchChanges();
        }

        public void Merge()
        {
            this.scm.MergeBranches();
        }

        public void Stage()
        {
            this.scm.StageChanges();
        }

        public void GetBranch()
        {
            this.scm.ReturnBranch();
        }

        public void NewBranch(string branchName)
        {
            this.scm.CreateBranch(branchName);
        }

    }
}
