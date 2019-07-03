using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BikeInfo
{
    public GameObject frontWheelMesh;
    public GameObject rareWheelMesh;

    public bool motor;
    public bool steering;
}

public class bike_2 : MonoBehaviour {

    public float default_speed = 10.0f;
    public float jumpSpeed = 6.0f;
    public float gravity = 20.0f;
    public float rotation = 1.0f;
    private Vector3 move = Vector3.zero;

    public List<BikeInfo> bikeInfo;
    public float maxMotorTorque;
    public float maxSteeringAngle;

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
}
