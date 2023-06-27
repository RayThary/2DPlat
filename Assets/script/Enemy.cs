using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform m_TrsBefore;

    [SerializeField] private float EnemySpeed = 1.0f;
    [SerializeField]private bool m_PlayerCheck;

    [SerializeField]private int NextMove;//다음움직임왼쪽오른쪽일지랜덤생성
    [SerializeField]private float timer =0;
    [Header("움직임바꾸는시간")]
    [SerializeField]private float ChangeTime = 8.0f;
    Rigidbody2D m_rig2d;

    private Transform target;

    [SerializeField] private bool Tester = false;

    void Start()
    {
        NextMove = 1;
        m_rig2d = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeMove();
        CheckPlayer();

    }

    private void CheckPlayer()
    {
        RaycastHit2D rayHitPlayer = Physics2D.CircleCast(transform.position, 5.0f, Vector3.up, 0f, LayerMask.GetMask("Player"));

        if (rayHitPlayer.collider == null)
        {
            m_rig2d.velocity = new Vector2(NextMove * EnemySpeed, m_rig2d.velocity.y);
            
            Vector2 frontVec = new Vector2(m_rig2d.position.x + NextMove, m_rig2d.position.y);//내앞백터
            RaycastHit2D rayHitGround = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
            RaycastHit2D rayHitSidWall = Physics2D.Raycast(frontVec, Vector3.zero, 1, LayerMask.GetMask("SideWall", "Enemy"));
            if (rayHitGround.collider == null || rayHitSidWall.collider != null)
            {
                NextMove *= -1;
            }
        }
        else if (rayHitPlayer.collider != null)
        {
            m_PlayerCheck = true;
            Vector3 dir = target.position;
            float f_right = 0.0f;
            if (transform.position.x > dir.x)
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                f_right = 1;
            }
            else if (transform.position.x < dir.x)
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                f_right = -1;
            }
        }



        
        if (m_PlayerCheck)
        {
            float f_right = 0.0f;
            Vector3 dir = target.position;
            if(transform.position.x>dir.x)
            {
                transform.localScale=new Vector3(1.0f,1.0f, 1.0f);
                f_right = 1;
            }
            else if (transform.position.x<dir.x)
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                f_right = -1;
            }
            Vector2 frontVec = new Vector2(m_rig2d.position.x + NextMove, m_rig2d.position.y);
            RaycastHit2D rayHitGround = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
            RaycastHit2D rayHitSidWall = Physics2D.Raycast(frontVec, Vector3.right, 1, LayerMask.GetMask("SideWall","Enemy"));
            if (rayHitGround.collider == null || rayHitSidWall.collider != null)
            {
                f_right *= -1;
                m_PlayerCheck = false;
            }
            m_rig2d.velocity = new Vector2(EnemySpeed*f_right, m_rig2d.velocity.y);
            

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
                timer = 7;
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

           
        }
    }


  

}