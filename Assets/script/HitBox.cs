using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private Player player;
    private SkillAttack skill;

    [SerializeField] private HitType hitType;
  
    public enum HitType 
    {
        Ground,
        Wall,
        Enemy,
        Skill,
    }

    public enum eHitBoxState
    {
        Enter,
        Stay,
        Exit,
    }

    private void Start()
    {
        player = GetComponentInParent<Player>();
        skill= GetComponent<SkillAttack>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.OnTriggerPlayer(eHitBoxState.Enter, hitType, collision);
        //skill.OnTriggerSkill(eHitBoxState.Enter, hitType, collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.OnTriggerPlayer(eHitBoxState.Exit, hitType, collision);
    }
}
