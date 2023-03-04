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
**********************************************************************/

using AppWithAddons.SDK;
using System.Reflection;

namespace AppWithAddons.App.Handlers
{
    internal static class AddonsHandler
    {
        internal static Dictionary<int, IAddon>? Addons;

        internal static Dictionary<int, IAddon> LoadAddons()
        {
            // Local addons dictionary instance, will be returned at the end of the method.
            Dictionary<int, IAddon> loadedAddons = new();

            try
            {
                // Read all 'DLL' files located in the 'AddonsExtenxion' directory.
                FileInfo[] files = new DirectoryInfo(Constants.AddonsDirectory).GetFiles("*.dll");

                // Read all found assembly files
                Assembly assembly;
                Type[] assemblyTypes;
                foreach (FileInfo file in files)
                {
                    assembly = Assembly.LoadFile(file.FullName);

                    // Look for types that implement the 'IAddon' interface and are NOT interface definitions!
                    assemblyTypes = assembly.GetTypes().Where(t => typeof(IAddon).IsAssignableFrom(t) && !t.IsInterface).ToArray();

                    // Once all viable types are found, create an instance at runtime for each one.
                    int i = -1;
                    foreach (Type type in assemblyTypes)
                    {
                        var addonInstance = Activator.CreateInstance(type) as IAddon;
                        Console.WriteLine($"Found addon: '{addonInstance.Name}' (v{addonInstance.Version})...loading");
                        loadedAddons.Add(i++, addonInstance);
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

        internal static void InitializeAddons()
        {
            try
            {
                foreach (IAddon addon in Addons.Values)
                {
                    InitializeAddon(addon);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RUNTIME EXCEPTION] {ex.Message}");
            }
        }

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
    }
}
