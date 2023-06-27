using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowAttack : MonoBehaviour
{

    private RePlayer player;
    [SerializeField] private float m_fArrowSpeed = 2.0f;

    
    
    private Transform m_TrsTarget;
    [SerializeField] private Transform m_test;

    [SerializeField] private Transform m_TrsAroowAttackItem;
    private float m_ArrowStartPos;
    private float m_ArrowEndPos;

    [Header("테스트용")]
    private Rigidbody2D rig2d;
    private Transform target;
    
    private bool check = false;




    private void Start()
    {
        //실험
        rig2d= GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        Destroy(gameObject, 7f);
    }

    void Update()
    {
        EnemyCheck();
        
    }

   

    private void arrowMove()
    {
        Vector3 dir = (m_TrsTarget.position - transform.position).normalized;

        transform.position += dir * m_fArrowSpeed * Time.deltaTime;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0.0f,0.0f,1.0f));

        #region 실패나중에활용해볼가능성있음
        /*
        transform.position = Vector2.MoveTowards(transform.position, target.position, m_fArrowSpeed * Time.deltaTime);

         Vector3 m_startPos = m_TrsArrow.position;
         Vector3 m_EndPos = m_TrsTarget.position;


         Vector3 m_Cnter= (m_startPos+m_EndPos) * 0.5f;
         m_Cnter.y = m_Angle * 2;
         m_ArrowStartPos += Time.deltaTime * m_fArrowSpeed;


        transform.position = Vector3.Slerp(m_TrsArrow.position, m_Cnter, m_ArrowStartPos);


        if (m_ArrowStartPos >= 1.0f)
        {
             transform.position = Vector3.Slerp(m_Cnter, m_TrsTarget.position, m_ArrowEndPos);
             m_ArrowEndPos += Time.deltaTime * m_fArrowSpeed;
        }

        if (m_ArrowEndPos >= 1.0f)
        {
             transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        }   
        */
        #endregion
    }

    private void EnemyCheck()
    {
        RaycastHit2D[] HitEnemy = Physics2D.CircleCastAll(target.position, 5.0f, Vector3.up, 0f, LayerMask.GetMask("Enemy"));//위치지정필요

        if (HitEnemy.Length != 0)
        {
            m_TrsTarget = checkClosedEnemy(HitEnemy);
            arrowMove();
        }
        else 
        {
            Destroy(this); 
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
                return _values[x].transform;
            }
        }
        return null;
    }

   
}
