using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseTextController : MonoBehaviour {

    int count = 3;
    float frameCount = 0f;
    Text myText;


    void Start () {
        myText = transform.GetComponent<Text>();
        switch (MainController.state) {
            case 0:
                myText.text = "PRESS +/-";
                break;
            case 3:
                myText.text = "PAUSE";
                break;
            case 4:
                PlayerController _player1 = MainController.player1.GetComponent<PlayerController>();
                PlayerController _player2 = MainController.player2.GetComponent<PlayerController>();
                if (_player1.score > _player2.score) {
                    myText.text = "Winner : Player1!";
                } else if (_player1.score < _player2.score) {
                    myText.text = "Winner : Player2!";
                } else {
                    myText.text = "Draw";
                }

                break;
            default:
                break;
        }
    }


    void Update () {
        switch (MainController.state) {
            case 0:
                GetStartKey();
                break;
            case 1:
                CountDown();
                break;
            default:
                break;
        }
    }


    // スタートキーを押した時
    void GetStartKey() {
        if (Input.GetButtonUp("Start")) {
            frameCount = 0;
            myText.text = "3";
        }
    }


    // カウントを進める
    void CountDown() {
        frameCount += Time.deltaTime;

        // 1秒経った、かつ、カウントが１〜３の場合
        if (frameCount >= 1 && count > 0)
        {
            myText.text = count.ToString();
            count -= 1;
            frameCount = 0;
        }
        // カウントが０になった時、ゲームスタート
        else if (frameCount >= 1 && count == 0)
        {
            myText.text = "Start!";
            frameCount = 0;
            Invoke("StartGame", 0.5f);
        }
    }


    // ゲームを開始する
    void StartGame() {
        MainController.state = 2;
        Destroy(transform.gameObject);
    }
}
