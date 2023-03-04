/**********************************************************************
    Author            : Rilegis
    Repository        : Samples.NET
    Project           : AppWithAddons.App
    File name         : Program.cs
    Date created      : 04/03/2023
    Purpose           : This is the entry point for the application.

    Revision History  :
    Date        Author      Ref     Revision 
    04/03/2023  Rilegis     1       First code commit.
    04/03/2023  Rilegis     2       Added "AddonsDirectory" handling.
**********************************************************************/

namespace AppWithAddons.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine($"Starting execution of application '{typeof(Program).Namespace}'");

                // Check if 'addons' directory exists;
                // If it does not exists then create it, and proceed to load the plugins
                if (!Directory.Exists(Constants.AddonsDirectory))
                {
                    Console.WriteLine($"No addons directory found...creating '{Constants.AddonsDirectory}' directory.");
                    Directory.CreateDirectory(Constants.AddonsDirectory);
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RUNTIME EXCEPTION] {ex.Message}");
            }
        }
    }
}