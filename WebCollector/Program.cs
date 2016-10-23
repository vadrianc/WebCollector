using System;
using System.Net;
using WebCollector.Config;
using WebCollector.Utils;

namespace WebCollector
{
    /// <summary>
    /// Main program class.
    /// </summary>
    class Program
    {
        static void Main()
        {
            try {
                WebConfigReader reader = new WebConfigReader("default.xml");
                WebCollectorSession session = reader.Read();

                session.Html = HtmlUtils.GetHtmlString(session.StartAddress);
                session.Run();
            } catch (WebException ex) {
                Console.WriteLine(ex.Message);
            } catch (Exception ex) {
                Console.WriteLine("Fatal exception");
                Console.WriteLine(ex.Message);
            } finally {
                Console.WriteLine();
                Console.WriteLine("Press any key to exit ...");
                Console.ReadKey();
            }
        }
    }
}
