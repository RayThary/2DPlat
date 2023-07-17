using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamage : MonoBehaviour
{
    private Enemy enemy;
    private RePlayer player;

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
        enemy = FindObjectOfType<Enemy>();
    }

    private void Update()
    {
        AxeDelay();
    }
    private void AxeDelay()
    {
        if (skillType == eSkillType.Axe)
        {
            if (GameManager.instance.GetPlayer().AxeAttack == true)
            {
                AxeAttack = true;
            }
            else
            {
                AxeAttack = false;
            }
        }
        
    }
    private void Axefalse()
    {
        AxeAttack = false;
    }
}
