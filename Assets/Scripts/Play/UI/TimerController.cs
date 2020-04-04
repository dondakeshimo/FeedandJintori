using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {

    Text timerText;         // Textコンポーネント
    int timeCount = 30;     // 秒数
    float frameCount = 0;   // フレームカウント


    // 初期化
    void Start () {
        timerText = transform.GetComponent<Text>();
        timerText.text = timeCount.ToString();
    }

    // フレーム更新
    void Update () {
        if (MainController.state == 2) {  // 開始した後
            frameCount += Time.deltaTime;
            // 1秒たったら、timeCountを進める
            if (frameCount >= 1)
            {
                timeCount -= 1;
                timerText.text = timeCount.ToString();
                frameCount = 0;
                if (timeCount <= 0){
                    MainController.state = 4;   // stateをfinishに
                    print("Finish");
                }
            }
        }
    }
}
