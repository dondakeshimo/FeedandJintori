using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayingStateEnum {
    preparing,
    counting,
    playing,
    pausing,
    finished
}


public static class PlayingStateController {

    static PlayingStateEnum playingState = PlayingStateEnum.preparing;


    public static PlayingStateEnum PlayingState {
        set { playingState = value; }
        get { return playingState; }
    }
}
