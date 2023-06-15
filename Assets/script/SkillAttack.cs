using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : MonoBehaviour
{
    private Player player;
    [SerializeField]private float speed=5.0f;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        //player = GetComponent<Player>();
        //player = GameObject.Find("Player").GetComponent<Player>();
        player = FindObjectOfType<Player>();
        
    }

    void Update()
    {
        skill1();
    }

    private void skill1()
    {
        if (player.skill1)
        {
            transform.position += new Vector3(1.0f, 0, 0) * speed * Time.deltaTime;
        }
    }
}
