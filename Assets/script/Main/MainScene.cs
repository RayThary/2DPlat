using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum eScene
{
    main,
    Tutorial,
    game
}

public class MainScene : MonoBehaviour
{
    public static MainScene instance;

    [SerializeField] private Button BtnStart;

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
        BtnStart.onClick.AddListener(()=>LoadScene(eScene.Tutorial));
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
