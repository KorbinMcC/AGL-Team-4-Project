using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batResetter : MonoBehaviour
{
    public int resetLevel = -1;
    private Vector3 startPos;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            this.transform.position = startPos;
        }
        if (this.transform.position.y < resetLevel) {
            this.transform.position = startPos;
        }
    }
}
