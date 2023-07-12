using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamage : MonoBehaviour
{
    private Enemy enemy;
    public enum eSkillType
    {
        Axe,
        Dagger,
        Arrow
    }

    public eSkillType skillType;

    private int damage;
    private bool AxeAttack=false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (skillType == eSkillType.Axe)
            {
                if (AxeAttack)
                {
                    enemy.EnemyHpCheck(damage);
                    Debug.Log("üũ");
                }
            }
            else
            {
                enemy.EnemyHpCheck(damage);
            }
            
        }
    }
    private void Awake()
    {
        if (skillType == eSkillType.Axe)
        {
            damage = 5;
        }
        else if(skillType == eSkillType.Dagger)
        {
            damage = 2;
        }
        else if( skillType == eSkillType.Arrow)
        {
            damage = 3;
        }
    }
    private void Update()
    {
        if (skillType == eSkillType.Axe)
        {
            if (AxeAttack == false)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    AxeAttack = true;
                }
            }
        }
        if (AxeAttack)
        {
            Invoke("Axefalse", 1f);
        }
    }
    private void Axefalse()
    {
        AxeAttack = false;
    }
}
