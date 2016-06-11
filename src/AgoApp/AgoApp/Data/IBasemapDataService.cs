using AgoApp.Model;
using Esri.ArcGISRuntime.Portal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgoApp.Data
{
    /// <summary>
    /// Offers the basemaps of some portal instance.
    /// </summary>
    public interface IBasemapDataService
    {
        /// <summary>
        /// Task for accessing the basemaps.
        /// </summary>
        /// <returns>Task for accesing the basemaps.</returns>
        Task<IEnumerable<ArcGISPortalItem>> GetBasemaps();
    }
}