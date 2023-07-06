using Unity.Netcode.Components;

namespace NetworkOwnerAuthoritatives
{
        public class OwnerNetworkAnimator : NetworkAnimator
        {
                protected override bool OnIsServerAuthoritative()
                {
                        return false;
                }
        }
}