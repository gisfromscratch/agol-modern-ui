/*
 * Copyright 2016 Jan Tschada
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using Esri.ArcGISRuntime.Portal;
using System.Linq;

namespace AgoApp.Data
{
    /// <summary>
    /// Represents a data services for the basemaps.
    /// </summary>
    public class BasemapDataService : IBasemapDataService
    {
        private readonly IPortalConnectionDataService _portalConnectionDataService;

        /// <summary>
        /// Creates a new instance using a portal connection data service.
        /// </summary>
        /// <param name="portalConnectionDataService">Delivers the current portal connection.</param>
        public BasemapDataService(IPortalConnectionDataService portalConnectionDataService)
        {
            _portalConnectionDataService = portalConnectionDataService;
        }

        public async Task<IEnumerable<ArcGISPortalItem>> GetBasemaps()
        {
            var portalConnection = _portalConnectionDataService.GetPortalConnection();
            if (null == portalConnection || null == portalConnection.Portal)
            {
                return await Task.FromResult(Enumerable.Empty<ArcGISPortalItem>());
            }

            var portal = portalConnection.Portal;
            var portalInfo = portal.ArcGISPortalInfo;
            var searchResult = await portalInfo.SearchBasemapGalleryAsync();
            return await Task.FromResult(searchResult.Results);
        }
    }
}
