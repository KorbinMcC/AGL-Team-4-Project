using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSonar : MonoBehaviour
{
    GameObject[] objectsToBeFlashed;

    private void Awake()
    {
        objectsToBeFlashed = GameObject.FindGameObjectsWithTag("Flashing");
    }

    private void OnGUI()
    {
        Event e = Event.current;//when shift is pressed
        if (e.isKey && e.keyCode == KeyCode.LeftShift)
        {
            for (int i = 0; i < objectsToBeFlashed.Length; i++)
            {
                objectsToBeFlashed[i].GetComponent<ColorSwapper>().flashColor();
            }
        }
    }
}
