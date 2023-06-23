using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : MonoBehaviour
{

    [SerializeField] private float m_fDaggerSpeed = 5.0f;
    [SerializeField] private float m_fArrowSpeed = 2.0f;

    
    private Vector3 m_VecStart;
    private Transform m_TrsTarget;
    [SerializeField] private Transform m_test;

    [SerializeField] private Transform m_TrsAroowAttackItem;
    private float m_ArrowStartPos;
    private float m_ArrowEndPos;

    int m_Angle;

    private void Start()
    {
        m_Angle = Random.Range(-1, 2);
        m_VecStart = transform.position;

    }

    void Update()
    {
        //arrowMove();
        Vector2.Distance(transform.position, m_test.position);
        //Vector2.MoveTowards()
        //디스턴스
    }



    private void arrowMove()
    {
        
        Vector3 m_EndPos = m_TrsTarget.position;
        transform.Translate(m_TrsTarget.position * Time.deltaTime);

        m_VecStart = m_VecStart + m_TrsTarget.position * Time.deltaTime*m_fArrowSpeed;

        /*곡선 실패나중에활용해볼가능성있음
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
    }



    //private Transform checkClosedEnemy(RaycastHit2D[] _values)
    //{
    //    int count = _values.Length;
    //    float beforeDis = 0;
    //    float minimumDis = 0;

    //    if (count == 1)
    //    {
    //        return _values[0].transform;
    //    } 

    //    for (int i = 0; i < count; i++)
    //    {
    //        float _DisEnemy = Vector2.Distance(transform.position, _values[i].transform.position);//1 3 2
    //        if (i == 0)
    //        {
    //            minimumDis = _DisEnemy;
    //        }
    //        else if (_DisEnemy < beforeDis)
    //        {
    //            minimumDis = _DisEnemy;
    //        }

    //        beforeDis = _DisEnemy;
    //    }

    //    for(int x = 0; x < count; x++)
    //    {
    //        float _DisEnemy = Vector2.Distance(transform.position, _values[x].transform.position);

    //        if (minimumDis == _DisEnemy)
    //        {
    //            Debug.Log(_values[x].transform.name);
    //            return m_TrsTarget= _values[x].transform;
    //        }
    //    }
    //    return null;
    //}

    public void SetTarget(Transform _trs)
    {
        m_TrsTarget = _trs;
    }
}
