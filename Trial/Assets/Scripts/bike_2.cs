using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bike_2 : MonoBehaviour {

    [Header("Velocities")]

    public float default_speed = 10.0f;
    public float acceleration = 1.0f;
    public float deacceleration = 1.0f;
    public float maxSpeed = 100.0f;

    float current_speed;

    [Header("Rotation Manager")]
    public float rotation = 1.0f;
    public float wheeleSpeed = 50.0f;


    private Vector3 move = Vector3.zero;
    private Vector3 wheeleOffset = Vector3.zero;
    private Transform resetPos;

    [Header("Objects Manager")]
    public GameObject frontWheel;
    public GameObject backWheel;
    MeshCollider frontWheelCollider;
    MeshCollider backWheelCollider;

    public Transform rotationBackPoint;
    public Transform rotationFrontPoint;

    [Header("Bools")]
    public bool rareWheelGrounded = false;
    public bool frontWheelGrounded = false;

    bool isBraking = false;

    Rigidbody bike;

    private float total_rot = 0.0f;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Bike movement script added to:" + gameObject.name);
        wheeleOffset = transform.position - rotationBackPoint.position;

        bike = GetComponent<Rigidbody>();
        resetPos = transform;

        frontWheelCollider = frontWheel.GetComponent<MeshCollider>();
        backWheelCollider = backWheel.GetComponent<MeshCollider>();

    }

    void Update()
    {
        Rotation();

        rareWheelGrounded = backWheelisGrounded();
        frontWheelGrounded = frontWheelisGrounded();

        if (rareWheelGrounded)
        {
            if (Input.GetButton("Fire1") && current_speed < maxSpeed && !isBraking)
            {
                Accelerate();
            }
            else
            {
                Deaccelerate();
            }

            Vector3 velocity_direction = Vector3.Cross(transform.right, Vector3.up);
            bike.velocity = -velocity_direction.normalized * current_speed;
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

        if(Input.GetButton("Fire2"))
        {
            transform.SetPositionAndRotation(resetPos.position, resetPos.rotation);
        }


        //Freezing rotation by Z axis.
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }

    void Rotation()
    {
        Vector3 new_Axis = Vector3.Cross(transform.forward, Vector3.up)+new Vector3(0.0f,0.0f,1.0f);

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


}
