using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicSkill : MonoBehaviour
{
    private bool checkAxe;
    private bool axeAttacking;
    [SerializeField]private float attdelay=1.0f;
    private float timer = 0.0f;

    private bool checkDagger;

    [SerializeField]private Animator anim;
    private GameObject Axe;
    private GameObject Dagger;
    [SerializeField]private float speed =2;

    
     
    
    void Start()
    {
        timer = 1.0f;
        checkAxe = true;
        checkDagger = false;

        Axe = transform.Find("Axe").gameObject;
        Dagger = transform.Find("Dagger").gameObject;

        RePlayer player = transform.GetComponentInParent<RePlayer>();
        if (player != null)
        {
            player.SetAction(() => axeOff());
        }
    }


    void Update()
    {
        checkWeapon();
        axeAttack();

    }
    
    private void checkWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Axe.SetActive(true);
            Dagger.SetActive(false);
            checkAxe = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Axe.SetActive(false);
            Dagger.SetActive(true);
        }
    }
    private void axeAttack()
    {
        timer += Time.deltaTime;

        if (checkAxe)
        {
            if (timer <= attdelay)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                timer += Time.deltaTime;
                axeAttacking = true;
            }
            if (axeAttacking)
            {
                anim.SetBool("AxeAttack", true);
                timer = 0.0f;
                axeAttacking = false;
            }
        }

    }
    private void axeOff()
    {
        anim.SetBool("AxeAttack", false);
    }
  
}
