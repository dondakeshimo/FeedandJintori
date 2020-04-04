using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedHaveController : MonoBehaviour {

    Transform myTransform;
    Text myText;

    public int playerNum;
    public int seedNum;
    int[] seedCounts;
    int seedCount = 0;

    void Start () {
        myTransform = transform;
        myText = myTransform.GetComponent<Text>();

        switch(playerNum) {
            case 1:
                seedCounts = MainController.player1.GetComponent<PlayerController>().seedCounts;
                break;
            case 2:
                seedCounts = MainController.player2.GetComponent<PlayerController>().seedCounts;
                break;
            default:
                break;
        }
        ChangeText();
    }

    void Update () {
        if (seedCount != seedCounts[seedNum]) {
            ChangeText();
        }
    }

    void ChangeText() {
        myText.text = "x" + seedCounts[seedNum];
        seedCount = seedCounts[seedNum];
    }
}
