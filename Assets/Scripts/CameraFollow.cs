using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform Target;
    public float Smooth = 2f;
    public bool HorizontalCenter = true;
    public bool VerticalCenter = false;

    private Vector3 offset;


    private void Start() {
        if (!Target)
            Target = GameObject.FindGameObjectWithTag("Player").transform;

        offset = transform.position - Target.position;
        if (HorizontalCenter)
            offset.x = 0;
        if (VerticalCenter)
            offset.y = 0;
    }

    private void FixedUpdate() {
        transform.position = Vector3.Lerp(transform.position
            , Target.position + offset
            , Time.fixedDeltaTime * Smooth);
    }

}
