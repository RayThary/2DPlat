using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PassWall : MonoBehaviour
{
    private float timer = 0;
    [SerializeField] private float PassTime = 1.0f;
    [SerializeField] private CompositeCollider2D com2D;
    private Transform[] passChildren;

    private BoxCollider2D[] box2d;
    private void Start()
    {
        com2D = GetComponent<CompositeCollider2D>();
        passChildren = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        RaycastHit2D PlayerHit = Physics2D.BoxCast(com2D.bounds.center, com2D.bounds.size, 0f, Vector2.up, 1f, LayerMask.GetMask("Player"));
        if (PlayerHit)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                gameObject.layer = LayerMask.NameToLayer("PassWall");
                changepasschildrens();
            }
        }
        if (gameObject.layer == LayerMask.NameToLayer("PassWall"))
        {
            timer += Time.deltaTime;
            if (timer >= PassTime)
            {
                gameObject.layer = LayerMask.NameToLayer("Ground");
                returnpasschildrens();
                timer = 0;
            }
        }
    }

    private void changepasschildrens()
    {
        int count = passChildren.Length;
        for(int i=0; i < count; i++)
        {
            passChildren[i].gameObject.layer = LayerMask.NameToLayer("PassWall");
        }
    }
    private void returnpasschildrens()
    {
        int count = passChildren.Length;
        for (int i = 0; i < count; i++)
        {
            passChildren[i].gameObject.layer = LayerMask.NameToLayer("Ground");
        }
    }

}
