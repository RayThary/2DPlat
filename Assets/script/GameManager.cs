using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private RePlayer player;
    [SerializeField] private LayerMask m_Player;//¾ÆÁ÷¾È¾¸

    public int nowStage;
    [SerializeField]private Transform[] NextStageStartTrs;

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

        //player = GameObject.Find("Player").GetComponent<RePlayer>();
        player = FindObjectOfType<RePlayer>();
    }
  

    public void SetPlayer(RePlayer _player)
    {
        player = _player;
    }

    public RePlayer GetPlayer()
    {
        return player;
    }
    public Transform GetPlayerTransform() 
    {
        return player.transform;
    }

    public Transform GetStageTransform(int _value)
    {
        return NextStageStartTrs[_value];
    }
}
