using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BikeInfo
{
    public WheelCollider frontWheel;
    public GameObject frontWheelMesh;
    public WheelCollider rareWheel;
    public GameObject rareWheelMesh;
    public bool motor;
    public bool steering;
}

public class bike_2 : MonoBehaviour {

    public float speed = 10.0f;
    public float jumpSpeed = 6.0f;
    public float gravity = 20.0f;
    public float rotation = 1.0f;
    private Vector3 move = Vector3.zero;

    public List<BikeInfo> bikeInfo;
    public float maxMotorTorque;
    public float maxSteeringAngle;



    private float total_rot = 0.0f;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Bike movement script added to:" + gameObject.name);
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        //        Rigidbody controller = GetComponent<Rigidbody>();


        if (controller.isGrounded)
        {
            move = new Vector3(0, 0, -Input.GetAxis("Vertical"));
            move = transform.TransformDirection(move);
            move *= speed;
            if (Input.GetButton("Jump"))
                move.y = jumpSpeed;
        }

        if (Input.GetKey(KeyCode.Q) && total_rot<45)
        {
            total_rot += rotation;
            transform.Rotate(rotation, 0,0);
        }

        if(!Input.GetKey(KeyCode.Q) && total_rot>0)
        {
            transform.Rotate(-rotation, 0,0);
            total_rot -= rotation;
        }

        move.y -= gravity * Time.deltaTime;
        controller.Move(move*Time.deltaTime);

        if(!Input.GetKey(KeyCode.Q) && total_rot<=0)
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotation, 0);
        }
        else if(Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotation, 0);
        }

    }

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach(BikeInfo bikeInfo in bikeInfo)
        {
            if(bikeInfo.steering)
            {
                bikeInfo.frontWheel.steerAngle = steering;
            }
        }
    }

}
