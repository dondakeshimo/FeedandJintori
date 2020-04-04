using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseCanvasController : MonoBehaviour {
    Transform myTransform;
    public GameObject pauseBG;
    public GameObject countdownText;

    bool finished = false;

    /*———————————————— 初期化 ————————————————*/
    void Start () {
        myTransform = transform;
        MakePause();
    }
    
    /*———————————————— フレーム更新 ————————————————*/
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



    /*———————————————— Pause画面を出す(インスタンス化) ————————————————*/

    void MakePause() {
        Instantiate(pauseBG, myTransform);
        Instantiate(countdownText, myTransform);
    }



    /*———————————————— stateによる処理 ————————————————*/

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
            foreach(Transform child in myTransform) {
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
