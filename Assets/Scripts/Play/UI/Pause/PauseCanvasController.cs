using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseCanvasController : MonoBehaviour {

    public GameObject pauseBG;
    public GameObject countdownText;

    bool finished = false;


    void Start () {
        MakePause();
    }


    void Update () {
        switch (MainController.state) {
            case 0: // スタート待機
                GetStartKey();
                break;
            case 1: // カウント中
                break;
            case 2: // プレイ中
                GetPauseKey();
                break;
            case 3: // ポーズ中
                GetRestartKey();
                break;
            case 4: // finish
                GetFinished();
                break;
            default:
                break;
        }
    }


    void MakePause() {
        Instantiate(pauseBG, transform);
        Instantiate(countdownText, transform);
    }


    // スタートキーを押した時
    void GetStartKey() {
        if (Input.GetButtonUp("Start")) {
            MainController.state = 1;
        }
    }


    // ポーズキーを押した時
    void GetPauseKey() {
        if (Input.GetButtonDown("Start")) {
            MainController.state = 3;
            MakePause();
        }
    }


    // リスタート(ポーズ解除）
    void GetRestartKey() {
        if (Input.GetButtonDown("Start")) {
            MainController.state = 2;
            foreach(Transform child in transform) {
                Destroy(child.gameObject);
            }
        }
     }


    // 終わった時
    void GetFinished() {
        // 終わった時
        if (!finished) {
            finished = true;
            MakePause();
        }
        // 終わっている状態でStartキーを押した時
        else if (finished && Input.GetButtonUp("Start")) {
            SceneManager.LoadScene("Title");
        }
    }
}
