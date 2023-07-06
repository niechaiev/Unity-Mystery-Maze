using Unity.Netcode.Components;

namespace NetworkOwnerAuthoritatives
{
    public class ClientNetworkTransform : NetworkTransform
    {
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}