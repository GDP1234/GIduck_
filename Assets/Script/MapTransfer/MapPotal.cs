using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPotal : MonoBehaviour
{
    public string transferMapName;

    [HideInInspector]
    public GameObject map;

    private GameManager gameManager;
    private GameObject player;
    private Player_Action playerAction;

    CameraMove CMove;

    FadeEffect fadeEffect;

    private void Start()
    {
        gameManager = GameManager.instance;
        

        map = GameObject.Find(transferMapName);
        if (map == null)
            Debug.Log("못찾음!!" + transferMapName);

        player = GameObject.Find("Player");
        if (player == null)
            Debug.Log("Player 못찾음 !!");

        CMove = GameObject.Find("Main Camera").GetComponent<CameraMove>();

        //fade 효과
        fadeEffect = GameObject.Find("FadeImage").GetComponent<FadeEffect>();
        fadeEffect.OnFade(FadeState.FadeIn);

        playerAction = player.GetComponent<Player_Action>();
        playerAction.OnStop(PlayerState.MoveOff);


    }

    // 다음 작업은 여기부터

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 게임매니저의 재시작 위치 초기화
            gameManager.playerStartingPt = transferMapName;
            // 플레이어의 위치를 다음 포탈로 이동
            player.transform.position = map.transform.position;

            // 맵이 전환될 때 
            if (MapDictionary.instance.dict.ContainsKey(transferMapName))
            {
                gameManager.currentMapName = MapDictionary.instance.dict[transferMapName];
                CMove.CameraPosMove();

                //페이드 효과
                fadeEffect.OnFade(FadeState.FadeIn);
                playerAction.OnStop(PlayerState.MoveOff);
            }
            else
            {
                Debug.Log(" 등록되지 않음 ");
            }
        }
    }
}
