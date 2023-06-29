using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    private basicSkill basicskill;
    public int dagger;


    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        basicskill = FindObjectOfType<basicSkill>();
    }

    void Update()
    {
        DaggerCount();
    }

    public int DaggerCount()
    {
        return basicskill.DaggerCount = dagger;
    }
}
