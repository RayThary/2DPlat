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

    [SerializeField] private float moveMax = 0.2f;
    [SerializeField] private float speed = 2f;
    private Vector3 itemPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            basicSkill.DaggerCount += addDagger;
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        basicSkill = FindObjectOfType<basicSkill>();
        itemPos = transform.position;
    }
    private void Update()
    {
        Vector3 dirPos = itemPos;
        dirPos.y = itemPos.y + moveMax * Mathf.Sin(Time.time * speed);
        transform.position = dirPos;
    }
}
