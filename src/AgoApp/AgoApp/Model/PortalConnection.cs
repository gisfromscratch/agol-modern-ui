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
