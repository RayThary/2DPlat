using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �������� �����ִ� ������ �����ִ½�ũ��Ʈ�Դϴ�
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
