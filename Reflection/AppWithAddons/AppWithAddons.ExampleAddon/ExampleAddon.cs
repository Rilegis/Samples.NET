/**********************************************************************
    Author            : Rilegis
    Repository        : Samples.NET
    Project           : AppWithAddons.ExampleAddon
    File name         : ExampleAddon.cs
    Date created      : 04/03/2023
    Purpose           : This is an example of an addon created for the 'AppWithAddon' sample project.
                        In order to create an addon for the 'AppWithAddon' app, it is required to implement
                        the 'IAddon' interface.

    Revision History  :
    Date        Author      Ref     Revision 
    04/03/2023  Rilegis     1       First code commit.
**********************************************************************/

using AppWithAddons.SDK;
using System;

namespace AppWithAddons.ExampleAddon
{
    public class ExampleAddon : IAddon
    {
        public string Name => "Example Addon";
        public string Description => "An example addon with basic functionality for the 'AppWithAddons' sample project.";
        public string Author => "Rilegis";
        public string AuthorUrl => "https://github.com/Rilegis/Samples.NET/tree/main/Reflection/AppWithAddons/AppWithAddons.ExampleAddon";
        public string Version => "1.0.0"; // (Major).(Minor).(Patch)[b(buildnumber)]

        // This method gets called only once, during addon loading.
        public void Initialize()
        {
            Console.WriteLine($"[{Name} (v{Version})] Initializing addon execution...");

            // Do your stuff...blah...blah...blah

            Console.WriteLine($"[{Name} (v{Version})] Initialization completed.");
        }

        // This method gets called every time an execution of this addon is required.
        public void Run()
        {
            // This addon just prints it's informations.
            Console.WriteLine($"Addon: {Name}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"AuthorUrl: {AuthorUrl}");
            Console.WriteLine($"Version: {Version}");
        }

        // This method gets called only once, during addon unloading.
        public void Terminate()
        {
            Console.WriteLine($"[{Name} (v{Version})] Terminating addon execution...");

            // Do your stuff...blah...blah...blah

            Console.WriteLine($"[{Name} (v{Version})] Termination completed.");
        }
    }
}
