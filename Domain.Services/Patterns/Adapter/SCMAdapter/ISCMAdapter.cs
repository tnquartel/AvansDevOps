using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Patterns.Adapter.SCMAdapter
{
    public interface ISCMAdapter
    {
        public void Commit();

        public void Push();

        public void Pull();

        public void Merge();

        public void Stage();

        public void Fetch();

        public void GetBranch();

        public void NewBranch(string branchName);
    }
}
