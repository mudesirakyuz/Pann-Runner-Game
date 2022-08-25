using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    Animator anim;
    NavMeshAgent nMesh;
    private Transform finishLine;
    public float speed;
    
    private float collisionTime;
    private bool control;
    private bool rotator;
    private float[] pos = new float[3];

    
    void Start()
    {
        control = false;
        rotator = false;

        anim = GetComponent<Animator>();
        nMesh = GetComponent<NavMeshAgent>();//DENE

        pos[0] = transform.position.x;
        pos[1] = transform.position.y;
        pos[2] = transform.position.z;

        finishLine = GameObject.FindGameObjectWithTag("FinishLineForOtherPlayers").GetComponent<Transform>();

        nMesh.SetDestination(finishLine.position);
        nMesh.speed = 0;
    }
    
        
    void Update()
    {
        if(Run.isFinished)
        {
            speed = 0;
            nMesh.speed = speed;
        }
        if(Run.isStartForOtherPlayers == true && control == false)
        {
            control = true;
            nMesh.enabled = true;
            nMesh.SetDestination(finishLine.position);
            nMesh.speed = speed;
            anim.SetBool("IsStart",true);
            
        }
        if(anim.GetBool("IsCollision"))
        {
            if(collisionTime >= 2.1335)//Düşme animasyonu bitene kadar bekle
            {
                anim.SetBool("IsCollision",false);
                //anim.SetBool("IsStart",false);
                nMesh.enabled = false;
                transform.position = new Vector3(pos[0], pos[1], pos[2]);
                collisionTime = 0;
                control = false;
                
            }
            else
            {
                collisionTime += Time.deltaTime+0.01f;
            }
            
            
        }
        Rotating();
        Stick();

    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "ObstacleHorizantal")
        {
            nMesh.speed = 0;
            anim.SetBool("IsCollision",true);
        }
        if (collision.collider.tag == "Finish")
        {
            anim.SetBool("IsStart",false);
            anim.SetBool("IsFinish",true);
        }
        if (collision.collider.tag == "RotatingStick")
        {
            collisionTime = Time.deltaTime + 1;
            rotator = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (Right.temasOther)
            {
                Right.temasOther = false;
            }
            else
            {
                Left.temasOther = false;
            }
        }
    }

    private void Rotating()
    {
        Vector3 vec1 = new Vector3(transform.position.x + 0.3f * Time.deltaTime, transform.position.y, transform.position.z);
        Vector3 vec2 = new Vector3(transform.position.x - 0.3f * Time.deltaTime, transform.position.y, transform.position.z);

        if (Right.temasOther)
        {
            nMesh.gameObject.transform.position = vec1;
            
        }
        else if (Left.temasOther)
        {
            nMesh.gameObject.transform.position = vec2;
        }
    }

    private void Stick()
    {
        if (rotator)
        {
            Vector3 v = new Vector3(transform.position.x, 0, transform.position.z - 0.2f);
            nMesh.gameObject.transform.position = v;
            if (collisionTime <= 0)
            {
                rotator = false;
            }
            collisionTime -= Time.deltaTime;
        }
    }
}
