using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	/*———————————————— 初期化 ————————————————*/
	void Start () {
		
	}
	
	/*———————————————— フレーム更新 ————————————————*/
	void Update () {
        if (Input.GetKey(KeyCode.Return)) {
            SceneManager.LoadScene ("Play");
        }   
	}
}
