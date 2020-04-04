using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedOptionPanel : MonoBehaviour {

    public int playerNum;
    public Sprite[] sprites;
    public GameObject optionPrefab;
    public GameObject framePrefab;

    Transform myTransform;
    float frameSize;        // 枠の横サイズ

    /*———————————————— 初期化 ————————————————*/
    void Start () {
        myTransform = transform;
        frameSize = MainController.Frame_Size;

        ArrayOption();
        MakeFrame();
    }


    /*———————————————— option作成 ————————————————*/

    // optionを並べる
    void ArrayOption() {
        for (int i = 0; i < sprites.Length; i++) {
            MakeOption(i);
        }
    }


    // optionをインスタンス化
    void MakeOption(int i) {
        Transform instance = Instantiate(optionPrefab, myTransform).transform;
        instance.Translate(new Vector3(i * frameSize, 0, 0));
        instance.GetChild(0).GetComponent<Image>().sprite = sprites[i];
        SeedHaveController seedHave = instance.GetChild(2).GetComponent<SeedHaveController>();
        seedHave.playerNum = playerNum;
        seedHave.seedNum = i;
    }


    /*———————————————— frame作成 ————————————————*/

    void MakeFrame() {
        GameObject instance = Instantiate(framePrefab, myTransform);
        SeedChooseController scc = instance.GetComponent<SeedChooseController>();
        scc.playerNum = playerNum;
        scc.seedNum = sprites.Length;
    }
}
