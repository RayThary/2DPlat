using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("�� Ÿ�� 1����  2���Ÿ�  3����   �Ѱ���üũ���ּ���")]
    [SerializeField] private bool Type1;
    [SerializeField] private bool Type2;
    [SerializeField] private bool Type3;

    private Transform m_TrsBefore;

    [SerializeField] private float EnemySpeed = 1.0f;
    private float beforeSpeed = 0.0f;
    private bool m_PlayerCheck;
    [SerializeField] private float m_EnemyPlayerCheckRange = 5.0f;

    private int NextMove;//���������ӿ��ʿ�����������������
    private float timer =0;
    private float waittimer = 0;
    [Header("�����ӹٲٴ½ð�")]
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

        //�⺻������
        m_rig2d.velocity = new Vector2(NextMove * EnemySpeed, m_rig2d.velocity.y);

        Vector2 frontVec = new Vector2(m_rig2d.position.x + NextMove, m_rig2d.position.y);// �̿�����Ʈ x+1�� ��ġ

        RaycastHit2D rayHitGround = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        RaycastHit2D rayHitCheckWall = Physics2D.Raycast(frontVec, Vector3.zero, 1, LayerMask.GetMask("SideWall", "Enemy"));
        RaycastHit2D rayHitFailling = Physics2D.Raycast(frontVec, Vector3.zero, 1, LayerMask.GetMask("FaillingWall"));

        Vector3 dir = target.position;//���� ��ġ

        if (rayHitGround.collider == null || rayHitCheckWall.collider != null)
        {
            NextMove *= -1;
        }
        if (rayHitFailling.collider != null)
        {
            NextMove *= -1;
        }

       

        //�÷��̾� üũ
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

        //�÷��̾ �����ȿ� ������ �÷��̾�������� 2������ӵ��� �޷��´� ���������� �״�� �ڷε��ư��� �׸��� ���������γ����� ����.
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
        //���Ÿ� ������ �ϴ� �� �÷��̾ �߰��ϸ� �÷��̾���ġ�� ����â�� �������Դϴ�.
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
        //�÷��̾ �����ϸ鰰�� �����ϰ��������
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