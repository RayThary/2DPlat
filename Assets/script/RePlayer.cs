using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool m_groundcheck = false;

    [Header("플레이어 대쉬관련")]

    private bool m_playerDash;
    private bool m_PlayerRightDash;
    private bool m_PlayerLeftDash;
    private bool m_PlayerDoubleTap;
    private bool m_PlayerDashing;
    private bool m_dashRightCheck;
    private bool m_dashLeftCheck;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
