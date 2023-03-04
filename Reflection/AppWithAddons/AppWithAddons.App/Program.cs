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

                // Load addons
                AddonsHandler.Addons = AddonsHandler.LoadAddons(); // Might be changed to AddonsHandlers ah = new(); and do addon loading/initialization inside the constructor
                AddonsHandler.InitializeAddons();

                while (true)
                {
                    Console.WriteLine($"Currently loaded addons:");
                    foreach (KeyValuePair<int, IAddon> addon in AddonsHandler.Addons)
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
                            if (AddonsHandler.Addons.TryGetValue(id, out IAddon addon))
                                addon.Run();
                            else
                                Console.WriteLine("Invalid selection.");
                        }
                        else
                            Console.WriteLine("Invalid input.");
                    }
                }

                AddonsHandler.TerminateAddons(); // Might be changed to ah.Dispose(); and do addon termination inside the deconstructor.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RUNTIME EXCEPTION] {ex.Message}");
            }
        }
    }
}