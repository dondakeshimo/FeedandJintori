using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleOption : MonoBehaviour {

    Transform myTransform;


    void Start () {
        myTransform = transform;
        myTransform.GetComponent<Text>().text = myTransform.parent.name;
    }
}
