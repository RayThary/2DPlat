using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePlayer : MonoBehaviour
{
   
    [SerializeField, Tooltip("�ӵ�")] private float m_playerspeed = 4;
    [Header("��������")]
    [SerializeField,Tooltip("�߷�")] private float m_jumpGravity = 0f;
    [SerializeField,Tooltip("������")] private float m_playerjump = 5f;
    private float m_gravity = 9.81f;
    private bool m_jumpcheck = false;
    private bool m_doublejump;
    private bool m_doublecheck = true;
    private bool m_groundcheck = false;

    [Header("�÷��̾� �뽬����")]

    private bool m_playerDash;
    private bool m_PlayerRightDash;
    private bool m_PlayerLeftDash;
    private bool m_PlayerDoubleTap;
    private bool m_PlayerDashing;
    private bool m_dashRightCheck;
    private bool m_dashLeftCheck;
    private float playerDashCoolTime = 0.0f;//��� ��Ÿ��
    [SerializeField] private float playerDashMaxCoolTime = 4.9f;

    private float playerDashTimer = 0.0f;//��� �ٽô���������� �ð�
    [SerializeField] private float playerDashLimit = 1.0f;

    float timer = 0.0f;//��ø� �ϴ½ð�
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
