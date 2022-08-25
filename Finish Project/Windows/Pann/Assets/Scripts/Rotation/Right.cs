using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right : MonoBehaviour
{
    public float speed;
    public static bool temas;
    public static bool temasOther;
    // Start is called before the first frame update
    void Start()
    {
        temasOther = false;
        temas = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0,0,speed);        
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
