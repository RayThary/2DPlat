using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayForkMove : MonoBehaviour
{
    [SerializeField] private float m_fHayForkSpeed=3.0f;
    
    private Transform targetTrs;
    private Vector3 targetVec;
    private Vector3 bornPos;
    private BoxCollider2D box2d;

    [SerializeField][Range(0, 1)] private float m_fStartPos = 0;
    [SerializeField][Range(0, 1)] private float m_fEndtPos = 0;

    

    void Start()
    {
        box2d= GetComponent<BoxCollider2D>();
        targetTrs = GameManager.instance.GetPlayerTransform();
        targetVec = targetTrs.position;
        bornPos = gameObject.transform.position;
    }

    
    void Update()
    {
        if (box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            Destroy(gameObject);
        }
        
    }

    private void AttackCheck()
    {
        
        Vector3 VecTargetdis = (targetVec - transform.position).normalized;

        Vector3 m_Center = (bornPos + targetVec) * 0.5f;

        m_Center.y += 2.0f;


        if (m_fStartPos <= 1.0)
        {
            Vector2 direction = m_Center - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.position = Vector3.Lerp(bornPos, m_Center, m_fStartPos);
            m_fStartPos += Time.deltaTime * m_fHayForkSpeed;
        }

        if (m_fStartPos >= 1.0f)
        {
            float angle = Mathf.Atan2(VecTargetdis.y, VecTargetdis.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0.0f, 0.0f, 1.0f));

            transform.position = Vector3.Lerp(m_Center, targetVec, m_fEndtPos);
            m_fEndtPos += Time.deltaTime * m_fHayForkSpeed;
        }
        else if (m_fEndtPos >= 1.0f)
        {
            Destroy(gameObject);
        }

        if (transform.position.x < targetVec.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (transform.position.x > targetVec.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
