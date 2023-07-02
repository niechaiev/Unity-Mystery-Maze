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
            float radius = endRadius;
            while (radius >= startRadius)
            {
                radius -= endRadius * Time.deltaTime;

                dissolveController.target1Radius = radius;
                dissolveController.target2Radius = radius;
                yield return null;
            }

            yield return new WaitForSeconds(lightsOutDelay);
            radius = startRadius;
            while (radius <= endRadius)
            {
                radius += endRadius * Time.deltaTime;

                dissolveController.target1Radius = radius;
                dissolveController.target2Radius = radius;
                yield return null;
            }
        }
    }
}