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
    04/03/2023  Rilegis     2       Added "AddonsDirectory" existence check.
    04/03/2023  Rilegis     3       Added main program loop to execute a given addon.
    09/03/2023  Rilegis     4       Modified logic due to accessibility change for class "AddonsHandler".
**********************************************************************/

using AppWithAddons.App.Handlers;
using AppWithAddons.SDK;

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

                // Instantiates the "AddonsHandler" class, which automatically loads and initializes all addons.
                AddonsHandler addonsHandler = new();

                while (true)
                {
                    Console.WriteLine($"Currently loaded addons:");
                    foreach (KeyValuePair<int, IAddon> addon in addonsHandler.Addons)
                    {
                        Console.WriteLine($"{addon.Key}\t{addon.Value.Name}");
                    }
                    Console.Write("Select an addon to run, or type 'q' to quit: ");
                    string selection = Console.ReadLine();
                    if (selection.ToLower().Equals("q"))
                        break;
                    else
                    {
                        if (int.TryParse(selection, out int id))
                        {
                            if (addonsHandler.Addons.TryGetValue(id, out IAddon addon))
                                addon.Run();
                            else
                                Console.WriteLine("Invalid selection.");
                        }
                        else
                            Console.WriteLine("Invalid input.");
                    }
                }

                // Disposes of all resources (Kinda unnecessary due to .NET managed code, but i like it)
                addonsHandler.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RUNTIME EXCEPTION] {ex.Message}");
            }
        }
    }
}