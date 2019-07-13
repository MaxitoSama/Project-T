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
        wheeleOffset = transform.position - rotationBackPoint.position;

        bike = GetComponent<Rigidbody>();

        resetPos = transform.position;
        resetRot = transform.rotation;

        frontWheelCollider = frontWheel.GetComponent<MeshCollider>();
        backWheelCollider = backWheel.GetComponent<MeshCollider>();
    }

    void Update()
    {

        rareWheelGrounded = backWheelisGrounded();
        frontWheelGrounded = frontWheelisGrounded();

       
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

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);

        //Freezing rotation by Z axis.

    }

    void Rotation()
    {

        if (Input.GetAxis("Vertical") <= -0.5)
        {
            if (backWheelisGrounded())
            {
                transform.RotateAround(rotationBackPoint.position, transform.right, Time.deltaTime * wheeleSpeed);
                Debug.Log("Wheeleeeeee");
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
                transform.RotateAround(rotationBackPoint.position, -transform.right, Time.deltaTime * wheeleSpeed/2.0f);
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
                transform.RotateAround(rotationFrontPoint.position, transform.right, Time.deltaTime * wheeleSpeed / 2.0f);
            }
        }
    }

    void Accelerate()
    {
          current_speed += acceleration;
    }

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

    bool frontWheelisGrounded()
    {
        bool ret=false;

        ret = frontWheel.GetComponent<CollisionTest>().isGrounded;

        return ret;
    }

    bool backWheelisGrounded()
    {
        bool ret = false;

        ret = backWheel.GetComponent<CollisionTest>().isGrounded;

        return ret;
    }

    void Jumping()
    {
        if(rareWheelGrounded && frontWheelGrounded)
        {
            if (Input.GetButton("Fire2"))
            {
                bike.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
            }
        }

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
