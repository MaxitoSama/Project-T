using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_animation : MonoBehaviour {

    public float rotation = 1.0f;
    public float total_rot = 1.0f;
    private bool ret_A = false;
    private bool ret_D = false;

    Vector3 wheel_rotation = Vector3.zero;

    // Use this for initialization
    void Start ()
    {
        Debug.Log("Movement Animation script added to:" + gameObject.name);
        wheel_rotation = transform.localEulerAngles;

    }
	
	// Update is called once per frame
	void Update ()
    {
        rotation = (45 * Input.GetAxis("Horizontal"));
        wheel_rotation.y =rotation-90;
        transform.localEulerAngles= wheel_rotation;
    }
}
