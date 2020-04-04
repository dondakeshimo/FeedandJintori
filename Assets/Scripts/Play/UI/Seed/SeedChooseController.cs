using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedChooseController : MonoBehaviour {

    public int playerNum;   // playerの番号
    public int seedNum;   // スプライト数

    int chosenOption = 0;   // 選ばれたoption番号
    float frameSize;        // 枠の横サイズ
    //float speed = 800f;     // 枠の移動速度
    Transform myTransform;  // transformのキャッシュ用
    Vector3 startPosition;  // 最初のposition
    Vector3 target;         // 移動先のposition
    GameObject playerObj;   // playerのobject
    PlayerController playerController;


    /*——————————————初期化——————————————————*/
    void Start () {
        // 値の取得
        frameSize = MainController.Frame_Size;              // 枠の横サイズを取得
        myTransform = transform;
        startPosition = myTransform.position;

        ChangeColor();

        target = myTransform.position;                      // 移動先を現在地に(初期化)
        switch (playerNum) {                                // playerObjにPlayerを代入
            case 1:
                playerObj = MainController.player1;
                break;
            case 2:
                playerObj = MainController.player2;
                break;
            default:
                break;
        }
        playerController = playerObj.GetComponent<PlayerController>();
    }


    /*———————————————— 更新 ————————————————*/
    // フレーム毎の更新
    void Update () {
        // frameが動いていない時
        if (myTransform.position == target) {
            playerController.chosenOption = chosenOption;
            // 入力があればtargetをセット
            SetTarget();
        }
    }

    // 秒数毎の更新
    void FixedUpdate()
    {
        // frameを動かす
        Move();
    }


    void ChangeColor() {
        Color color;
        switch (playerNum) {
            case 1:
                color = new Color(0.7f, 0, 0, 1f);
                break;
            case 2:
                color = new Color(0, 0, 0.7f, 1f);
                break;
            default:
                color = Color.white;
                break;
        }
        myTransform.GetComponent<Image>().color = color;
    }



    /*——————————————— frameの移動 —————————————————*/

    // 入力に応じてtargetのpositionをセット
    void SetTarget() {
        target = myTransform.position;    // targetに現在値を入力
        int num = seedNum - 1;          // 最右位置用

        // 入力値の取得
        float h = 0f;
        switch (playerNum) {
            case 1:
                h = GetFrameButton("Right1", "Left1", frameSize * num, 0f);
                break;
            case 2:
                h = GetFrameButton("Right2", "Left2", frameSize * num, 0f);
                break;
            default:
                break;
        }

        // 移動先のx座標を計算
        target.x += h * frameSize;
    }


    // キーを入力した時の処理
    float GetFrameButton(string key1, string key2, float max, float min) {
        // key1を押した時
        if (Input.GetButtonDown(key1) && (myTransform.position.x < max + startPosition.x)) {
            chosenOption += 1;
            return 1f;
        }
        // key2を押した時
        else if (Input.GetButtonDown(key2) && (myTransform.position.x > min + startPosition.x))
        {
            chosenOption -= 1;
            return -1f;
        }
        return 0f;
    }


    // 枠をtargetに動かす
    void Move() {
        //myTransform.position = Vector3.MoveTowards(myTransform.position, target, speed * Time.deltaTime);
        myTransform.position = target;
    }
}
