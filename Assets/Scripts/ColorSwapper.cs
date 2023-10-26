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

    private bool isSwapping = false;
    private bool reversing = false;
    private float timer = 0f;
    private float lerp = 0f;

    private float distanceRatio = 0f;

    public void flashColor(Vector3 source, float range) {
        //delay flashColor based on distance from player
        float distance = Vector3.Distance(source, this.transform.position);
        //if distance is too far, don't flash
        if (distance > range) {
            return;
        }
        if (!isSwapping) {
            isSwapping = false;
        reversing = false;
        }
        distanceRatio = Mathf.Clamp((1- (distance / range)), 0.001f, .99f);
        float delay = distance / (10f * propagationSpeed);
        Invoke("flashColorHelper", delay);
    }

    private void flashColorHelper() {
        isSwapping = true;
        reversing = false;
    }

    void Update()
    {
        //if not swapping, return to start color, exit
        if (!isSwapping)  {
            return;
        }

        if (!reversing) {
            if (lerp > distanceRatio) {
                reversing = true;
            }
            timer += Time.deltaTime * distanceRatio;
        }
        else if (reversing) {
            if (lerp <= 0.01f) {
                reversing = false;
                isSwapping = false;
                timer = 0f;
                lerp = 0f;
                GetComponent<Renderer>().material = startColor;
                return;
            }
            timer -= Time.deltaTime * distanceRatio;
        }
        lerp = Mathf.PingPong(timer, colorChangeSpeed) / colorChangeSpeed;
        showWithLightValue(lerp);
    }

    void showWithLightValue(float lightValue) {
        GetComponent<Renderer>().material.Lerp(startColor, endColor, lightValue);
    }
}
