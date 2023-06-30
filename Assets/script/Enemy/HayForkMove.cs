using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayForkMove : MonoBehaviour
{
    [SerializeField] private float m_fHayForkSpeed=3.0f;
    
    private Transform targetTrs;
    private Vector3 bornPos;
    
    [SerializeField][Range(0, 1)] private float m_fStartPos = 0;
    [SerializeField][Range(0, 1)] private float m_fEndtPos = 0;
    void Start()
    {
        targetTrs = GameManager.instance.GetPlayerTransform();
        bornPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 VecTargetdis = (targetTrs.position - transform.position).normalized;
        
        


        Vector3 m_EndPos = targetTrs.position;
        Vector3 m_Cnter= (bornPos + m_EndPos) * 0.5f;

        m_Cnter.y +=  2.0f;


        if (m_fStartPos <= 1.0)
        {
            
            transform.position = Vector3.Lerp(bornPos, m_Cnter, m_fStartPos);
            m_fStartPos += Time.deltaTime * m_fHayForkSpeed;
        }

        if (m_fStartPos >= 1.0f)
        {   
            float angle = Mathf.Atan2(VecTargetdis.y, VecTargetdis.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0.0f, 0.0f, 1.0f));
         
            transform.position = Vector3.Lerp(m_Cnter, targetTrs.position, m_fEndtPos);
            m_fEndtPos += Time.deltaTime * m_fHayForkSpeed;
        }
      
    }
}
