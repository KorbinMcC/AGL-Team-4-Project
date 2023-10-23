using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lampActivation : MonoBehaviour
{
    List<GameObject> objectsToBeFlashed;

    public float distance = 2f;

    public float frequency = 1f;

    private float timer;

    void Start()
    {
        //put all objects with the "Flashing" tag into an array within distance
        objectsToBeFlashed = new List<GameObject>(GameObject.FindGameObjectsWithTag("Flashing"));
        for (int i = 0; i < objectsToBeFlashed.Count; i++) {
            if (Vector3.Distance(objectsToBeFlashed[i].transform.position, this.transform.position) > distance) {
                objectsToBeFlashed.RemoveAt(i);
                i--;
            }
        }
    }

    void Update() {
        timer += Time.time;
        if (timer >= 1 / frequency) {
            timer = 0;
            for (int i = 0; i < objectsToBeFlashed.Count; i++) {
                objectsToBeFlashed[i].GetComponent<ColorSwapper>().flashColor(this.transform.position, distance);
            }
        }
    }
}
