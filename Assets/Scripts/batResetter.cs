using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batResetter : MonoBehaviour
{
    private Vector3 startPos;

    void Start() {
        startPos = transform.position;
    }

    public void setStartPos(Vector3 pos) {
        startPos = pos;
    }

    public void resetBat() {
        transform.position = startPos;
    }
}
