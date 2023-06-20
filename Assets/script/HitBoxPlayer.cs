using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxPlayer : HitBoxParent
{
    private Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.OnTriggerPlayer(eHitBoxState.Enter, hitType, collision);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        player.OnTriggerPlayer(eHitBoxState.Stay, hitType, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.OnTriggerPlayer(eHitBoxState.Exit, hitType, collision);
    }
}
