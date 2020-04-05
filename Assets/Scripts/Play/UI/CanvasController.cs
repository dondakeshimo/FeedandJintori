using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    // タネのオプション関連
    public GameObject framePrefab;
    public GameObject[] optionPanelPrefab;
    // その他のプレハブ
    public GameObject timerPrefab;
    public GameObject scorePrefab;


    void Start () {
        // 選択肢を並べる
        MakeSeedOption();

        // タイマーを表示する
        MakeTimer();

        // 得点を表示する
        MakeScore(1);
        MakeScore(2);
    }


    void MakeSeedOption() {
        foreach (GameObject prefab in optionPanelPrefab) {
            Instantiate(prefab, transform);
        }
    }


    void MakeTimer() {
        GameObject timer = Instantiate(timerPrefab, Vector3.zero, Quaternion.identity, transform);
        timer.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }


    void MakeScore(int playerNum) {
        GameObject score = Instantiate(scorePrefab, Vector3.zero, Quaternion.identity, transform);
        score.GetComponent<ScoreController>().playerNum = playerNum;
        switch (playerNum) {
            case 1:
                CalcUIPosition(score, 0, 1, new Vector2(0, 1), new Vector3(5, -5, 0));
                break;
            case 2:
                CalcUIPosition(score, 1, 1, new Vector2(1, 1), new Vector3(-5, -5, 0));
                break;
            default:
                break;
        }
    }


    // UIのアンカー、pivot、位置を設定
    void CalcUIPosition(GameObject obj, float lr, float updown, Vector2 pivot, Vector3 position)
    {
        RectTransform rt = obj.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(lr, updown);
        rt.anchorMax = new Vector2(lr, updown);
        rt.pivot = pivot;
        rt.anchoredPosition3D = position;
    }
}
