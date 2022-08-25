using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left : MonoBehaviour
{
    public float speed;
    public static bool temas;
    public static bool temasOther;
    void Start()
    {
        temas = false;
        temasOther = false;
    }
    
    void FixedUpdate()
    {
        transform.Rotate(0,0,-speed);        
    }
    
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            temas = true;
        }
        if (collision.gameObject.tag == "OtherPlayer")
        {
            temasOther = true;
        }

    }
}
