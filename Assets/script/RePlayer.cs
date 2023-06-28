using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RePlayer : MonoBehaviour
{
    [SerializeField, Tooltip("속도")] private float m_playerspeed = 4;
    [Header("점프관련")]
    [SerializeField,Tooltip("중력")] private float m_jumpGravity = 0f;
    [SerializeField,Tooltip("점프력")] private float m_playerjump = 5f;
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
    private KeyCode beforeKeycode;

    [SerializeField] private float playerDashCoolTime = 0.0f;//대시 쿨타임
    [SerializeField] private float playerDashMaxCoolTime = 5.0f;

    private float playerDashTimer = 0.0f;//대시 다시누르기까지의 시간
    [SerializeField] private float playerDashLimit = 0.2f;

    float timer = 0.0f;//대시를 하는시간
   [SerializeField] float dashLimitTimer = 0.2f;

    [SerializeField] private LayerMask GroundCheck;
    private bool passCheck;

    private Rigidbody2D m_rig2d;

    private Vector3 moveDir;
    private UnityAction action = null;

    void Start()
    {
        m_rig2d = GetComponent<Rigidbody2D>();
        GameManager.instance.SetPlayer(this);
    }

    void Update()
    {

        playerMove();
        jumpCheck();
        playerDoubleTapCheck();
        playerDash();
        jumpGravity();
        passChecking();
    }

    public void SetAction(UnityAction _action)
    {
        action += _action;
    }

    public void OnAction()
    {
        action?.Invoke();
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
            m_PlayerDoubleTap = true;
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
            m_PlayerDoubleTap = true;
        }

        m_rig2d.velocity = moveDir * m_playerspeed;
    }
    private void playerDoubleTapCheck()
    {
        if (playerDashCoolTime < playerDashMaxCoolTime)
        {
            m_PlayerRightDash = false;
            m_PlayerLeftDash = false;
            return;
        }

        if (m_PlayerDoubleTap)
        {
            playerDashTimer += Time.deltaTime;
        }
        if (playerDashTimer >= playerDashLimit)
        {
            m_PlayerDoubleTap = false;
            m_PlayerRightDash = false;
            m_PlayerLeftDash = false;
        }
        if (m_PlayerDoubleTap==false)
        {
            playerDashTimer = 0;
        }

        if (m_PlayerDoubleTap && beforeKeycode == KeyCode.RightArrow && Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_PlayerRightDash = true;
            playerDashCoolTime = 0;
            playerDashTimer = 0;
            timer = 0;
        }
        else if (m_PlayerDoubleTap && beforeKeycode == KeyCode.LeftArrow && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_PlayerLeftDash = true;
            playerDashCoolTime = 0;
            playerDashTimer = 0;
            timer = 0;
        }

    }

    private void playerDash()
    {
        playerDashCoolTime += Time.deltaTime;

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
                m_jumpGravity = 5.0f;
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

    private void passChecking()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 1, LayerMask.GetMask("PassWall"));
        if (hit.collider != null)
        {
            passCheck = true;
            m_groundcheck = false;
        }
        if (hit.collider == null)
        {
            passCheck = false;
        }
    }

    public void OnTriggerPlayer(HitBoxParent.eHitBoxState _state, HitBoxParent.HitType _hitType, Collider2D _collision)
    {
        switch (_state)
        {
            case HitBoxParent.eHitBoxState.Enter:
                switch (_hitType)
                {
                    case HitBoxParent.HitType.Ground:
                        if (_collision.gameObject.layer == LayerMask.NameToLayer("Ground")) 
                        {
                            if (passCheck==false)
                            {
                                m_groundcheck = true;
                                m_doublecheck = true;
                            }
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
                        if (_collision.gameObject.layer == LayerMask.NameToLayer("Ground")) 
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
