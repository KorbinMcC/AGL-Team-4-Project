using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidGamePrompt : MonoBehaviour
{
    TMPro.TextMeshProUGUI text;
    public string prompt;

    public float fadingTime = 4f;

    void Start() //hide on start
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        text.color = Color.clear;
    }

    public void displayPrompt() {
        StartCoroutine(fadeIn());
        StartCoroutine(fadeOut());
    }

    IEnumerator fadeIn() {
        float elapsedTime = 0f;
        while (elapsedTime < fadingTime) {
            text.color = Color.Lerp(text.color, Color.white, (elapsedTime / fadingTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        text.color = Color.white;
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
