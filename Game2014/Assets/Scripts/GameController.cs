using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    GameObject _onScreenController;

    [SerializeField]
    bool _isTesting;

    // Start is called before the first frame update
    void Start()
    {
        _onScreenController = GameObject.Find("OnScreenControllers");
        if (_isTesting)
        {
            _onScreenController.SetActive(Application.platform != RuntimePlatform.WindowsEditor &&
                                          Application.platform != RuntimePlatform.WindowsPlayer);
            Debug.Log("GameController is working");
        }

    }
}
