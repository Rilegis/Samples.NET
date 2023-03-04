/**********************************************************************
    Author            : Rilegis
    Repository        : Samples.NET
    Project           : AppWithAddons.SDK
    File name         : IAddon.cs
    Date created      : 04/03/2023
    Purpose           : This is the interface that describes how the addons for this app
                        will need to look like;
                        All addons for the app will have to implement this interface.

    Revision History  :
    Date        Author      Ref     Revision 
    04/03/2023  Rilegis     1       First code commit.
    04/03/2023  Rilegis     2       Interface definition.
**********************************************************************/

namespace AppWithAddons.SDK
{
    public interface IAddon
    {
        public string Name { get; }
        public string Description { get; }
        public string Author { get; }
        public string AuthorUrl { get; }
        public string Version { get; }

        public void Initialize();
        public void Run();
        public void Terminate();
    }
}
