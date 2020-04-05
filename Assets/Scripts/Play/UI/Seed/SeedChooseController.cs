using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedChooseController : MonoBehaviour {

    public int playerNum;
    public int seedNum;

    int chosenOption = 0;
    float frameSize;
    //float speed = 800f;
    Vector3 startPosition;
    Vector3 target;
    GameObject playerObj;
    PlayerController playerController;


    void Start () {
        // 値の取得
        frameSize = MainController.Frame_Size;
        startPosition = transform.position;

        ChangeColor();

        target = transform.position;                      // 移動先を現在地に(初期化)
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


    void Update () {
        // frameが動いていない時
        if (transform.position == target) {
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
        transform.GetComponent<Image>().color = color;
    }


    // 入力に応じてtargetのpositionをセット
    void SetTarget() {
        // targetに現在値を入力
        target = transform.position;
        // 最右位置用
        int num = seedNum - 1;

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
        if (Input.GetButtonDown(key1) && (transform.position.x < max + startPosition.x)) {
            chosenOption += 1;
            return 1f;
        }
        // key2を押した時
        else if (Input.GetButtonDown(key2) && (transform.position.x > min + startPosition.x))
        {
            chosenOption -= 1;
            return -1f;
        }
        return 0f;
    }


    // 枠をtargetに動かす
    void Move() {
        //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.position = target;
    }
}
