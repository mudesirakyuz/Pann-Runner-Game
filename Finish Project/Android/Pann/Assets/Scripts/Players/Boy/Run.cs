using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Run : MonoBehaviour
{
    Animator anim;
    Animator animWall;
    Rigidbody rb;

    public AudioSource runSound, deadSound, winSound;
    public GameObject wall;
    public Image followPoint;

    public float power;
    public float speed;
    public float speedCopy;
    public float jumpSpeed;

    private bool rotator;
    private bool grounded;
    public static bool oneTime;

    private float collisionTime;
    private float z;

    public static bool isFinished;
    public static bool isStartForOtherPlayers;
    public static bool buttonPress;
    public static bool isJump;

    public static float moveX;


    void Start()
    {
        speedCopy = speed;
        rotator = false;
        grounded = true;
        oneTime = true;
        buttonPress = false;
        isJump = false;
        isFinished = false;
        isStartForOtherPlayers = false;

        rb = this.gameObject.GetComponent<Rigidbody>();
        anim = this.gameObject.GetComponent<Animator>();
        animWall = wall.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (anim.GetBool("IsCollision") == false)
        {
            if(grounded)
                jump();

            if(buttonPress == true && isFinished == false)
            {
                followPoint.enabled = false;
                anim.SetBool("IsStart",true);
                if(oneTime)
                {
                    oneTime = false;
                    runSound.Play();
                }
            }

            if(anim.GetBool("IsStart"))
            {
                isStartForOtherPlayers = true;
                if(isFinished)
                {
                    if(transform.position.x > -23f)
                    {
                        if(transform.position.z < 0)
                        {
                            z = transform.position.z - Time.deltaTime * -0.5f;
                            if(z > 0) //Eğer hesaplanan değer büyükse sıfırdan z'yi sıfırla
                                z = 0;
                        } 
                        else if(transform.position.z > 0)
                        {
                            z = transform.position.z + Time.deltaTime * -0.5f;
                            if(z < 0) //Eğer hesaplanan değer küçükse sıfırdan z'yi sıfırla
                                z = 0;
                        }
                        else
                        {
                            z = transform.position.z;
                        }
                        transform.position = new Vector3(transform.position.x + Time.deltaTime * -0.5f, transform.position.y, z);  
                        speed = 0;
                    }
                    else
                    {
                        anim.SetBool("IsFinished",true);
                        anim.SetBool("IsStart",false);
                    }
                }
                else
                {
                    Vector3 vec = new Vector3(moveX*Time.deltaTime,0,0);
                    Rotating(vec);
                }
                
                
            }
        }
        else //Eğer çarptıysa
        {
            if(collisionTime >= 1.1335)
            {
                followPoint.enabled = true;
                anim.SetBool("IsCollision",false);
                anim.SetBool("IsStart",false);
                anim.SetBool("IsGameOver",true);
                transform.position = new Vector3(0.37f, 0, 0);
                speed = speedCopy;
                collisionTime = 0;
                oneTime = true;
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                collisionTime += Time.deltaTime+0.01f;
            }
        }
       
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "ObstacleHorizantal")
        {
            speed = 0;
            anim.SetBool("IsCollision",true);
            runSound.Stop();
            deadSound.Play();
        }
        
        if (collision.collider.tag == "RotatingStick")
        {
            collisionTime = Time.deltaTime+1;
            rotator = true;
        }

        if(collision.collider.tag == "Ground")
        {
            grounded = true;
        }

        if(collision.collider.tag == "Finish")
        {
            oneTime = false;
            runSound.Stop();
            winSound.Play();
            isFinished = true;
            animWall.SetBool("Wall_IsFinish",true);

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if(Right.temas)
            {
                Right.temas = false; 
            }
            else
            {
                Left.temas = false;
            }
        }
    }

    private void jump()
    {
        if(anim.GetBool("IsJump"))
        {
            anim.SetBool("IsJump",false);
        }

        if(isJump == true && grounded == true)
        {
            grounded = false;
            anim.SetBool("IsJump",true);
            rb.velocity = Vector3.up * Time.deltaTime * jumpSpeed;
        }
    }

    private void Rotating(Vector3 vec)
    {
        if(!Right.temas && !Left.temas)
        {
            transform.Translate(vec.x * power, 0, vec.z + speed);
        }
        else if(Right.temas)
        {
            transform.Translate(vec.x + 0.02f, 0, vec.z + speed);
        }
        else if(Left.temas)
        {
            transform.Translate(vec.x - 0.02f, 0, vec.z + speed);
        }
        if(rotator)
        {
            transform.Translate(vec.x * power, 0, vec.z - 0.05f);
            if(collisionTime <= 0)
            {
                rotator = false;
            }
            collisionTime -= Time.deltaTime;
        }
    }
}
