using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bike_2 : MonoBehaviour {

    public float default_speed = 10.0f;
    public float jumpSpeed = 6.0f;
    public float gravity = 20.0f;
    public float rotation = 1.0f;
    private Vector3 move = Vector3.zero;
    
    public float maxMotorTorque;
    public float maxSteeringAngle;

    public WheelCollider frontWheelCollider;
    public WheelCollider backWheelCollider;
    public Transform frontWheelTransform;
    public Transform backWheelTransform;
    public Transform handlebarTrnsform;

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
       ColliderDirection();
       UpdateWheelPoses();


        if (Input.GetButton("Fire1"))
        {
            backWheelCollider.motorTorque = -100;
        }

        
    }

    void Rotation()
    {
        if (Input.GetAxis("Balance") > 0 && total_rot < 45)
        {
            total_rot += rotation;
            transform.Rotate(rotation, 0, 0);
        }
        else
        {
            if (total_rot > 0)
            {
                transform.Rotate(-rotation, 0, 0);
                total_rot -= rotation;
            }
        }

        if (Input.GetAxis("Balance") <= 0 && total_rot <= 0)
        {
            transform.Rotate(transform.up, Input.GetAxis("Horizontal") * rotation);
        }
    }

    void ColliderDirection()
    {
        frontWheelCollider.steerAngle = Input.GetAxis("Horizontal") * maxSteeringAngle;
    }

    private void UpdateWheelPoses()
    {
        Vector3 _fronPos = frontWheelTransform.position;
        Vector3 _backPos = backWheelTransform.position;

        Quaternion _fronRot = frontWheelTransform.rotation;
        Quaternion _backRot = backWheelTransform.rotation;

        frontWheelCollider.GetWorldPose(out _fronPos, out _fronRot);
        backWheelCollider.GetWorldPose(out _backPos, out _backRot);

    }
}
