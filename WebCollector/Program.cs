namespace WebCollector
{
    using System;
    using System.Net;
    using Config;
    using SoftwareControllerLib.Utils;
    using Utils;

    /// <summary>
    /// Main program class.
    /// </summary>
    static class Program
    {
        static void Main()
        {
            try {
                WebConfigReader reader = new WebConfigReader("default.xml");
                WebCollectorSession session = reader.Read();
                session.Html = HtmlUtils.GetHtmlString(session.StartAddress);
                ConsoleOutput.Instance.Message(string.Format("Navigated to {0}", session.StartAddress));

                session.RunUntilFailure();
            } catch (WebException ex) {
                ConsoleOutput.Instance.Error(ex.Message);
            } catch (Exception ex) {
                ConsoleOutput.Instance.Error("Fatal exception");
                ConsoleOutput.Instance.Error(ex.Message);
            } finally {
                ConsoleOutput.Instance.Message(string.Empty);
                ConsoleOutput.Instance.Message("Press any key to exit ...");
                Console.ReadKey();
            }
        }
    }
}