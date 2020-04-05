using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour {

    // 育つまでの時間
    public float growTime = 8f;
    public GameObject flowerPrefab;
    MeshRenderer meshrenderer;
    float frameCount = 0;


    private void Start()
    {
        meshrenderer = GetComponent<MeshRenderer>();
    }


    void Update () {
        if (MainController.state == 2) {
            // 秒数更新
            frameCount += Time.deltaTime;

            // growTimeが経ったら花が咲く
            if (frameCount >= growTime)
            {
                // タネを透明に
                meshrenderer.material.color = new Color(0, 0, 0, 1.0f);
                GameObject flower = Instantiate(flowerPrefab, transform.position, Quaternion.identity);
                flower.name = transform.name;
                Destroy(gameObject);
                frameCount = 0;
            }
        }
    }









    /*———————————————————————————— 未使用 ————————————————————————————*/


    /*———————————————ラフレシア関連—————————————————*/

    // 周りのラフレシアを確認
    bool SearchNeighbors() {
        if (GameObject.FindWithTag("Rafflesia")) {
            GameObject[] rafflesias = GameObject.FindGameObjectsWithTag("Rafflesia");
            foreach (GameObject rafflesia in rafflesias) {
                float d_x = Mathf.Abs(rafflesia.transform.position.x - transform.position.x);
                float d_z = Mathf.Abs(rafflesia.transform.position.z - transform.position.z);
                if ((d_x <= 1) && (d_z <= 1))
                {
                    return true;
                }
            }
        }
        return false;
    }

    // ラフレシアがあった時に破壊される
    void DestroyByRafflesia() {
        if (SearchNeighbors()) {
            Destroy(gameObject);
        }
    }
}
