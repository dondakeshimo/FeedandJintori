using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleOptionFrame: MonoBehaviour {

    float FRAME_SIZE;
    Vector3 direction;
    int ChosenOption = 0;
    int OptionNum = 3;


    void Start () {
        FRAME_SIZE = TitleController.FRAME_SIZE;
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
        int tempOption = ChosenOption + toRight;
        if (tempOption < 0) {
            ChosenOption = 0;
        } else if (tempOption >= OptionNum) {
            ChosenOption = OptionNum - 1;
        } else {
            ChosenOption = tempOption;
            direction.x = toRight * FRAME_SIZE;
            transform.Translate(direction);
        }
    }


    void OnEnterButton(){
        PlayingStateController.PlayingState = PlayingStateEnum.preparing;
        SceneManager.LoadScene("_Scenes/Play");
    }
}
