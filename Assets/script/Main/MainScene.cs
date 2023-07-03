using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    public static MainScene instance;

    [SerializeField] private Button BtnStart;
    public enum eScene 
    { 
        main,
        game
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        BtnStart.onClick.AddListener(()=>LoadScene(eScene.game));
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        
    }

    public void LoadScene(eScene _value)
    {
        SceneManager.LoadSceneAsync((int)_value);
    }
}
