using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class StageChange : MonoBehaviour
{
    public int stage;
    private BoxCollider2D  box2d;
    [SerializeField] private GameObject NextStageStart;
    [SerializeField] private GameObject PlayerObj;

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
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stage++;
            PlayerObj.transform.position = NextStageStart.transform.position;
        }
        GameManager.instance.nowStage = stage;
    }
}
