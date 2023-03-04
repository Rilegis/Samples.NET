/**********************************************************************
    Author            : Rilegis
    Repository        : Samples.NET
    Project           : AppWithAddons.App
    File name         : Constants.cs
    Date created      : 04/03/2023
    Purpose           : This static class contains basic constant properties needed for the app's execution.

    Revision History  :
    Date        Author      Ref     Revision 
    04/03/2023  Rilegis     1       Class definition and 'AddonsDirectory' constant definition.
    04/03/2023  Rilegis     2       Added some basic summaries.
**********************************************************************/

namespace AppWithAddons.App
{
    internal static class Constants
    {
        /// <summary>
        /// Directory containing all *.dll addons assemblies.
        /// </summary>
        internal const string AddonsDirectory = @".\addons";
    }
}
