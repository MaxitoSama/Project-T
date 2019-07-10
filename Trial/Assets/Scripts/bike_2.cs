using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bike_2 : MonoBehaviour {

    public float default_speed = 10.0f;
    public float rotation = 1.0f;
    public float acceleration = 1.0f;
    public float deacceleration = 1.0f;

    float current_speed;

    public float maxFrontBrakeTorke = 200.0f;
    public float maxBackBrakeTorke = 200.0f;

    private Vector3 move = Vector3.zero;
    
    public float maxSpeed=100.0f;
    public float maxSteeringAngle;

    public Transform frontWheelTransform;
    public Transform backWheelTransform;
    public Transform handlebarTrnsform;

    public WheelCollider fronfWheelCollider;
    public WheelCollider backWheelCollider;


    bool isBraking = false;

    Rigidbody bike;

    private float total_rot = 0.0f;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Bike movement script added to:" + gameObject.name);

        bike = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Rotation();

        if(fronfWheelCollider.isGrounded || backWheelCollider.isGrounded)
        {
            if (Input.GetButton("Fire1") && current_speed< maxSpeed && !isBraking)
            {
                Accelerate();
            }
            else
            {
                Deaccelerate();
            }

            bike.velocity = -transform.forward * current_speed;
        }

        if (Input.GetAxis("Horizontal")!=0)
        {
            transform.Rotate(Vector3.up, Input.GetAxis("Horizontal"));
        }
       

        if (Input.GetAxis("FrontBrake")!=0)
        {
            isBraking = true;
        }
        else
        {
            isBraking = false;

        }

        if (Input.GetAxis("BackBrake") != 0)
        {
            isBraking = true;
        }
        else
        {
            isBraking = false;
        }


        //Freezing rotation by Z axis.
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);

        Debug.Log(bike.velocity.magnitude);
    }

    void Rotation()
    {
        if (Input.GetAxis("Vertical") < 0)
        {
            bike.AddForce(transform.forward*100);
        }
    }

    void Accelerate()
    {
        current_speed += acceleration;
    }
    void Deaccelerate()
    {
        if(!isBraking)
        {
            if (current_speed >= default_speed)
            {
                current_speed -= deacceleration;
            }
        }        
        else
        {
            if (current_speed > 0)
            {
                current_speed -= deacceleration;
            }
        }
    }
   
}
