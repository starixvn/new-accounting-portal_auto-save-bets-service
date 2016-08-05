using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.SaveBets.Builder.Header;

namespace Fanex.SaveBets.Testing
{
    public class Program
    {
        private static void Main(string[] args)
        {
            HeaderBuilder builder = new HeaderBuilderImp();
            var result = builder.GetHeaderByBettype(0);

            foreach (var header in result)
            {
                Console.WriteLine(header);
            }

            Console.ReadKey();
        }
    }
}
