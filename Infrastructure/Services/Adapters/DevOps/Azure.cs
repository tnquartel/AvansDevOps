using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Adapters.DevOps
{
    public class Azure
    {
        // Deze classe is een voorbeeld implementatie van een derde DevOps systeem
        public void Builder()
        {
            Console.WriteLine("Build");
        }

        public void Tester()
        {
            Console.WriteLine("Test");
        }

        public void Analyser()
        {
            Console.WriteLine("Analyse");
        }

        public void Deployer()
        {
            Console.WriteLine("Deploy");
        }

        public void SourcesGetter()
        {
            Console.WriteLine("Sources");
        }

        public void PackagesGetter()
        {
            Console.WriteLine("Package");
        }

        public void UtilitiesGetter()
        {
            Console.WriteLine("Utility");
        }
    }
}
