using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Patterns.Adapter.DevOpsAdapter
{
    public class Azure
    {
        // Deze classe is een voorbeeld implementatie van een derde DevOps systeem
        public void Build()
        {
            Console.WriteLine("Build");
        }

        public void Test()
        {
            Console.WriteLine("Test");
        }

        public void Analyse()
        {
            Console.WriteLine("Analyse");
        }

        public void Deploy()
        {
            Console.WriteLine("Deploy");
        }

        public void GetSources()
        {
            Console.WriteLine("Sources");
        }

        public void GetPackages()
        {
            Console.WriteLine("Package");
        }

        public void GetUtilities()
        {
            Console.WriteLine("Utility");
        }
    }
}
