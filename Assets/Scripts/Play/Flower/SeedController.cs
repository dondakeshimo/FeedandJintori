using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour {

    public float growTime = 8f;     // 育つまでの時間
    public GameObject flowerPrefab; // flowerのプレハブ
    MeshRenderer meshrenderer;      // alpha値用
    Transform myTransform;

    float frameCount = 0;                // フレームカウント

    /*————————————————初期化————————————————*/
    private void Start()
    {
        myTransform = transform;
        meshrenderer = GetComponent<MeshRenderer>();
        //Invoke("DestroyByRafflesia", 0.2f);
        //DestroyByRafflesia();
    }


    /*———————————————フレーム毎の更新—————————————————*/
    void Update () {
        if (MainController.state == 2) {
            // 秒数更新
            frameCount += Time.deltaTime;

            // growTimeが経ったら花が咲く
            if (frameCount >= growTime)
            {
                meshrenderer.material.color = new Color(0, 0, 0, 1.0f);     // タネを透明に
                GameObject flower = Instantiate(flowerPrefab, myTransform.position, Quaternion.identity); // 花をインスタンス化
                flower.name = myTransform.name;
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
