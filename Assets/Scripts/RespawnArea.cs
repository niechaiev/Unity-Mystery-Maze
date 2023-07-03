using System;
using UnityEngine;

public class RespawnArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerMovement>().Respawn();
        Debug.Log("collision");
    }
}