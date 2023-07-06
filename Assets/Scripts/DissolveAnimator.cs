using System.Collections;
using AmazingAssets.AdvancedDissolve;
using Unity.Netcode;
using UnityEngine;

public class DissolveAnimator : NetworkBehaviour
{
    [SerializeField] private AdvancedDissolveGeometricCutoutController dissolveController;
    [SerializeField] private float endRadius = 12f;
    [SerializeField] private float startRadius = 1f;
    [SerializeField] private int visibleMazeDelay = 1;
    [SerializeField] private int lightsOutDelay = 4;
    [SerializeField] private float animationDuration = 1;

    [ContextMenu("Do Something")]
    private void Start()
    {
        NetworkManager.Singleton.OnServerStarted += OnServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }
    
    void OnServerStarted()
    {
        /*Debug.Log("OnServerStarted");
        Debug.Log("IsServer: " + IsServer);
        Debug.Log("IsClient: " + IsClient);
        Debug.Log("IsHost: " + IsHost);*/
        StartRoutine();
    }

    void OnClientConnected(ulong id)
    {
    }

    void StartRoutine()
    {
        if(!IsServer) return;
        StartCoroutine(AnimationSynchronizer());
    }
    private IEnumerator AnimationSynchronizer()
    {
        var cycleDuration = visibleMazeDelay + lightsOutDelay + animationDuration + animationDuration;
        while (true)
        {
            StartAnimationClientRpc();
            yield return new WaitForSeconds(cycleDuration);
        }
    }
    
    [ClientRpc] 
    private void StartAnimationClientRpc()
    {
        StartCoroutine(AnimateMoveRadius());
    }
    

    private IEnumerator AnimateMoveRadius()
    {
        yield return AnimateMove(startRadius, endRadius);        
        yield return new WaitForSeconds(visibleMazeDelay);
        yield return AnimateMove(endRadius, startRadius);
        yield return new WaitForSeconds(lightsOutDelay);
    }
    
    private IEnumerator AnimateMove(float startValue, float endValue)
    {
        for (float i = 0; i < 1; i += Time.deltaTime/animationDuration)
        {
            float radius = Mathf.Lerp(startValue, endValue, i);
            dissolveController.target1Radius = radius;
            dissolveController.target2Radius = radius;
            yield return null;
        }

        dissolveController.target1Radius = endValue;
        dissolveController.target2Radius = endValue;
    }
}