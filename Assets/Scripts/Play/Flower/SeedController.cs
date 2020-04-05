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
        if (PlayingStateController.PlayingState == PlayingStateEnum.playing) {
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
}
