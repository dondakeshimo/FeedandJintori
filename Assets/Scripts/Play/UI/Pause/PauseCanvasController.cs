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
        switch (PlayingStateController.PlayingState) {
            case PlayingStateEnum.preparing:
                GetStartKey();
                break;
            case PlayingStateEnum.counting:
                break;
            case PlayingStateEnum.playing:
                GetPauseKey();
                break;
            case PlayingStateEnum.pausing:
                GetRestartKey();
                break;
            case PlayingStateEnum.finished:
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
            PlayingStateController.PlayingState = PlayingStateEnum.counting;
        }
    }


    // ポーズキーを押した時
    void GetPauseKey() {
        if (Input.GetButtonDown("Start")) {
            PlayingStateController.PlayingState = PlayingStateEnum.pausing;
            MakePause();
        }
    }


    // リスタート(ポーズ解除）
    void GetRestartKey() {
        if (Input.GetButtonDown("Start")) {
            PlayingStateController.PlayingState = PlayingStateEnum.playing;
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
