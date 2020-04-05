using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedOptionPanel : MonoBehaviour {

    public int playerNum;
    public Sprite[] sprites;
    public GameObject optionPrefab;
    public GameObject framePrefab;
    float frameSize;


    void Start () {
        frameSize = MainController.Frame_Size;

        ArrayOption();
        MakeFrame();
    }


    // optionを並べる
    void ArrayOption() {
        for (int i = 0; i < sprites.Length; i++) {
            MakeOption(i);
        }
    }


    void MakeOption(int i) {
        Transform instance = Instantiate(optionPrefab, transform).transform;
        instance.Translate(new Vector3(i * frameSize, 0, 0));
        instance.GetChild(0).GetComponent<Image>().sprite = sprites[i];
        SeedHaveController seedHave = instance.GetChild(2).GetComponent<SeedHaveController>();
        seedHave.playerNum = playerNum;
        seedHave.seedNum = i;
    }


    void MakeFrame() {
        GameObject instance = Instantiate(framePrefab, transform);
        SeedChooseController scc = instance.GetComponent<SeedChooseController>();
        scc.playerNum = playerNum;
        scc.seedNum = sprites.Length;
    }
}
