using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private GameObject objPlayer;
   
    void Start()
    {
        RePlayer player= GameManager.instance.GetPlayer();
        objPlayer = player.gameObject;

    }

    
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.zero, 0, LayerMask.GetMask("Player"));
        if (hit.collider != null)
        {
            Destroy(objPlayer);
            Debug.Log("1");
        }
    }
}
