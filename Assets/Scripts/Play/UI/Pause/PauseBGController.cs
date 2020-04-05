using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBGController : MonoBehaviour {

    void Update () {
        if(PlayingStateController.PlayingState == PlayingStateEnum.playing) {
            Destroy(transform.gameObject);
        }
    }
}
