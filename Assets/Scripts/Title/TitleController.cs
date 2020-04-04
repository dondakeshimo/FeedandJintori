using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour {

    public GameObject optionPrefab;
    public GameObject framePrefab;

    // transform
    Transform myTransform;
    // option数
    int optionNum = 3;
    // optionのサイズ
    public static float FRAME_SIZE = 120;


    void Start () {
        myTransform = transform;
        ArrayOptions();
        InstantiateOption(framePrefab, 0, false);
    }


    void Update () {
    }


    void ArrayOptions() {
        for (int i = 0; i <= optionNum-1; i++) {
            InstantiateOption(optionPrefab, i, true);
        }
    }


    void InstantiateOption(GameObject prefab, int dx, bool isOption) {
        GameObject instance = Instantiate(prefab, myTransform);
        instance.transform.Translate(new Vector3(dx * FRAME_SIZE, 0, 0));
        if (isOption) {
            instance.name = "Stage" + dx;
        }
    }
}
