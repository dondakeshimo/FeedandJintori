using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleOption : MonoBehaviour {

    void Start () {
        transform.GetComponent<Text>().text = transform.parent.name;
    }
}
