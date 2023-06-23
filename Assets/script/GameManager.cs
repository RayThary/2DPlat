using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private SkillAttack skillArrow;
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
    }

    private void Start()
    {
        skillArrow = GetComponent<SkillAttack>();
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

        RaycastHit2D[] HitEnemy = Physics2D.CircleCastAll(transform.position, 5.0f, Vector3.up, 0f, LayerMask.GetMask("Enemy"));//위치지정필요

        if (HitEnemy.Length != 0)
        {
            
            checkClosedEnemy(HitEnemy);
            skillArrow.SetTarget(targetpos);
            Instantiate(Arrow);
        }
        else
        {
            Debug.Log("범위안에 적이없습니다.");//Instantiate안되는방법 ? 아님바로디스트로이
        }

    }

    private Transform checkClosedEnemy(RaycastHit2D[] _values)
    {
        int count = _values.Length;
        float beforeDis = 0;
        float minimumDis = 0;
      

        if (count == 1)
        {
            return _values[0].transform;
        }

        for (int i = 0; i < count; i++)
        {
            float _DisEnemy = Vector2.Distance(transform.position, _values[i].transform.position);//1 3 2
            if (i == 0)
            {
                minimumDis = _DisEnemy;
            }
            else if (_DisEnemy < beforeDis)
            {
                minimumDis = _DisEnemy;
            }

            beforeDis = _DisEnemy;
        }

        for (int x = 0; x < count; x++)
        {
            float _DisEnemy = Vector2.Distance(transform.position, _values[x].transform.position);

            if (minimumDis == _DisEnemy)
            {
                Debug.Log(_values[x].transform.name);
                return targetpos = _values[x].transform;
            }
        }
        return null;
    }



}
