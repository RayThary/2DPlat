using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("적 타입 1근접  2원거리  3미정   한개만체크해주세요")]
    [SerializeField] private bool Type1;
    [SerializeField] private bool Type2;
    [SerializeField] private bool Type3;

    private Transform m_TrsBefore;

    [SerializeField] private float EnemySpeed = 1.0f;
    private float beforeSpeed = 0.0f;
    private bool m_PlayerCheck;
    [SerializeField] private float m_EnemyPlayerCheckRange = 5.0f;

    private int NextMove;//다음움직임왼쪽오른쪽일지랜덤생성
    private float timer =0;
    private float waittimer = 0;
    [Header("움직임바꾸는시간")]
    [SerializeField]private float ChangeTime = 8.0f;
    Rigidbody2D m_rig2d;

    private Transform target;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, m_EnemyPlayerCheckRange);
    }

    void Start()
    {
        NextMove = 1;
        m_rig2d = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        beforeSpeed = EnemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeMove();
        CheckPlayer();

    }


    private void CheckPlayer()
    {

        //기본움직임
        m_rig2d.velocity = new Vector2(NextMove * EnemySpeed, m_rig2d.velocity.y);

        Vector2 frontVec = new Vector2(m_rig2d.position.x + NextMove, m_rig2d.position.y);// 이오브젝트 x+1의 위치

        RaycastHit2D rayHitGround = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        RaycastHit2D rayHitCheckWall = Physics2D.Raycast(frontVec, Vector3.zero, 1, LayerMask.GetMask("SideWall", "Enemy"));
        RaycastHit2D rayHitFailling = Physics2D.Raycast(frontVec, Vector3.zero, 1, LayerMask.GetMask("FaillingWall"));

        Vector3 dir = target.position;//적의 위치

        if (rayHitGround.collider == null || rayHitCheckWall.collider != null)
        {
            NextMove *= -1;
        }
        if (rayHitFailling.collider != null)
        {
            NextMove *= -1;
        }

       

        //플레이어 체크
        RaycastHit2D rayHitPlayer = Physics2D.CircleCast(transform.position, m_EnemyPlayerCheckRange
            , Vector3.up, 0f, LayerMask.GetMask("Player"));

        if (rayHitPlayer.collider != null)
        {
            m_PlayerCheck = true;
        }
        else
        {
            m_PlayerCheck = false;
        }

        //플레이어가 범위안에 들어오면 플레이어방향으로 2배빠른속도로 달려온다 벽에닿으면 그대로 뒤로돌아간다 그리고 범위밖으로나가도 간다.
        if (Type1)
        {
            if (m_PlayerCheck)
            {


                float f_right = 0.0f;
                if (transform.position.x > dir.x)
                {
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    f_right = -1;
                }
                else if (transform.position.x < dir.x)
                {
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    f_right = 1;
                }


                m_rig2d.velocity = new Vector2(f_right * EnemySpeed * 2, m_rig2d.velocity.y);

                if (rayHitGround.collider == null || rayHitCheckWall.collider != null)
                {
                    f_right *= -1;
                    m_PlayerCheck = false;
                }
            }
        }
        //원거리 공격을 하는 적 플레이어를 발견하면 플레이어위치로 삼지창을 던질것입니다.
        if (Type2)
        {
            if (transform.position.x > dir.x)
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

            }
            else if (transform.position.x < dir.x)
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            }
        }
        //플레이어가 점프하면같이 점프하고아직미정
        if (Type3)
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