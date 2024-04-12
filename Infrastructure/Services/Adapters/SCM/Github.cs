using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Adapter.SCM
{
    public class Github
    // Deze classe is een voorbeeld implementatie van een derde SCM systeem
    {
        private ArrayList branches = new ArrayList();
        private string branch;

        public Github()
        {
            this.branches[0] = "main";
            this.branch = (string?)this.branches[0];
        }

        public void CommitChanges()
        {
            Console.WriteLine("Commit");
        }

        public void PushChanges()
        {
            Console.WriteLine("Push");
        }

        public void PullChanges()
        {
            Console.WriteLine("Pull");
        }

        public void MergeBranches()
        {
            Console.WriteLine("Merge");
        }
        public void StageChanges()
        {
            Console.WriteLine("Stage");
        }
        public void FetchChanges()
        {
            Console.WriteLine("Fetch");
        }

        public void ReturnBranch()
        {
            Console.WriteLine("Branch: " + this.branch);
        }

        public void CreateBranch(string branchName)
        {
            this.branches.Add(branchName);
            Console.WriteLine("NewBranch: " + branchName);
        }

    }
}
