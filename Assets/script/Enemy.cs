using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform m_TrsBefore;

    [SerializeField] private float EnemySpeed = 1.0f;
    [SerializeField]private bool m_PlayerCheck;

    [SerializeField]private int NextMove=1;//다음움직임왼쪽오른쪽일지랜덤생성
    [SerializeField]private float timer =0;
    [Header("움직임바꾸는시간")]
    [SerializeField]private float ChangeTime = 4.0f;
    Rigidbody2D m_rig2d;

    private Transform target;

    [SerializeField] private bool Tester = false;

    void Start()
    {
        m_rig2d= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeMove();
        CheckPlayer();

    }

    private void CheckPlayer()
    {
        if (m_PlayerCheck == false)
        {
            m_rig2d.velocity = new Vector2(NextMove * EnemySpeed, m_rig2d.velocity.y);

            Vector2 frontVec = new Vector2(m_rig2d.position.x + NextMove, m_rig2d.position.y);
            RaycastHit2D rayHitGround = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
            RaycastHit2D rayHitSidWall = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("SideWall"));
            if (rayHitGround.collider == null || rayHitSidWall.collider != null)
            {
                NextMove *= -1;
            }
        }
        if (m_PlayerCheck)
        {
            
            m_rig2d.velocity = new Vector2(EnemySpeed, m_rig2d.velocity.y);
            Vector3 dir = target.position;
            if(transform.position.x>dir.x)
            {
                transform.localScale=new Vector3(1.0f,1.0f, 1.0f);
            }
            else if (transform.position.x<dir.x)
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
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
                        case HitBoxParent.HitType.Player:
                            if (_collision.gameObject.layer == LayerMask.NameToLayer("Player"))
                            {
                                m_PlayerCheck = true;
                            }
                            break;
                    }
                }
                break;

            case HitBoxParent.eHitBoxState.Exit:
                {
                    switch (_hitType)
                    {
                        case HitBoxParent.HitType.Player:
                            if (_collision.gameObject.layer == LayerMask.NameToLayer("Player"))
                            {
                                m_PlayerCheck = false;
                            }
                            break;
                    }
                }
                break;
        }
    }


  

}