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
    private bool m_doublejump;
    private bool m_doublecheck = true;
     [SerializeField]private bool m_groundcheck = false;

    [Header("플레이어 대쉬관련")]

    private bool m_playerDash;
    private bool m_PlayerRightDash;
    private bool m_PlayerLeftDash;
    private bool m_PlayerDoubleTap;
    private bool m_PlayerDashing;
    private bool right;
    private bool left;
    private float playerDashCoolTime = 0.0f;//대시 쿨타임
    [SerializeField] private float playerDashMaxCoolTime = 4.9f;

    private float playerDashTimer = 0.0f;//대시 다시누르기까지의 시간
    [SerializeField] private float playerDashLimit = 1.0f;

    float timer = 0.0f;//대시를 하는시간
    [SerializeReference] float dashLimitTimer = 0.2f;

    [SerializeField] private LayerMask GroundCheck;

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
        PlayerDashCheck();
        test();
    }

    private void playerMove()
    {
        if (m_PlayerDashing)
        {
            return;
        }


        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDir.x = 1;
            transform.localScale = new Vector3(1, 1, 1);

        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            moveDir.x = 0;
            m_PlayerDoubleTap = false;
            beforeKeycode = KeyCode.RightArrow;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir.x = -1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moveDir.x = 0;
            beforeKeycode = KeyCode.LeftArrow;
            m_PlayerDoubleTap = false;
        }

        m_rig2d.velocity = moveDir * m_playerspeed;
    }


    private void test()
    {
        if (playerDashCoolTime < playerDashMaxCoolTime)
        {
            m_PlayerRightDash = false;
            return;
        }
        if (m_PlayerDoubleTap == false)
        {
            playerDashTimer += Time.deltaTime;
        }
        if (playerDashTimer >= playerDashLimit)
        {
            m_PlayerDoubleTap = true;
            m_PlayerRightDash = false;
        }
        if (m_PlayerDoubleTap)
        {
            playerDashTimer = 0;
        }
        if (m_PlayerDoubleTap == false && beforeKeycode == KeyCode.RightArrow && Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_PlayerRightDash = true;
            playerDashCoolTime = 0;
            playerDashTimer = 0;
            timer = 0;
        }
        else if (m_PlayerDoubleTap == false && beforeKeycode == KeyCode.LeftArrow && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_PlayerLeftDash = true;
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

        if (m_PlayerRightDash)
        {
            m_PlayerDashing = true;
            moveDir.x = 4.0f;
        }
        else if (m_PlayerLeftDash)
        {
            m_PlayerDashing = true;
            moveDir.x = -4.0f;
        }
        if (m_PlayerDashing)
        {
            timer += Time.deltaTime;
            m_rig2d.velocity = moveDir * m_playerspeed;
            if (timer >= dashLimitTimer)
            {
                m_PlayerDashing = false;
                m_PlayerRightDash = false;
                m_PlayerLeftDash = false;
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


    public void OnTriggerPlayer(HitBoxParent.eHitBoxState _state, HitBoxParent.HitType _hitType, Collider2D _collision)
    {
        switch (_state)
        {
            case HitBoxParent.eHitBoxState.Enter:
                switch (_hitType)
                {
                    case HitBoxParent.HitType.Ground:
                        if (_collision.gameObject.layer == LayerMask.NameToLayer("Ground") ||
                           _collision.gameObject.layer == LayerMask.NameToLayer("PassWall"))
                        {
                                m_groundcheck = true;
                                m_doublecheck = true;
                        }

                        break;
                    case HitBoxParent.HitType.Wall:

                        break;
                }
                break;
            case HitBoxParent.eHitBoxState.Stay:
                switch (_hitType)
                {
                    case HitBoxParent.HitType.Ground:
                        
                        break;
                    case HitBoxParent.HitType.PassWall:

                        break;
                }
                break;
            case HitBoxParent.eHitBoxState.Exit:
                switch (_hitType)
                {
                    case HitBoxParent.HitType.Ground:
                        if (_collision.gameObject.layer == LayerMask.NameToLayer("Ground") ||
                            _collision.gameObject.layer == LayerMask.NameToLayer("PassWall"))
                        {
                            m_groundcheck = false;
                        }
                        break;
                    case HitBoxParent.HitType.PassWall:
                        
                        break;
                }
                break;
        }
    }
}
