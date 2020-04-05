using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RafflesiaController : MonoBehaviour {

    // 周りのタネ
    // 周り8マスにあるタネ系
    GameObject[] neighbors = new GameObject[8];
    // タネ系のlayer
    int seedLayer = 1 << 9;
    // 周りに飛ばすray8本
    Ray[] rayAround = new Ray[8];


    void Start () {
        MakeRayAround();
        SearchNeighbors();
        DestroyNeighbors();
    }


    // 周りのタネ系オブジェクトを探索
    // 探索用のrayを作成
    void MakeRayAround() {
        int index = 0;
        Vector3 rayPosition = transform.position;
        rayPosition.y += 3f;
        for (int i = -1; i <= 1; i++) {
            for (int j = -1; j <= 1; j++) {
                if (!(i == 0 && j == 0)) {
                    rayPosition.x = transform.position.x + i;
                    rayPosition.z = transform.position.z + j;
                    rayAround[index] = new Ray(rayPosition, -transform.up);
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


    // 周りのタネ系オブジェクトを破壊
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
