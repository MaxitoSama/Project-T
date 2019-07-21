using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bike_2 : MonoBehaviour {

    [Header("Velocities")]

    public float default_speed = 10.0f;
    public float acceleration = 1.0f;
    public float deacceleration = 1.0f;
    public float maxSpeed = 100.0f;
    public float jumpImpulse = 10.0f;

    float current_speed;

    [Header("Rotation Manager")]
    public float rotation = 1.0f;
    public float wheeleSpeed = 50.0f;


    private Vector3 move = Vector3.zero;
    private Vector3 wheeleOffset = Vector3.zero;

    Vector3 resetPos;
    Quaternion resetRot;

    [Header("Objects Manager")]
    public GameObject frontWheel;
    public GameObject backWheel;
    MeshCollider frontWheelCollider;
    MeshCollider backWheelCollider;

    public Transform rotationBackPoint;
    public Transform rotationFrontPoint;
    public Transform centerOfMass;


    [Header("Bools")]
    public bool rareWheelGrounded = false;
    public bool frontWheelGrounded = false;

    bool isBraking = false;
    bool frontBrake = false;
    bool backBrake = false;


    Rigidbody bike;

    private float total_rot = 0.0f;

    private Quaternion myrotation;

 
    // Use this for initialization
    void Start()
    {
        Debug.Log("Bike movement script added to:" + gameObject.name);

        //Setting the rigidbody of the bike
        bike = GetComponent<Rigidbody>();

        //Setting the start position to restart
        resetPos = transform.position;
        resetRot = transform.rotation;

        //Setting the wheels of the bike
        frontWheelCollider = frontWheel.GetComponent<MeshCollider>();
        backWheelCollider = backWheel.GetComponent<MeshCollider>();
    }

    void Update()
    {
        //Cheking if the wheels are colliding.
        rareWheelGrounded = backWheelisGrounded();
        frontWheelGrounded = frontWheelisGrounded();

        //Movement Manager----------------------------------------------------------------------------
        Acceleration();
        Rotation();
        Jumping();

        if (Input.GetAxis("Horizontal")!=0 && frontWheelGrounded)
        {
            transform.Rotate(Vector3.up, Input.GetAxis("Horizontal"));
        }

        if (Input.GetAxis("FrontBrake")!=0)
        {
            isBraking = true;
            frontBrake = true;
        }
        else
        {
            isBraking = false;
            frontBrake = false;
        }

        if (Input.GetAxis("BackBrake") != 0)
        {
            isBraking = true;
            backBrake = true;
        }
        else
        {
            isBraking = false;
            backBrake = false;
        }

        if (Input.GetButton("Fire3"))
        {
            transform.position = resetPos;
            transform.rotation = resetRot;
        }

        //Freezing rotation by Z axis.
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);

    }
    
    //This function manages the acceleration of the bike.
    void Acceleration()
    {
        if (rareWheelGrounded && Input.GetButton("Fire1") && current_speed < maxSpeed && !isBraking)
        {
            Accelerate();
        }
        else
        {
            Deaccelerate();
        }

        Vector3 velocity_direction = Vector3.Cross(transform.right, Vector3.up);

        if (!rareWheelGrounded && !frontWheelGrounded)
            bike.velocity = new Vector3(-velocity_direction.x * current_speed, bike.velocity.y, -velocity_direction.z * current_speed);
        else
            bike.velocity = new Vector3(-velocity_direction.x * current_speed, 0.0f, -velocity_direction.z * current_speed);

    }
    
    //This function manages the rotation of the bike ("Wheele")
    void Rotation()
    {

        if (Input.GetAxis("Vertical") <= -0.5)
        {
            if (backWheelisGrounded())
            {
                transform.RotateAround(rotationBackPoint.position, transform.right, Time.deltaTime * wheeleSpeed);
            }
            else
            {
                transform.RotateAround(transform.position, transform.right, Time.deltaTime * wheeleSpeed);
            }
        }
        else
        {
            if (backWheelisGrounded() && !frontWheelGrounded)
            {
                transform.RotateAround(rotationBackPoint.position, -transform.right, Time.deltaTime * wheeleSpeed);
            }
        }

        if (Input.GetAxis("Vertical") >= 0.5)
        {
            if (frontWheelisGrounded())
            {
                transform.RotateAround(rotationFrontPoint.position, -transform.right, Time.deltaTime * wheeleSpeed);
            }
            else
            {
                transform.RotateAround(transform.position, -transform.right, Time.deltaTime * wheeleSpeed);
            }
        }
        else
        {
            if (!backWheelisGrounded() && frontWheelGrounded)
            {
                transform.RotateAround(rotationFrontPoint.position, transform.right, Time.deltaTime * wheeleSpeed);
            }
        }
    }

    //To accelerate the bike
    void Accelerate()
    {
          current_speed += acceleration;
    }

    //To deaccelerate the bike
    void Deaccelerate()
    {
        if (rareWheelGrounded || frontWheelGrounded)
        {
            if (current_speed > 0)
            {
                if (!isBraking)
                {
                    current_speed -= deacceleration;
                }
                else
                {
                    current_speed -= deacceleration * 2;
                }
            }
            else
            {
                current_speed = 0;
            }
        }
    }

    //Checks if the front wheel is touching something
    bool frontWheelisGrounded()
    {
        bool ret=false;

        ret = frontWheel.GetComponent<CollisionTest>().isGrounded;

        return ret;
    }
    
    //Checks if the back wheel is touching something
    bool backWheelisGrounded()
    {
        bool ret = false;

        ret = backWheel.GetComponent<CollisionTest>().isGrounded;

        return ret;
    }

    //Jump manager
    void Jumping()
    {
        //Normal jump
        if(rareWheelGrounded && frontWheelGrounded)
        {
            if (Input.GetButton("Fire2"))
            {
                bike.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
            }
        }

        //Jumping while doing a wheele
        if(rareWheelGrounded && !frontWheelGrounded)
        {
            if (Input.GetButton("Fire2"))
            {
                if(Input.GetAxis("Vertical")>=0.3)
                {
                    bike.AddForce(transform.forward * jumpImpulse* Input.GetAxis("Vertical"), ForceMode.Impulse);
                }
                else
                {
                    bike.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
                }
            }
        }
    }
}
