using System.Collections;
using System.Collections.Generic;
using AmazingAssets.AdvancedDissolve;
using UnityEngine;
using UnityEngine.Serialization;

public class DissolveAnimator : MonoBehaviour
{
    [SerializeField] private AdvancedDissolveGeometricCutoutController dissolveController;
    [SerializeField] private float endRadius = 12f;
    [SerializeField] private float startRadius = 1f;
    [SerializeField] private int visibleMazeDelay = 1;
    [SerializeField] private int lightsOutDelay = 5;


    private void Start()
    {
        StartCoroutine(AnimateMoveRadius());
    }

    private IEnumerator AnimateMoveRadius()
    {
        while (true)
        {
            yield return new WaitForSeconds(visibleMazeDelay);
            yield return AnimateMove(endRadius, startRadius);
            yield return new WaitForSeconds(lightsOutDelay);
            yield return AnimateMove(startRadius, endRadius);
        }
    }

    private IEnumerator AnimateMove(float startValue, float endValue)
    {
        for (float i = 0; i < 1; i += Time.deltaTime)
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