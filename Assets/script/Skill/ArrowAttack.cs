using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowAttack : MonoBehaviour
{
    [SerializeField] private float m_fArrowSpeed = 2.0f;
    private Transform m_TrsTarget;
    [SerializeField] private Transform m_TrsAroowAttackItem;

    private Transform target;
    private bool m_bThroughPlayer;

    private BoxCollider2D box2d;

    private float timer = 0.0f;
    [SerializeField] private float changeTargetXY = 5;
    private bool changeTarget;
    private float changeTargetX;
    private float changeTargetY;

    private void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        box2d = GetComponent<BoxCollider2D>();
        Destroy(gameObject, 10f);
    }

    void Update()
    {
        EnemyCheck();
        changeTargetCheck();
        
    }

   

    private void arrowMove()
    {
        Vector3 VecTargetdis = (m_TrsTarget.position - transform.position).normalized;
        Vector3 VecChangeTarget = new Vector3(m_TrsTarget.position.x + changeTargetX, m_TrsTarget.position.y + changeTargetY, m_TrsTarget.position.z);
        Vector3 VecChangeTargerdis = (VecChangeTarget - transform.position).normalized;
        
        

        RaycastHit2D ThroughPlayer = Physics2D.BoxCast(box2d.bounds.center, box2d.size, 0f, Vector3.zero, 0, LayerMask.GetMask("Enemy"));
        
        if (ThroughPlayer.collider==null&&m_bThroughPlayer==false)
        {
            transform.position += VecTargetdis * m_fArrowSpeed * Time.deltaTime;
            
            float angle = Mathf.Atan2(VecTargetdis.y, VecTargetdis.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0.0f, 0.0f, 1.0f));
            
        }
        else
        {
            changeTarget = true;
            if (timer < 1f)
            {
                m_bThroughPlayer = true;
            }
            else
            {
                m_bThroughPlayer = false;
                timer = 0;
            }   
            transform.position += VecChangeTargerdis.normalized * m_fArrowSpeed * Time.deltaTime;
            float angle = Mathf.Atan2(VecChangeTargerdis.y, VecChangeTargerdis.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0.0f, 0.0f, 1.0f));
        }
        
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
    private void changeTargetCheck()
    {
        if (m_bThroughPlayer)
        {
            timer += Time.deltaTime;
        }
        if ( changeTarget&&m_bThroughPlayer==false)
        {

            changeTargetX = Random.Range(0, 2);
            changeTargetY = Random.Range(0, 2);
            if (changeTargetX == 0)
            {
                changeTargetX = -changeTargetXY;
            }
            else if (changeTargetX == 1)
            {
                changeTargetX = changeTargetXY;
            }
            if (changeTargetY == 0)
            {
                changeTargetY = -changeTargetXY;
            }
            else if (changeTargetY == 1)
            {
                changeTargetY = changeTargetXY;
            }
            changeTarget = false;
        }
    }

    private void EnemyCheck()
    {
        RaycastHit2D[] HitEnemy = Physics2D.CircleCastAll(target.position, 10.0f, Vector3.up, 0f, LayerMask.GetMask("Enemy"));//위치지정필요

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
