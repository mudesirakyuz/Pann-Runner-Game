using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject followObject;
    Vector3 offset;
    void Start()
    {
        offset = transform.position - followObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = followObject.transform.position + offset;
    }
}
