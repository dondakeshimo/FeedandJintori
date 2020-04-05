using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public static GameObject player1;
    public static GameObject player2;
    public Transform[] tile;
    public static float Frame_Size = 100f;
    public const int MAP_WIDTH = 7;
    public const int MAP_HEIGHT = 7;


    void Start () {
        // 床を生成
        MakeField();

        // playerをインスタンス化
        player1 = Instantiate(player1Prefab, new Vector3(1, 0.01f, 1), Quaternion.identity);
        player1.name = "Player1";
        player2 = Instantiate(player2Prefab, new Vector3(5, 0.01f, 5), Quaternion.identity);
        player2.name = "Player2";
    }


    void Update() {
    }


    void MakeField() {
        for (int z = 0; z < MAP_WIDTH; z++) {
            for (int x = 0; x < MAP_HEIGHT; x++) {
                int tileNum = 1;
                // MAPの外周のみtileNumは0
                if (z == 0 || z == MAP_WIDTH || x == 0 || x == MAP_HEIGHT) {
                    tileNum = 0;
                }

                // sandプレハブをインスタンス化
                Instantiate(tile[tileNum], new Vector3(x, 0, z), Quaternion.identity);
            }
        }
    }
}
