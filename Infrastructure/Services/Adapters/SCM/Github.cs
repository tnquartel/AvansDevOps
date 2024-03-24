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

        public void Commit()
        {
            Console.WriteLine("Commit");
        }

        public void Push()
        {
            Console.WriteLine("Push");
        }

        public void Pull()
        {
            Console.WriteLine("Pull");
        }

        public void Merge()
        {
            Console.WriteLine("Merge");
        }
        public void Stage()
        {
            Console.WriteLine("Stage");
        }
        public void Fetch()
        {
            Console.WriteLine("Fetch");
        }

        public void GetBranch()
        {
            Console.WriteLine("Branch: " + this.branch);
        }

        public void NewBranch(string branchName)
        {
            this.branches.Add(branchName);
            Console.WriteLine("NewBranch: " + branchName);
        }

    }
}
