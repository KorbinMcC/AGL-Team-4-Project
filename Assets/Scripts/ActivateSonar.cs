using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSonar : MonoBehaviour
{
    GameObject[] objectsToBeFlashed;

    BatAudio batAudio;

    public float cooldown = 0f;

    public float maxCooldown = 5f;

    public float viewDistance = 5f;

    private void Awake()
    {
        objectsToBeFlashed = GameObject.FindGameObjectsWithTag("Flashing");
        batAudio = GetComponent<BatAudio>();
        cooldown = 0;
    }


    private void FixedUpdate()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }
    private void OnGUI()
    {
        Event e = Event.current;//when shift is pressed
        if (e.isKey && e.keyCode == KeyCode.LeftShift && cooldown <= 0)
        {
            for (int i = 0; i < objectsToBeFlashed.Length; i++)
            {
                objectsToBeFlashed[i].GetComponent<ColorSwapper>().flashColor(this.transform.position, viewDistance);
            }
            cooldown = maxCooldown;
            batAudio.PlaySqueak();
        }
    }
}
