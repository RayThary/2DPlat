using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : MonoBehaviour
{
    
    [SerializeField] private float m_fDaggerSpeed = 5.0f;
    [SerializeField] private float m_fArrowSpeed = 2.0f;

    [SerializeField] private Transform m_TrsArrow;//화살생성위치
    private Vector3 m_startPos;
    private Transform m_TrsTarget;//범위안에있을경우 생성되서 범위안에있는 몬스터중 가장가까운 몬스터가 지정될만한곳
    

    [SerializeField][Range(0, 1)] private float m_ArrowStartPos;
    [SerializeField][Range(0, 1)] private float m_ArrowEndPos;

    int m_Angle;
    private void OnBecameInvisible()
    {
        //Destroy(gameObject);
    }



    private void Start()
    {
        m_Angle = Random.Range(-1, 2);
        m_startPos = transform.position;
        
    }

    void Update()
    {
        
        checkEnemy();
    }
    private void checkEnemy()
    {
        RaycastHit2D[] HitEnemy = Physics2D.CircleCastAll(transform.position, 5.0f, Vector3.up, 0f, LayerMask.GetMask("Enemy"));

        if (HitEnemy.Length != 0)
        {            
            checkClosedEnemy(HitEnemy);
        }

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

        for(int x = 0; x < count; x++)
        {
            float _DisEnemy = Vector2.Distance(transform.position, _values[x].transform.position);

            if (minimumDis == _DisEnemy)
            {
                Debug.Log(_values[x].transform.name);
                return m_TrsTarget= _values[x].transform;
            }
        }
        return null;
    }
  
}
