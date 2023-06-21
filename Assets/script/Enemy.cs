using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform m_TrsBefore;

    private bool m_PlayerCheck;
    [SerializeField] private int NextMove;
    private float timer =0;
    [SerializeField]private float ChangeTime = 4.0f;
    Rigidbody2D m_rig2d;
    void Start()
    {
        m_rig2d= GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeMove();
        if (m_PlayerCheck == false)
        {
            m_rig2d.velocity = new Vector2(NextMove * 4, m_rig2d.velocity.y);

            Vector2 frontVec = new Vector2(m_rig2d.position.x + NextMove, m_rig2d.position.y);
            RaycastHit2D rayHitGround = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
            RaycastHit2D rayHitSidWall = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("SideWall"));
            if (rayHitGround.collider == null || rayHitSidWall.collider != null)
            {
                NextMove *= -1;
            }
        }
        if(m_PlayerCheck)
        {

        }

    }
    
    private void ChangeMove()
    {
        timer += Time.deltaTime;
        if (timer >= ChangeTime)
        {
            NextMove = Random.Range(-1, 2);
            if (NextMove == 0)
            {
                timer = 3;
            }
            else
            {
                timer = 0;
            }
        }

    }
    public void OnTriggerEnemy(HitBoxParent.eHitBoxState _state, HitBoxParent.HitType _hitType, Collider2D _collision)
    {
        switch (_state)
        {
            case HitBoxParent.eHitBoxState.Enter:
                {
                    switch (_hitType)
                    {
                        case HitBoxParent.HitType.Ground:

                            break;
                        case HitBoxParent.HitType.Wall:

                            break;
                    }
                }
                break;
        }
    }


  

}