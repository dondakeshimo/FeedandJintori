using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RafflesiaController : MonoBehaviour {

    Transform myTransform;                      // transformのキャッシュ用

    // 周りのタネ
    GameObject[] neighbors = new GameObject[8]; // 周り8マスにあるタネ系
    int seedLayer = 1 << 9;                     // タネ系のlayer
    Ray[] rayAround = new Ray[8];               // 周りに飛ばすray8本


	/*——————————————— 咲いた時の処理 —————————————————*/
	void Start () {
        myTransform = transform;
        MakeRayAround();
        SearchNeighbors();
        DestroyNeighbors();
	}
	

	/*——————————————— 咲いてる時の処理 —————————————————*/
	void Update () {
	}



    /*——————————————— 周りのタネ系オブジェクトを探索 —————————————————*/
    // 探索用のrayを作成
    void MakeRayAround() {
        int index = 0;                                  // ray配列のindex
        Vector3 rayPosition = myTransform.position;     // rayの始点
        rayPosition.y += 3f;                            // rayの始点を上に
        for (int i = -1; i <= 1; i++) {
            for (int j = -1; j <= 1; j++) {
                if (!(i == 0 && j == 0)) {
                    rayPosition.x = myTransform.position.x + i;
                    rayPosition.z = myTransform.position.z + j;
                    rayAround[index] = new Ray(rayPosition, -myTransform.up);
                    index++;
                }
            }
        }
    }

    // rayを飛ばして探索
    void SearchNeighbors() {
        for (int i = 0; i < 8; i++) {
            neighbors[i] = null;
            RaycastHit hit;
            if (Physics.Raycast(rayAround[i], out hit, 3f, seedLayer)) {
                neighbors[i] = hit.collider.gameObject;
            }
        }
    }


    /*———————————————— 周りのタネ系オブジェクトを破壊 ————————————————*/
    void DestroyNeighbors() {
        foreach (GameObject obj in neighbors) {
            if (obj != null) {
                if (obj.GetComponent<FlowerController>()) {
                    obj.GetComponent<FlowerController>().DieFlower();
                }
                Destroy(obj);
            }
        }
    }

}
