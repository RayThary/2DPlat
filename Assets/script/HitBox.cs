using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private Player player;

    [SerializeField] private HitType hitType;
  
    public enum HitType 
    {
        Ground,
        Wall,
        Enemy
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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.OnTrigger(eHitBoxState.Enter, hitType, collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.OnTrigger(eHitBoxState.Exit, hitType, collision);
    }
}