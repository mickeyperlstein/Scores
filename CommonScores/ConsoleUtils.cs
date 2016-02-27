using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonScores
{
    public static class ConsoleUtils
    {
        public static void Title(string message)
        {
            Console.WriteLine("================================================================");
            Console.WriteLine("\n\n");
            Console.WriteLine("{0}", message);
            Console.WriteLine("\n\n");
            Console.WriteLine("================================================================");
        }
    }
}
