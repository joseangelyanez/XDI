using System;
using System.Linq;
using XDI.Core.DependencyInjection;
using XDI.Providers;

namespace XDI
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (ICodeGenerator generator in new CSharpDataflipFromJson().GetGenerators())
                generator.GenerateCode();

            Console.Read();
        }
    }
}
