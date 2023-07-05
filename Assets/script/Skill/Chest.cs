using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Chest : MonoBehaviour
{
    public enum ChestType
    {
        Random,
        Dagger,
        Arrow,
    }

    private BoxCollider2D box2d;
    private GameObject chest;
    private TextMesh chestText;
    private bool checkChest;
    private Animator anim;
    private bool checkSpawn;
    private bool checkDestroy=false;
    private Vector3 chestVec;
    private int checkItem;
    [SerializeField]private Transform spawnTrs;

    [SerializeField] private GameObject ItemArrow;
    [SerializeField] private GameObject ItemDagger;
    
    public ChestType eChestType;

    void Start()
    {
        if (eChestType == ChestType.Random)
        {
            checkItem = Random.Range(0, 2);
        }
        else if (eChestType == ChestType.Arrow)
        {
            checkItem = 0;
        }
        else if(eChestType == ChestType.Dagger)
        {
            checkItem = 1;
        }

        chestVec= transform.position;
        chestVec.y = transform.position.y + 2;
        box2d = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        chest = GameObject.Find("ChestText");
        chestText = chest.GetComponent<TextMesh>();
    }

    
    void Update()
    {
        if (box2d.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            chestText.text = "Press the \"z\" key";
            checkChest = true;
        }
        else
        {
            checkChest = false;
            chestText.text = "";
        }

        if (checkChest)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                anim.SetBool("OnChest", true);
                //checkSpawn = true;
                Invoke("itemSpawn",0.8f);
            }
        }
        if (checkSpawn)
        {
            //Invoke("itemSpawn", 0.8f);
            if (checkItem == 0)
            {
                Instantiate(ItemArrow, chestVec, Quaternion.identity, spawnTrs);
            }
            else if (checkItem == 1)
            {
                Instantiate(ItemDagger, chestVec, Quaternion.Euler(new Vector3(0, 0, 90)), spawnTrs);
            }
            checkSpawn = false;
            checkDestroy = true;
        }
        if (checkDestroy)
        {
            Destroy(gameObject);
        }
    }
    private void itemSpawn()
    {
        checkSpawn = true;
        //if (i == 0)
        //{
        //    Instantiate(ItemArrow, chestVec, Quaternion.identity, spawnTrs);
        //    checkSpawn = false;
        //    checkDestroy = true;
        //}
        //else
        //{
        //    Instantiate(ItemDagger, chestVec, Quaternion.Euler(new Vector3(0,0,90)), spawnTrs);
        //    checkSpawn = false;
        //    checkDestroy = true;
        //}
       
    }
}
