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
    [Header("플레이어 대쉬관련")]

    [SerializeField]private bool m_playerDash;
    private bool one;
    [SerializeField]private float playerDashCoolTime = 0.0f;
    [SerializeField]private float playerDashMaxCoolTime = 4.9f;
    [SerializeField] private float playerDashLimit = 1.0f;
    private float playerDashTimer = 0.0f;

    [SerializeField] private bool downWalk;


    private Rigidbody2D m_rig2d;

    private Vector3 moveDir;

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
        playerDash();
    }

    private void playerMove()
    {
        //if (one)
        //{
        //    return;
        //}
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDir.x = 1;
            transform.localScale = new Vector3(1, 1, 1);
            m_playerDash = true;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            moveDir.x = 0;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            m_playerDash = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir.x = -1;
            transform.localScale = new Vector3(-1, 1, 1);
            m_playerDash = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moveDir.x = 0;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            m_playerDash = false;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

        }

        m_rig2d.velocity = moveDir * m_playerspeed;
    }

    private void playerDash()
    {
        playerDashCoolTime += Time.deltaTime;
        playerDashTimer += Time.deltaTime;
        if (m_playerDash && playerDashTimer<playerDashLimit && Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            one = true;
            m_rig2d.velocity += new Vector2(10.0f, 0f);
        }
        else if (one)
        {
            
        }
        if (playerDashTimer > playerDashLimit)
        {
            playerDashTimer = 0;
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
        if (one)
        {
            return;
        }
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


    public void OnTrigger(HitBox.eHitBoxState _state, HitBox.HitType _hitType, Collider2D _collision)
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
                    case HitBox.HitType.Wall:

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
                    case HitBox.HitType.Wall:

                        break;
                }
                break;
        }

    }
}
