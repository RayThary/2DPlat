using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicSkill : MonoBehaviour
{
    [SerializeField]private Animator anim;
    private bool checkAxe;
    private bool axeAttacking;
    [SerializeField]private float attAxedelay=1.0f;

    private bool checkDagger;
    private bool daggerAttacking;
    [SerializeField]private float attDaggerdelay=0.5f;
    public int DaggerCount=0;
    public int ArrowCount = 0;

    private float timer = 0.0f;

    private GameObject Axe;
    private GameObject Dagger;
    [SerializeField] private GameObject PreDagger;
    [SerializeField] private Transform TrsSkillSpawn;
    [SerializeField] private GameObject PreArrow;

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
        arrowAttack();
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
            if (Input.GetKeyDown(KeyCode.A)&&DaggerCount!=0)
            {
                DaggerCount--;
                timer += Time.deltaTime;
                Instantiate(PreDagger,SkillSpawn.position,transform.rotation,SkillSpawn);
                timer = 0;
            }
           
            
        }
    }

    private void arrowAttack()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            
            ArrowCount--;
            Instantiate(PreArrow, TrsSkillSpawn.position, transform.rotation, SkillSpawn);
        }
        
    }
}
