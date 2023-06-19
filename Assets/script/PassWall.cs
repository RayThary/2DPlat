using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PassWall : MonoBehaviour
{
    //private Player player;

    //private BoxCollider2D box2d;

    [SerializeField]private bool passWall;
    [SerializeField]private float timer=0;
    [SerializeField]private float PassTime=1.0f;

    void Start()
    {
        //box2d=GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (passWall)
        {
            if (gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                gameObject.layer = LayerMask.NameToLayer("PassWall");
            }
            timer += Time.deltaTime;
            if(timer >= PassTime)
            {
                timer= 0;
                passWall = false;
                gameObject.layer = LayerMask.NameToLayer("Ground");
            }
        }
            if (Input.GetKeyDown(KeyCode.DownArrow)) 
            {
                passWall = true;
            }



        //if (GameObject.Find("Player").GetComponent<Player>().DownWalk == true)
        //{
        //    //gameObject.SetActive(false);//�ڽ��ݶ��̴� �ΰ��ٲ��¹������ �ٸ���� �ƴϸ� �̹�����I������ ����
        //    box2d.enabled = false;
        //}
        //else
        //{
        //    box2d.enabled = true;
        //}
    }
}
