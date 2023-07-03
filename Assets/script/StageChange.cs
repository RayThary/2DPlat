using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class StageChange : MonoBehaviour
{
    public int stage;
    private BoxCollider2D  box2d;
    [SerializeField] private Transform TrsNextStage;
    private void Awake()
    {
        stage = 1;
    }
    private void Start()
    {
        box2d=GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                stage++;
            }
        }
                GameManager.instance.nowStage = stage;
    }
}
