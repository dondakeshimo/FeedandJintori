using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    // タネのオプション関連
    public GameObject framePrefab;             // 選択枠プレハブ
    public GameObject[] optionPanelPrefab;     // optionのプレハブ

    // その他のプレハブ
    public GameObject timerPrefab;  // タイマー表示text
    public GameObject scorePrefab;  // 得点表示text

    Transform myTransform;  // transform


    /*———————————————— 初期化 ————————————————*/
	void Start () {
        myTransform = transform;

        // 選択肢を並べる
        MakeSeedOption();

        // タイマーを表示する
        MakeTimer();

        // 得点を表示する
        MakeScore(1);
        MakeScore(2);
	}
	






    /*———————————————— タネoption生成 ————————————————*/

    void MakeSeedOption() {
        foreach (GameObject prefab in optionPanelPrefab) {
            Instantiate(prefab, myTransform);
        }
    }




    /*———————————————— タイマーの表示 ————————————————*/

    void MakeTimer() {
        GameObject timer = Instantiate(timerPrefab, Vector3.zero, Quaternion.identity, myTransform);
        timer.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }




    /*———————————————— 得点の表示 ————————————————*/

    void MakeScore(int playerNum) {
        GameObject score = Instantiate(scorePrefab, Vector3.zero, Quaternion.identity, myTransform);
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



    /*——————————————— UI関連 —————————————————*/

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
