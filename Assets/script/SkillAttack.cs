using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : MonoBehaviour
{
    private Player player;
    [SerializeField] private float m_fDaggerSpeed = 5.0f;
    [SerializeField] private float m_fArrowSpeed = 2.0f;

    [SerializeField] private Transform m_TrsArrow;
    [SerializeField] private Transform m_TrsTarget;//범위안에있을경우 생성되서 범위안에있는 몬스터중 가장가까운 몬스터가 지정될만한곳
    private bool m_CheckEnemy;

    [SerializeField][Range(0, 1)] private float m_Angle;

    private Transform myTransform;

    Camera m_cam = null;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }



    private void Start()
    {
        m_CheckEnemy = false;
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        skill1();
        skill2();
    }

    private void skill1()
    {
        if (player.skill1)
        {
            transform.position += new Vector3(1.0f, 0, 0) * m_fDaggerSpeed * Time.deltaTime;
        }
    }

    private void skill2()
    {
        if (player.skill2)
        {
            transform.position = Vector3.Slerp(m_TrsArrow.position, m_TrsTarget.position, m_Angle);
            m_Angle += Time.deltaTime * m_fArrowSpeed;
            if (m_CheckEnemy == true)
            {
                Destroy(gameObject);
            }
        }
    }
    public void OnTriggerSkill(HitBox.eHitBoxState _state, HitBox.HitType _hitType, Collider2D _collision)
    {
        switch (_state)
        {
            case HitBox.eHitBoxState.Enter:
                switch (_hitType)
                {
                    case HitBox.HitType.Skill:
                        if (_collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                        {
                            m_CheckEnemy = true;
                        }

                        break;
                }
                break;


        }

    }
}
