using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour {

    public int takeSeed;
    public int flowerNumber;
    public int score;


    void Start () {
        // 得点の追加
        plusScore(score);
    }


    public void TakeFlower(int[] seedNums) {
        seedNums[flowerNumber] += takeSeed;
        plusScore(-score);
    }


    public void DieFlower() {
        plusScore(-score);
    }


    // 花の得点をplayerに追加
    void plusScore(int _score) {
        switch (this.name)
        {
            case "1":
                MainController.player1.GetComponent<PlayerController>().score += _score;
                break;
            case "2":
                MainController.player2.GetComponent<PlayerController>().score += _score;
                break;
            default:
                break;
        }
    }

}
