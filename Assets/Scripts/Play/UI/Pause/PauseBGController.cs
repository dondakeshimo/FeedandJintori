using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBGController : MonoBehaviour {

    void Update () {
        if(MainController.state == 2) {
            Destroy(transform.gameObject);
        }
    }
}
