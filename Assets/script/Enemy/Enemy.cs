using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("한개만체크해주세요")]
    [Header("적 타입 1근접  2원거리  3미정")]
    [SerializeField] private bool Type1;
    [SerializeField] private bool Type2;
    [SerializeField] private GameObject Type2AttackWeapon;
    [SerializeField] private bool Type3;
    private BoxCollider2D box2d;
    private Transform m_TrsBefore;

    [SerializeField] private float EnemySpeed = 1.0f;
    private float beforeSpeed = 0.0f;
    private bool m_PlayerCheck;
    [Header("적인지범위")]
    [SerializeField] private float m_EnemyPlayerCheckRange = 5.0f;//Type1:5, Typ2:10 

    [SerializeField]private int NextMove;//다음움직임왼쪽오른쪽일지랜덤생성
    [SerializeField] private float timer =0;
    private float waittimer = 0;
    [Header("움직임바꾸는시간")]
    [SerializeField]private float ChangeTime = 8.0f;
    [SerializeField]private bool falling;

    private bool  m_bReCheckPlayer;
    private float m_fChecktimer=0.0f;
    private float m_fReCheckPlayerTimer = 3.0f;

    private Rigidbody2D m_rig2d;

    private Transform target;

    [SerializeField]private int m_fEnemyHp=5;


    //적 점프관련
    [SerializeField]private bool playerJumpCheck;
    private float jumpGravity = 0.0f;
    private float jumpingPower=5.0f;
    private RePlayer player;
    private float m_gravity = 9.81f;
    private bool GroundCheck;
    private bool oneJump;

    private bool AxeAttackCheck;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, m_EnemyPlayerCheckRange);
        
        
    }
    //axe 가 내려찍을때 플레이어에서 bool값 true 해주고 true 됬을때 닿아있는게 axe면 axe 에서 damege만큼 빼준다
    void Start()
    {
        player = GameManager.instance.GetPlayer();
        NextMove = 1;
        m_rig2d = GetComponent<Rigidbody2D>();
        //target = GameObject.Find("Player").GetComponent<Transform>();
        target = GameManager.instance.GetPlayerTransform();
        box2d = transform.Find("CheckFalling").GetComponent<BoxCollider2D>();
        
        beforeSpeed = EnemySpeed;
    }

    
    void Update()
    {
        ChangeMove();
        CheckPlayer();
        reCheckPlayer();
        Type3Move();
        CheckFalling();
        EnemyHp();
       
    }


    private void CheckPlayer()
    {
        m_rig2d.velocity = new Vector2(NextMove * EnemySpeed, m_rig2d.velocity.y);
        Vector2 frontVec = new Vector2(m_rig2d.position.x + NextMove, m_rig2d.position.y);// 이오브젝트 x+1의 위치

        RaycastHit2D rayHitGround = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        RaycastHit2D rayHitCheckWall = Physics2D.Raycast(frontVec, Vector3.zero, 1, LayerMask.GetMask("SideWall", "Enemy"));
        
        if (rayHitGround.collider == null || rayHitCheckWall.collider != null)
        {
            if (falling==false)
            {
                NextMove *= -1;
            }
        }

        if (rayHitGround.collider != null)
        {
            falling = false;
        }

        RaycastHit2D rayHitPlayer = Physics2D.CircleCast(transform.position, m_EnemyPlayerCheckRange
            , Vector3.up, 0f, LayerMask.GetMask("Player"));

        Vector3 dir = target.position;

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
                    m_bReCheckPlayer = true;
                }
            }
        }
        //원거리 공격을 하는 적 플레이어를 발견하면 플레이어위치로 무기던지는적
        if (Type2)
        {
            if (m_PlayerCheck)
            {
                Vector2 m_center = (transform.position + target.position) * 0.5f;
                RaycastHit2D hitGround = Physics2D.Raycast(m_center, transform.position - target.position, 8f,
                    LayerMask.GetMask("Ground,FallinWall"));

                if (hitGround.collider != null)
                {
                    Debug.Log("ground check");
                }
                if (transform.position.x > dir.x)
                {
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

                }
                else if (transform.position.x < dir.x)
                {
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }
                Instantiate(Type2AttackWeapon);
                if (rayHitGround.collider == null || rayHitCheckWall.collider != null)
                {

                }
                m_rig2d.velocity = new Vector2(NextMove, m_rig2d.velocity.y);
            }
        }
        //플레이어가 따라하는 적
        if (Type3)
        {
            if (m_PlayerCheck)
            {
                playerJumpCheck= player.Type3Check(playerJumpCheck);
                if (playerJumpCheck)
                {
                    jumpGravity = jumpingPower;
                    oneJump = true;
                }
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
                    m_bReCheckPlayer = true;
                }
            }
        }
    }
    private void Type3Move()
    {
        if (oneJump == false)
        {
            return;
        }

        jumpGravity -= m_gravity * Time.deltaTime;

        m_rig2d.velocity = new Vector2(m_rig2d.velocity.x, jumpGravity);
        if (GroundCheck == false)
        {
            if (jumpGravity < -10f)
            {
                jumpGravity = -10;
            }
        }
        else
        { 
            if (jumpGravity > 0)
            {
                jumpGravity += m_gravity * 3 * Time.deltaTime;
                if (jumpGravity > 0)
                {
                    jumpGravity = 0;
                    oneJump = false;
                }
            }
        }
    }
    
    private void reCheckPlayer()
    {
        if (m_bReCheckPlayer)
        {
            m_fChecktimer += Time.deltaTime;
            if (m_fChecktimer >= m_fReCheckPlayerTimer)
            {
                m_bReCheckPlayer = false;
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
                timer = 7;
            }
            else
            {
                timer = 0;
            }
        }
        if (NextMove == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (NextMove == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    private void CheckFalling()
    {
        RaycastHit2D fall = Physics2D.BoxCast(box2d.bounds.center, box2d.size, 0, Vector2.zero, 1f, LayerMask.GetMask("FallingGround"));
        if(fall.collider != null)
        {
            falling = true;
        }
    }
    public void EnemyHp()
    {
        if (m_fEnemyHp <= 0)
        {
            Destroy(gameObject);
        }
        //GameManager.instance.GetPlayerAttack(AxeAttackCheck);
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
                            
                            break;
                    }
                }
                break;
                case HitBoxParent.eHitBoxState.Stay:
                {
                    switch (_hitType) 
                    {
                        case HitBoxParent.HitType.Ground:
                            if(_collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
                            {
                                GroundCheck = true;
                            }
                            break;
                    }
                }
                break;
            case HitBoxParent.eHitBoxState.Exit:
                {
                    switch (_hitType)
                    {
                        case HitBoxParent.HitType.Ground:
                            if (_collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
                            {
                                GroundCheck = false;
                            }
                            break;
                    }
                }
                break;
        }
    }


  

}