using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemArrow : MonoBehaviour
{
    [SerializeField] private int addArrow = 2;
    private basicSkill basicSkill;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            basicSkill.ArrowCount += addArrow;
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        basicSkill = FindObjectOfType<basicSkill>();
    }
}
