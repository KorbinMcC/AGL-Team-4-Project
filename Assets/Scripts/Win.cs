using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    GameObject text;
    GameObject startText;
    string promptName = "End Prompt";

    string startPrompt = "StartingPrompt";

    void Start() {
        text = GameObject.Find(promptName);
        startText = GameObject.Find(startPrompt);
    }


    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<batResetter>().resetBat();
            text.GetComponent<MidGamePrompt>().displayPrompt();
            //wait for 5 seconds
            StartCoroutine(waitForSeconds());
        }
    }

    IEnumerator waitForSeconds() {
        yield return new WaitForSeconds(5);
        startText.GetComponent<StartingPrompt>().resetDisplay();
    }
}
