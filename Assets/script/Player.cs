using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_playerspeed = 4;
    [Header("플레이어 점프관련")]
    [SerializeField] private float m_jumpGravity = 0f;
    [SerializeField] private float m_playerjump = 5f;
    private float m_gravity = 9.81f;
    private bool m_jumpcheck = false;
    [SerializeField] private bool m_doublejump;
    private bool m_doublecheck = true;
    [SerializeField] private bool m_groundcheck = false;
    [Header("플레이어 공격관련")]
    [SerializeField] private GameObject m_AttBorn;
    [SerializeField] private GameObject m_Attack1;
    [SerializeField] private Transform m_trsGameObject;
    public bool skill1;
    public bool skill2;
    [Header("플레이어 대쉬관련")]

    [SerializeField] private bool m_playerDash;
    [SerializeField] private bool one;
    [SerializeField] private bool two;
    [SerializeField] private bool three;
    [SerializeField] private bool four;
    [SerializeField] private bool m_dashRightCheck;
    [SerializeField] private bool m_dashLeftCheck;
    [SerializeField] private float playerDashCoolTime = 0.0f;//대시 쿨타임
    [SerializeField] private float playerDashMaxCoolTime = 4.9f;

    [SerializeField] private float playerDashTimer = 0.0f;//대시 다시누르기까지의 시간
    [SerializeField] private float playerDashLimit = 1.0f;

    [SerializeField] float timer = 0.0f;//대시를 하는시간
    [SerializeReference] float dashLimitTimer = 0.2f;

    public bool DownWalk;

    private Rigidbody2D m_rig2d;

    private Vector3 moveDir;

    private KeyCode beforeKeycode;

    void Start()
    {
        m_rig2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerMove();
        jumpCheck();
        jumpGravity();
        AttackSkill1();
        PlayerDashCheck();
        test();
    }

    private void playerMove()
    {
        if (four)
        {
            return;
        }


        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (three)
            {
                m_dashRightCheck = true;
            }
            moveDir.x = 1;
            transform.localScale = new Vector3(1, 1, 1);

        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            moveDir.x = 0;
            m_dashRightCheck = false;
            three = false;
            beforeKeycode = KeyCode.RightArrow;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (three)
            {
                m_dashLeftCheck = true;
            }
            moveDir.x = -1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moveDir.x = 0;
            beforeKeycode = KeyCode.LeftArrow;
            three = false;
            m_dashLeftCheck = false;
        }


        m_rig2d.velocity = moveDir * m_playerspeed;
    }


    private void test()
    {
        if (playerDashCoolTime < playerDashMaxCoolTime)
        {
            one = false;
            return;
        }
        if (three == false)
        {
            playerDashTimer += Time.deltaTime;
        }
        if (playerDashTimer >= playerDashLimit)
        {
            three = true;
            one = false;
        }
        if (three)
        {
            playerDashTimer = 0;
        }
        if (three == false && beforeKeycode == KeyCode.RightArrow && Input.GetKeyDown(KeyCode.RightArrow))
        {
            one = true;
            playerDashCoolTime = 0;
            playerDashTimer = 0;
            timer = 0;
        }
        else if (three == false && beforeKeycode == KeyCode.LeftArrow && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            two = true;
            playerDashCoolTime = 0;
            playerDashTimer = 0;
            timer = 0;
        }



    }

    private void PlayerDashCheck()
    {
        playerDashCoolTime += Time.deltaTime;
        if (playerDashCoolTime > playerDashMaxCoolTime)
        {
            playerDashCoolTime = 6;
        }

        if (one)
        {
            four = true;
            moveDir.x = 4.0f;
        }
        else if (two)
        {
            four = true;
            moveDir.x = -4.0f;
        }
        if (four)
        {
            timer += Time.deltaTime;
            m_rig2d.velocity = moveDir * m_playerspeed;
            if (timer >= dashLimitTimer)
            {
                four = false;
                one = false;
                two = false;
                moveDir.x = 0.0f;
            }
        }

    }



    private void jumpCheck()
    {
        if (m_groundcheck == false)
        {

            if (Input.GetKeyDown(KeyCode.UpArrow) && m_doublecheck)
            {
                m_doublejump = true;
                m_doublecheck = false;
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_jumpcheck = true;
        }

    }

    private void jumpGravity()
    {
        if (m_groundcheck == false)
        {

            m_jumpGravity -= m_gravity * Time.deltaTime;
            if (m_jumpGravity < -10f)
            {
                m_jumpGravity = -10;
            }
            if (m_doublejump)
            {
                m_jumpGravity = 10.0f;
                m_doublejump = false;
            }
        }
        else
        {
            if (m_jumpcheck)
            {
                m_jumpcheck = false;
                m_jumpGravity = m_playerjump;
            }
            else if (m_jumpGravity < 0)
            {
                m_jumpGravity += m_gravity * 3 * Time.deltaTime;
                if (m_jumpGravity > 0)
                {
                    m_jumpGravity = 0;

                }
            }
        }

        m_rig2d.velocity = new Vector2(m_rig2d.velocity.x, m_jumpGravity);
    }

    private void AttackSkill1()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(m_Attack1, m_AttBorn.transform.position, Quaternion.identity, m_trsGameObject);
        }
    }


    public void OnTriggerPlayer(HitBox.eHitBoxState _state, HitBox.HitType _hitType, Collider2D _collision)
    {
        switch (_state)
        {
            case HitBox.eHitBoxState.Enter:
                switch (_hitType)
                {
                    case HitBox.HitType.Ground:
                        if (_collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
                        {
                            m_groundcheck = true;
                            m_doublecheck = true;
                        }
                        if (_collision.gameObject.layer == LayerMask.NameToLayer("PassWall"))
                        {
                            m_groundcheck = false;
                        }
                            break;
                    case HitBox.HitType.Wall:

                        break;
                }
                break;
            case HitBox.eHitBoxState.Stay:
                switch (_hitType)
                {
                    case HitBox.HitType.Ground:
                        
                        break;
                    case HitBox.HitType.PassWall:
                        
                        break;
                }
                break;
            case HitBox.eHitBoxState.Exit:
                switch (_hitType)
                {
                    case HitBox.HitType.Ground:
                        if (_collision.gameObject.layer == LayerMask.NameToLayer("Ground")) 
                        {
                            m_groundcheck = false;
                        }
                        break;
                    case HitBox.HitType.PassWall:
                        
                        break;
                }
                break;
        }

    }
}
