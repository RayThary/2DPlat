using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Chest : MonoBehaviour
{
    private BoxCollider2D box2d;
    private GameObject chest;
    private TextMesh chestText;
    private bool checkChest;
    private Animator anim;
    private bool checkSpawn;
    [SerializeField]private Transform spawnTrs;

    [SerializeField] private GameObject ItemArrow;
    [SerializeField] private GameObject ItemDagger;

    void Start()
    {
        
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
        if (checkChest)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                anim.SetBool("OnChest", true);
                Destroy(gameObject, 0.8f);
                Instantiate(ItemArrow, transform.position, Quaternion.identity, spawnTrs);
                //checkSpawn = true;
            }
        }
    }
    private void itemSpawn()
    {
        if (checkSpawn)
        {
            int i = Random.Range(0, 2);
            if (i == 0)
            {
                Instantiate(ItemArrow, transform.position,Quaternion.identity,spawnTrs);
                checkSpawn = false;
            }

            
        }
    }
}
