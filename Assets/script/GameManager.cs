using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private RePlayer player;
    private ArrowAttack skillArrow;
    private Transform targetpos;
    private bool ArrowAttack = false;
    [SerializeField] private GameObject Arrow;
    [SerializeField] private BoxCollider2D box2d;
    [SerializeField] private LayerMask m_Player;
    [SerializeField] private Transform m_TrsArrow;//ȭ�������ġ

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        player = GetComponent<RePlayer>();
        skillArrow = GetComponent<ArrowAttack>();
    }

    void Update()
    {
        ItemCheck();
    }

    private void ItemCheck()
    {
        if (box2d.IsTouchingLayers(m_Player) == true)
        {
            ArrowAttack = true;
        }
        if(ArrowAttack&& Input.GetKeyDown(KeyCode.A))
        {
            checkEnemy();
        }
    }
    private void checkEnemy()
    {

        RaycastHit2D[] HitEnemy = Physics2D.CircleCastAll(player.transform.position, 5.0f, Vector3.up, 0f, LayerMask.GetMask("Enemy"));//��ġ�����ʿ�

        if (HitEnemy.Length != 0)
        {
            Instantiate(Arrow);
        }
        else
        {
            Debug.Log("�����ȿ� ���̾����ϴ�.");//Instantiate�ȵǴ¹�� ? �ƴԹٷε�Ʈ����
        }
    }

}
