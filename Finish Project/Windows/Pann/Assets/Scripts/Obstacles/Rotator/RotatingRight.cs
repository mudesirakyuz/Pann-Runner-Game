using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingRight : MonoBehaviour
{
    public GameObject target;
    void Start()
    {
        target = GameObject.FindWithTag("Rotator");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(target.transform.position, Vector3.up, Time.deltaTime*120);
    }
}
