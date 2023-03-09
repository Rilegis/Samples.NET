/**********************************************************************
    Author            : Rilegis
    Repository        : Samples.NET
    Project           : AppWithAddons.App
    File name         : AddonsHandler.cs
    Date created      : 04/03/2023
    Purpose           : This class contains all the methods needed to handle the addons.

    Revision History  :
    Date        Author      Ref     Revision 
    04/03/2023  Rilegis     1       Class definition and 'LoadAddons()' method implementation.
    04/03/2023  Rilegis     2       Implemented 'InitializeAddon(IAddon addon)' and 'InitializeAddons()' methods for addons initialization...duh.
    04/03/2023  Rilegis     3       Modified 'InitializeAddons()' method for multithreaded processing.
    04/03/2023  Rilegis     4       Implemented 'TerminateAddon(IAddon addon)' and 'TerminateAddons()' methods for addons termination...again...duh.
    04/03/2023  Rilegis     5       Added some basic summaries.
    04/03/2023  Rilegis     6       Added some more basic summaries.
    04/03/2023  Rilegis     7       Fixed 'loadedAddons' dictionary key starting from -1.
    04/03/2023  Rilegis     8       Added 'SearchOption' to 'DirectoryInfo.GetFiles()' to enable recursive search on all subdirectories.
    09/03/2023  Rilegis     9       Changed class accessibility; Declared "AddonsHandler()" constructor; Implemented "IDisposable" interface.
**********************************************************************/

using AppWithAddons.SDK;
using System.Reflection;

namespace AppWithAddons.App.Handlers
{
    /// <summary>
    /// This class contains all the methods needed to handle the addons.
    /// </summary>
    internal class AddonsHandler : IDisposable
    {
        /// <summary>
        /// Dictionary instance containing all loaded addons.
        /// </summary>
        internal Dictionary<int, IAddon>? Addons;

        /// <summary>
        /// Constructor; Instantiates a new "AddonsHandler" class and automatically loads and initializes all addons.
        /// </summary>
        internal AddonsHandler()
        {
            try
            {
                Addons = LoadAddons();
                InitializeAddons();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RUNTIME EXCEPTION] {ex.Message}");
            }
        }

        /// <summary>
        /// Loads all viable addons found in the addons directory.
        /// </summary>
        /// <returns>The loaded addons.</returns>
        internal static Dictionary<int, IAddon> LoadAddons()
        {
            // Local addons dictionary instance, will be returned at the end of the method.
            Dictionary<int, IAddon> loadedAddons = new();

            try
            {
                // Read all 'DLL' files located in the 'AddonsExtenxion' directory.
                FileInfo[] files = new DirectoryInfo(Constants.AddonsDirectory).GetFiles("*.dll", SearchOption.AllDirectories);

                // Read all found assembly files
                Assembly assembly;
                Type[] assemblyTypes;
                foreach (FileInfo file in files)
                {
                    assembly = Assembly.LoadFile(file.FullName);

                    // Look for types that implement the 'IAddon' interface and are NOT interface definitions!
                    assemblyTypes = assembly.GetTypes().Where(t => typeof(IAddon).IsAssignableFrom(t) && !t.IsInterface).ToArray();

                    // Once all viable types are found, create an instance at runtime for each one.
                    int i = 0;
                    foreach (Type type in assemblyTypes)
                    {
                        var addonInstance = Activator.CreateInstance(type) as IAddon;
                        Console.WriteLine($"Found addon: '{addonInstance.Name}' (v{addonInstance.Version})...loading");
                        loadedAddons.Add(i, addonInstance);
                        i++;
                    }
                }

                // Return all loaded addons
                return loadedAddons;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RUNTIME EXCEPTION] {ex.Message}");

                // Return all loaded addons
                return loadedAddons;
            }
        }

        /// <summary>
        /// Initializes all loaded addons by calling the "Initialize()" interface method.
        /// </summary>
        internal void InitializeAddons()
        {
            // Since no addons depend from one another i can initialize them at the same time.
            try
            {
                Addons.AsParallel().ForAll(addon => { InitializeAddon(addon.Value); });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RUNTIME EXCEPTION] {ex.Message}");
            }
        }

        /// <summary>
        /// Initializes the specified addon by calling the "Initialize()" interface method.
        /// </summary>
        /// <param name="addon"></param>
        private static void InitializeAddon(IAddon addon)
        {
            try
            {
                addon.Initialize();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RUNTIME EXCEPTION] {ex.Message}");
            }
        }

        /// <summary>
        /// Terminates all loaded addons by calling the "Terminate()" interface method.
        /// </summary>
        internal void TerminateAddons()
        {
            // Since no addons depend from one another i can terminate them at the same time.
            try
            {
                Addons.AsParallel().ForAll(addon => { TerminateAddon(addon.Value); });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RUNTIME EXCEPTION] {ex.Message}");
            }
        }

        /// <summary>
        /// Terminates the specified addon by calling the "Terminate()" interface method.
        /// </summary>
        /// <param name="addon"></param>
        private static void TerminateAddon(IAddon addon)
        {
            try
            {
                addon.Terminate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RUNTIME EXCEPTION] {ex.Message}");
            }
        }

        /// <summary>
        /// Terminates all addons and clears "Addons" dictionary; Inherited by "IDisposable".
        /// </summary>
        public void Dispose()
        {
            try
            {
                TerminateAddons();
                Addons.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RUNTIME EXCEPTION] {ex.Message}");
            }
        }
    }
}
