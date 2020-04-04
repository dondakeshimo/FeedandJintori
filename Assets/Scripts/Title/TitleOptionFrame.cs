using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleOptionFrame: MonoBehaviour {

    float FRAME_SIZE;
    Transform myTransform;

    // frame
    Vector3 direction;

    // option
    int ChosenOption = 0;   // 選択中のoption番号
    int OptionNum = 3;      // option数


    void Start () {
        FRAME_SIZE = TitleController.FRAME_SIZE;
        myTransform = transform;
        direction = new Vector3(0, 0, 0);
    }


    void Update () {
        if (Input.GetButtonDown("Left1")) {
            OnLRButton(-1);
        } else if (Input.GetButtonDown("Right1")) {
            OnLRButton(1);
        } else if (Input.GetButtonDown("Abutton1")) {
            OnEnterButton();
        }
    }


    void OnLRButton(int toRight) {
        int tempOption = ChosenOption + toRight;    // 判定用の現在のoption番号
        if (tempOption < 0) {                       // 一番左にいて左を押した時
            ChosenOption = 0;                       // optionは0のまま
        } else if (tempOption >= OptionNum) {       // 一番右にいて右を押した時
            ChosenOption = OptionNum - 1;           // optionは最大のまま
        } else {                                    // frameが移動できる時
            ChosenOption = tempOption;              // option番号を更新
            direction.x = toRight * FRAME_SIZE;     // 移動先を計算
            myTransform.Translate(direction);       // frameを移動
        }
    }


    void OnEnterButton(){
        MainController.state = 0;
        SceneManager.LoadScene("_Scenes/Play");
    }
}
