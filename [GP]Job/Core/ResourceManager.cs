using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _GP_Job.Core
{
   static class ResourceManager
    {
       public static INIManager manager = new INIManager($"{AppDomain.CurrentDomain.BaseDirectory}\\config.ini");
    }
}
