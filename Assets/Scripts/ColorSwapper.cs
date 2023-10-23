using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwapper : MonoBehaviour
{
    public Material startColor;
    public Material endColor;


    [SerializeField] private float colorChangeSpeed = .2f;

    [SerializeField] public float propagationSpeed = 1f;

    [SerializeField] public float viewDistance = 1f;



    private bool isSwapping = false;
    private bool reversing = false;
    private float timer = 0f;
    private float lerp = 0f;

    public void flashColor(Vector3 source, float range) {
        //delay flashColor based on distance from player
        float distance = Vector3.Distance(source, this.transform.position);
        //if distance is too far, don't flash
        if (distance > range * propagationSpeed) {
            return;
        }
        float delay = distance / (10f * propagationSpeed);
        Invoke("flashColorHelper", delay);
    }

    private void flashColorHelper() {
        isSwapping = true;
    }

    void Update()
    {
        if (!isSwapping && !reversing)  {
            GetComponent<Renderer>().material = startColor;
            timer = 0f;
            return;
        }
        if (lerp > .99f) {
            if (reversing) {
                isSwapping = false;
                reversing = false;
                timer = 0f;
                lerp = 0f;
                swapColors();
                return;
            }
            else if (!reversing) {
                reversing = true;
                timer = 0f;
                lerp = 0f;
                swapColors();
                return;
            }
        }

        timer += Time.deltaTime;
        lerp = Mathf.PingPong(timer, colorChangeSpeed) / colorChangeSpeed;
        GetComponent<Renderer>().material.Lerp(startColor, endColor, lerp);
    }

    void swapColors() { //swap colors
        Material temp = startColor;
        startColor = endColor;
        endColor = temp;
        
    }
}
