using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static DontDestroyOnLoad _instance;

    public static DontDestroyOnLoad instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<DontDestroyOnLoad>();
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Stage 2")
        {
            Destroy(this.gameObject);
        }
    }
}
