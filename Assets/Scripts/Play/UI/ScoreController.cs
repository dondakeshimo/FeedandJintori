using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public int playerNum;
    int score = 0;
    Text scoreText;
    PlayerController player;


    void Start () {
        // Textをキャスト
        scoreText = transform.GetComponent<Text>();
        // PlayerControllerをキャスト
        switch (playerNum) {
            case 1:
                player = MainController.player1.GetComponent<PlayerController>();
                break;
            case 2:
                player = MainController.player2.GetComponent<PlayerController>();
                break;
            default:
                break;
        }
        // 色を変更
        ChangeColor();
    }


    void Update () {
        if (score != player.score) {
            score = player.score;
            scoreText.text = score.ToString();
        }
    }


    void ChangeColor() {
        switch (playerNum) {
            case 1:
                scoreText.color = new Color(0.8f, 0.3f, 0.3f, 1);
                break;
            case 2:
                scoreText.color = new Color(0.5f, 0.5f, 1f, 1);
                break;
            default:
                break;
        }
    }
}
