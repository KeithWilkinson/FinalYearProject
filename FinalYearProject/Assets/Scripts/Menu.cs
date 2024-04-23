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

    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
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
