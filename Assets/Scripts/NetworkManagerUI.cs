using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button startHostButton;
    [SerializeField] private Button startServerButton;
    [SerializeField] private Button startClientButton;

    private void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }

    private void Awake()
    {
        startHostButton.onClick.AddListener((() =>
        {
            NetworkManager.Singleton.StartHost();
            EventSystem.current.SetSelectedGameObject(null);

        }));        
        startServerButton.onClick.AddListener((() =>
        {
            NetworkManager.Singleton.StartServer();
            EventSystem.current.SetSelectedGameObject(null);
        }));        
        startClientButton.onClick.AddListener((() =>
        {
            NetworkManager.Singleton.StartClient();
            EventSystem.current.SetSelectedGameObject(null);
        }));
    }
    
    
}