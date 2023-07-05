using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signText : MonoBehaviour
{
    private BoxCollider2D box2d;
    [SerializeField]private GameObject tutorial;
    void Start()
    {
        box2d = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        if (box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            tutorial.SetActive(true);
        }
        else
        {
            tutorial.SetActive(false);
        }
    }
}
