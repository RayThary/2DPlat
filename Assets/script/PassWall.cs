using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PassWall : MonoBehaviour
{
    private float timer = 0;
    [SerializeField] private float PassTime = 1.0f;
    public BoxCollider2D box2D;

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(transform.position, box2D.size * transform.localScale);
    //}
    private void Start()
    {
        box2D = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        RaycastHit2D PlayerHit = Physics2D.BoxCast(box2D.bounds.center, box2D.bounds.size, 0f, Vector2.up, 1f, LayerMask.GetMask("Player"));
        if (PlayerHit)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                gameObject.layer = LayerMask.NameToLayer("PassWall");
            }
        }
        if (gameObject.layer == LayerMask.NameToLayer("PassWall"))
        {
            timer += Time.deltaTime;
            if (timer >= PassTime)
            {
                gameObject.layer = LayerMask.NameToLayer("Ground");
                timer = 0;
            }
        }
    }
}
