using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPrompt : MonoBehaviour
{
    TMPro.TextMeshProUGUI text;
    public string prompt;

    public float fadingTime = 4f;

    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        text.text = prompt;
    }

    public void resetDisplay() {
        text.color = Color.white;
    }

    //on pressing left shift, dissapear over time
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            resetDisplay();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            fadeOut();
        }
    }

    void fadeOut() {
        float elapsedTime = 0f;
        while (elapsedTime < fadingTime) {
            text.color = Color.Lerp(text.color, Color.clear, (elapsedTime / fadingTime));
            elapsedTime += Time.deltaTime;
        }
        text.color = Color.clear;
    }
}
