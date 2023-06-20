using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxParent : MonoBehaviour
{
    [SerializeField] protected HitType hitType;
  
    public enum HitType 
    {
        Ground,
        Wall,
        Enemy,
        Skill,
        PassWall,
    }

    public enum eHitBoxState
    {
        Enter,
        Stay,
        Exit,
    }
}
