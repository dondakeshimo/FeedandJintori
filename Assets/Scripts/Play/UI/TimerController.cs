using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {

    Text timerText;
    int timeCount = 30;
    float frameCount = 0;


    void Start () {
        timerText = transform.GetComponent<Text>();
        timerText.text = timeCount.ToString();
    }


    void Update () {
        if (PlayingStateController.PlayingState == PlayingStateEnum.playing) {
            frameCount += Time.deltaTime;
            // 1秒たったら、timeCountを進める
            if (frameCount >= 1)
            {
                timeCount -= 1;
                timerText.text = timeCount.ToString();
                frameCount = 0;
                if (timeCount <= 0){
                    PlayingStateController.PlayingState = PlayingStateEnum.finished;
                    print("Finish");
                }
            }
        }
    }
}
