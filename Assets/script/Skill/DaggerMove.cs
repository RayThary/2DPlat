using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DaggerMove : MonoBehaviour
{
    [SerializeField] private float daggerSpeed = 4.0f;
    private Transform playerTrs;
    private bool right;
    
    private void Start()
    {
        Destroy(gameObject, 4f);

        //RePlayer player = GameManager.instance.GetPlayer();
        //GameObject obj= player.gameObject;
        playerTrs = GameManager.instance.GetPlayerTransform();

        if (playerTrs.localScale.x == 1)
        {
            right = true;
        }
    }

    void Update()
    {
        if (right)
        {
            transform.Translate(transform.right * daggerSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right *-1* daggerSpeed * Time.deltaTime);
        }
    }
}
