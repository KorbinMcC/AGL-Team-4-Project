using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lampActivation : MonoBehaviour
{
    List<GameObject> objectsToBeFlashed;

    public float distance;

    public float frequency;

    private float timer = 5f;

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
        timer += Time.deltaTime;
        print(timer);
        if (timer >= frequency) {
            timer = 0;
            for (int i = 0; i < objectsToBeFlashed.Count; i++) {
                objectsToBeFlashed[i].GetComponent<ColorSwapper>().flashColor(this.transform.position, distance);
            }
        }
    }
}
