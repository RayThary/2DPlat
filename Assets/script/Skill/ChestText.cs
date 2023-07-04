using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChestText : MonoBehaviour
{
    [SerializeField] private float moveMax;
    [SerializeField] private float speed;
    
    private Vector3 chestPos;
    
    void Start()
    {
        chestPos = transform.position;
        
    }


    void Update()
    {
        Vector3 dirPos = chestPos;
        dirPos.y = chestPos.y + moveMax * Mathf.Sin(Time.time * speed);
        transform.position = dirPos;
    }
}
