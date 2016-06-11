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

using Esri.ArcGISRuntime.Portal;
using Esri.ArcGISRuntime.Security;

namespace AgoApp.Model
{
    /// <summary>
    /// Represents a connection to a portal instance.
    /// </summary>
    public class PortalConnection
    {
        private readonly TokenCredential _tokenCredential;
        private readonly ArcGISPortal _portal;

        /// <summary>
        /// Creates a new connection instance using a credential.
        /// </summary>
        /// <param name="tokenCredential">The token credential.</param>
        /// <param name="portal">The portal instance.</param>
        public PortalConnection(TokenCredential tokenCredential, ArcGISPortal portal)
        {
            _tokenCredential = tokenCredential;
            _portal = portal;
        }

        /// <summary>
        /// Creates a new connection instance using anonymous access.
        /// </summary>
        /// <param name="portal">The portal instance.</param>
        public PortalConnection(ArcGISPortal portal)
        {
            _tokenCredential = null;
            _portal = portal;
        }

        public TokenCredential TokenCredential
        {
            get { return _tokenCredential; }
        }

        public ArcGISPortal Portal
        {
            get { return _portal; }
        }
    }
}
