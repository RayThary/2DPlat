using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private BoxCollider2D box2d;
    
    
   
    void Start()
    {
        box2d = GetComponent<BoxCollider2D>();
        
    }

    
    void Update()
    {
        if (box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MainScene.instance.LoadScene(eScene.game);
            }
        }
    }
    
}
