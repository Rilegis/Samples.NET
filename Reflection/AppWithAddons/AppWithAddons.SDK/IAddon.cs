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
    04/03/2023  Rilegis     3       Added some basic summaries.
    23/03/2023  Rilegis     4       Added definition for "OnUpdate" virtual method.
    23/03/2023  Rilegis     5       Rolled back nonsense :) .
**********************************************************************/

namespace AppWithAddons.SDK
{
    /// <summary>
    /// Addons interface implementation.
    /// Every addon needs to implement this interface.
    /// </summary>
    public interface IAddon
    {
        /// <summary>
        /// Addon's fancy name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Addon's description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Addon's author.
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// Addon's author URL.
        /// </summary>
        public string AuthorUrl { get; }

        /// <summary>
        /// Addons version string.
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// Addon's initialization method.
        /// This method will be called only once, right after the loading sequence.
        /// </summary>
        public void Initialize();

        /// <summary>
        /// This method will be called every time the addons receives an execution request.
        /// </summary>
        public void Run();

        /// <summary>
        /// Addon's termination method.
        /// This method will be called only once, right before the app's shutdown.
        /// </summary>
        public void Terminate();
    }
}
