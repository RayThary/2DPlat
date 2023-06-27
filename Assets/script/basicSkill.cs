using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicSkill : MonoBehaviour
{
    private bool checkAxe;
    private bool axeAttacking;
    [SerializeField]private float attAxedelay=1.0f;

    private bool checkDagger;
    private bool daggerAttacking;
    [SerializeField]private float attDaggerdelay=0.5f;

    private float timer = 0.0f;
    [SerializeField]private Animator anim;
    private GameObject Axe;
    private GameObject Dagger;

    [SerializeField] private Transform SkillSpawn;
    
    
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
        daggerAttack();

    }
    
    private void checkWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Axe.SetActive(true);
            Dagger.SetActive(false);
            checkAxe = true;
            checkDagger = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Axe.SetActive(false);
            Dagger.SetActive(true);
            checkAxe=false;
            checkDagger = true;
        }
    }
    private void axeAttack()
    {
        timer += Time.deltaTime;

        if (checkAxe)
        {
            if (timer <= attAxedelay)
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

    private void daggerAttack()
    {
        timer += Time.deltaTime;
        if (checkDagger)
        {
            if (timer <= attDaggerdelay)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                timer += Time.deltaTime;
                daggerAttacking = true;
            }
            if (daggerAttacking)
            {
                Instantiate(Dagger, SkillSpawn);
            }
            
        }
    }
  
}
