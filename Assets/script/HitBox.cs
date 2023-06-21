using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private HitType hitType;
  
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
