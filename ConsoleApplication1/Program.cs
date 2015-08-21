using System;
using Microsoft.Owin.Hosting;
using SampleLibrary;

namespace ConsoleApplication1
{
    static class Program
    {
        static void Main(string[] args)
        {
            const string root = "http://localhost:12345";
            using (WebApp.Start<Startup>(root))
            {
                Console.WriteLine("Serving content on " + root);
                Console.WriteLine("(press <enter> to quit)");
                Console.ReadLine();
            }
        }
    }
}
