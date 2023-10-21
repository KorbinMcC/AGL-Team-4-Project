using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwapper : MonoBehaviour
{
    public Material startColor;
    public Material endColor;

    public GameObject player;

    [SerializeField] private float colorChangeSpeed = .2f;

    [SerializeField] public float propagationSpeed = 1f;

    private bool isSwapping = false;
    private bool reversing = false;
    private float timer = 0f;
    private float lerp = 0f;

    //on key press down 'N' swap colors

    private void Awake() {
        //set player to object with tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnGUI() { //set timer to 3 when pressed
        Event e = Event.current;
        if(e.isKey && e.keyCode == KeyCode.N){
            //delay flashColor based on distance from player
            flashColor();
        }
    }

    public void flashColor() {
        //delay flashColor based on distance from player
        float distance = Vector3.Distance(player.transform.position, this.transform.position);
        float delay = distance / (10f * propagationSpeed);
        Invoke("flashColorHelper", delay);
    }

    private void flashColorHelper() {
        isSwapping = true;
    }

    void Update()
    {
        if (!isSwapping && !reversing)  {
            timer = 0f;
            return;
        }
        if (lerp > .99f) {
            if (reversing) {
                print("reversing");
                isSwapping = false;
                reversing = false;
                timer = 0f;
                lerp = 0f;
                swapColors();
                return;
            }
            else if (!reversing) {
                print("1 way");
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