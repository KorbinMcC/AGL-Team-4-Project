using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    GameObject text;
    string promptName = "OutOfBoundsPrompt";

    void Start() {
        text = GameObject.Find(promptName);
    }


    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<batResetter>().resetBat();
            text.GetComponent<MidGamePrompt>().displayPrompt();
        }
    }
}
