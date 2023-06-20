using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxEnemy : HitBoxParent
{
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemy.OnTriggerEnemy(eHitBoxState.Enter, hitType, collision);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        enemy.OnTriggerEnemy(eHitBoxState.Stay, hitType, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemy.OnTriggerEnemy(eHitBoxState.Exit, hitType, collision);
    }
}
