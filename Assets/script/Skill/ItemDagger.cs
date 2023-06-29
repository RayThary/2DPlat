using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 아이템을 쓸수있는 개수를 정해주는스크립트입니다
/// </summary>

public class ItemDagger : MonoBehaviour
{

    [SerializeField]private int addDagger = 5;
    private basicSkill basicSkill;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            basicSkill.DaggerCount = addDagger;
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        basicSkill = FindObjectOfType<basicSkill>();
    }
   
}
