using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void FixedUpdate() {
        Vector3 wantToGo = target.position + offset;
        Vector3 wantToGoNicer = Vector3.Lerp(transform.position, wantToGo, smoothSpeed);
        transform.position = wantToGoNicer;
    }
}
