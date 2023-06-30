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
    [SerializeField] private Transform m_TrsArrow;//화살생성위치

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

        //player = GameObject.Find("Player").GetComponent<RePlayer>();
        player = FindObjectOfType<RePlayer>();
    }

    private void Start()
    {
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

        RaycastHit2D[] HitEnemy = Physics2D.CircleCastAll(player.transform.position, 5.0f, Vector3.up, 0f, LayerMask.GetMask("Enemy"));//위치지정필요

        if (HitEnemy.Length != 0)
        {
            
        }
        
    }

    public void SetPlayer(RePlayer _player)
    {
        player = _player;
    }

    public RePlayer GetPlayer()
    {
        return player;
    }
    public Transform GetPlayerTransform() 
    {
        return player.transform;
    }
}
