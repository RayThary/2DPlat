using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxSkill : MonoBehaviour
{
    private basicSkill basicskill;
    private ArrowAttack arrowskill;
 

    void Start()
    {
        basicskill = GetComponentInParent<basicSkill>();
        arrowskill = GetComponentInParent<ArrowAttack>();
    }

   
   
}
