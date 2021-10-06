using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _GP_Job
{
    class Program
    {
        static void Main(string[] args)
        {
            Core.SQL.initializeConnection();
            Core.MainProcessor.GrabData();
            Console.ReadLine();
        }
    }
}
