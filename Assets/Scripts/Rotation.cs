using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float speed = 2f;
    public float maxRotation = 45f;

    void Update()
    {
        transform.rotation = Quaternion.Euler(-60f, maxRotation * Mathf.Sin(Time.time * speed), 0f);
    }
}
