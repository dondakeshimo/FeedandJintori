using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {
    
    public Transform sandPrefab;
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public static GameObject player1;
    public static GameObject player2;

    public static float Frame_Size = 100f;  // 枠の横サイズ
    public static int state = 0;            // 0:startしていない/1:カウント中/2:start済み(プレイ中)/3:pause中/4:finish済み

    int[] Map = {
        0, 0, 0, 0, 0, 0, 0,
        0, 1, 1, 1, 1, 1, 0,
        0, 1, 1, 1, 1, 1, 0,
        0, 1, 1, 1, 1, 1, 0,
        0, 1, 1, 1, 1, 1, 0,
        0, 1, 1, 1, 1, 1, 0,
        0, 0, 0, 0, 0, 0, 0
    };

    public Transform[] tile;

	/*———————————————初期化—————————————————*/
	void Start () {

        // 床を生成
        MakeField();

        // playerをインスタンス化
        player1 = Instantiate(player1Prefab, new Vector3(1, 0.01f, 1), Quaternion.identity);
        player1.name = "Player1";
        player2 = Instantiate(player2Prefab, new Vector3(5, 0.01f, 5), Quaternion.identity);
        player2.name = "Player2";
	}


    /*———————————————— フレーム更新 ————————————————*/
    void Update() {
        
    }


    /*———————————————床関連—————————————————*/
    void MakeField() {
        int index = 0;
        for (int z = 0; z < 7; z++)
        {
            for (int x = 0; x < 7; x++)
            {
                int tileNum = Map[index];
                // sandプレハブをインスタンス化
                Instantiate(tile[tileNum], new Vector3(x, 0, z), Quaternion.identity);
                index++;
            }
        }
    }

}
