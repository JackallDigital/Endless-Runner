using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAround : MonoBehaviour
{
    private float spinSpeed = -40;
    void Update() {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}
