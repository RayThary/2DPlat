using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeDamage : MonoBehaviour
{
    private Enemy enemy;
    private GameObject obj;
    private PolygonCollider2D pol2d;
    [SerializeField] private int damage=5;
    
    private void Start()
    {
        pol2d = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
       
    }
  
}
