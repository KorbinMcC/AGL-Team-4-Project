using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prompt : MonoBehaviour
{
    TMPro.TextMeshProUGUI text;
    public string prompt;

    public float fadingTime = 4f;

    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        text.text = prompt;
    }

    //on pressing left shift, dissapear over time
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            StartCoroutine(fadeOut());
        }
    }

    IEnumerator fadeOut() {
        float elapsedTime = 0f;
        while (elapsedTime < fadingTime) {
            text.color = Color.Lerp(text.color, Color.clear, (elapsedTime / fadingTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        text.color = Color.clear;
    }
}
