using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBGController : MonoBehaviour {

    Transform myTransform;


	// 初期化
	void Start () {
        myTransform = transform;
	}
	

	// フレーム更新
	void Update () {
        if(MainController.state == 2) {
            Destroy(myTransform.gameObject);
        }
	}
}
