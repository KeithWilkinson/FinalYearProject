using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _startScreen;
    [SerializeField] private Camera _menuCamera;
    [SerializeField] private GameObject _car;
    public static bool _hasGameStarted = false;
    public bool isCarEnabled = false;
    [SerializeField] private GameObject _VFXObject;

    // Starts game
    public void StartGame()
    {
        _menuCamera.enabled = false;
        _startScreen.SetActive(false);
        if(isCarEnabled == true)
        {
            _car.SetActive(true);
        }
        _hasGameStarted = true;
    }
}
