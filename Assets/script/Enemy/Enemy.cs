using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("�Ѱ���üũ���ּ���")]
    [Header("�� Ÿ�� 1����  2���Ÿ�  3����")]
    [SerializeField] private bool Type1;
    [SerializeField] private bool Type2;
    [SerializeField] private GameObject Type2AttackWeapon;
    [SerializeField] private bool Type3;
    private BoxCollider2D box2d;
    private Transform m_TrsBefore;

    [SerializeField] private float EnemySpeed = 1.0f;
    private float beforeSpeed = 0.0f;
    private bool m_PlayerCheck;
    [Header("����������")]
    [SerializeField] private float m_EnemyPlayerCheckRange = 5.0f;//Type1:5, Typ2:10 

    [SerializeField]private int NextMove;//���������ӿ��ʿ�����������������
    [SerializeField] private float timer =0;
    private float waittimer = 0;
    [Header("�����ӹٲٴ½ð�")]
    [SerializeField]private float ChangeTime = 8.0f;
    [SerializeField]private bool falling;

    private Rigidbody2D m_rig2d;

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
        //target = GameObject.Find("Player").GetComponent<Transform>();
        target = GameManager.instance.GetPlayerTransform();
        box2d = GetComponent<BoxCollider2D>();
        beforeSpeed = EnemySpeed;
    }

    
    void Update()
    {
        ChangeMove();
        CheckPlayer();

        if (box2d.IsTouchingLayers(LayerMask.GetMask("FallingGround")))
        {
            falling = true;
        }
       
    }

    

    private void CheckPlayer()
    {
        m_rig2d.velocity = new Vector2(NextMove * EnemySpeed, m_rig2d.velocity.y);
        Vector2 frontVec = new Vector2(m_rig2d.position.x + NextMove, m_rig2d.position.y);// �̿�����Ʈ x+1�� ��ġ

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
        //���Ÿ� ������ �ϴ� �� �÷��̾ �߰��ϸ� �÷��̾���ġ�� �����������
        if (Type2)
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
        //�÷��̾ �����ϴ� ��
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
        if (NextMove == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (NextMove == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
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