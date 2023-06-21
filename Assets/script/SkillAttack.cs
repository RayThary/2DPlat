using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : MonoBehaviour
{
    
    [SerializeField] private float m_fDaggerSpeed = 5.0f;
    [SerializeField] private float m_fArrowSpeed = 2.0f;

    [SerializeField] private Transform m_TrsArrow;//화살생성위치
    private Vector3 m_startPos;
    [SerializeField] private Transform m_TrsTarget;//범위안에있을경우 생성되서 범위안에있는 몬스터중 가장가까운 몬스터가 지정될만한곳
    private bool m_CheckEnemy=false;//닿았는지확인

    [SerializeField][Range(0, 1)] private float m_ArrowStartPos;
    [SerializeField][Range(0, 1)] private float m_ArrowEndPos;


    int m_Angle;
    private void OnBecameInvisible()
    {
        //Destroy(gameObject);
    }



    private void Start()
    {
        m_CheckEnemy = false;
        m_Angle = Random.Range(-1, 2);
        m_startPos = transform.position;
    }

    void Update()
    {
        skill2();
    }


    private void skill2()
    {
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
        if (m_ArrowEndPos >= 1.1f)
        {
            Destroy(gameObject);
        }   
    }

    private void checkClosedEnemy(RaycastHit2D[] _values)
    {
        int count = _values.Length;
        for (int i = 0; i < count; i++)
        {
            Vector2.Distance(transform.position, _values[i].transform.position);
            
        }
    }
  
}
