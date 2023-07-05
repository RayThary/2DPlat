using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private RePlayer player;
    [SerializeField] private LayerMask m_Player;//아직안씀

    public int nowStage;
   

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

    public bool GetPlayerAttack()
    {
        return player.AxeAttack;
    }

    //게임종료 아직안씀
    private void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
    }
}
