using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_animation : MonoBehaviour {

    public float rotation = 1.0f;
    public float total_rot = 1.0f;
    private bool ret_A = false;
    private bool ret_D = false;

    // Use this for initialization
    void Start ()
    {
        Debug.Log("Movement Animation script added to:" + gameObject.name);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!ret_A && !ret_D)
        {
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && total_rot < 45)
            {
                total_rot += rotation;
                transform.Rotate(0, rotation, 0);
            }

            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && total_rot < 45)
            {
                total_rot += rotation;
                transform.Rotate(0, -rotation, 0);
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                ret_D = true;
               
            }

            if (Input.GetKeyUp(KeyCode.A) )
            {
                ret_A = true;
            }
        }
        else
        {
            if(ret_A && total_rot > 0)
            {
                transform.Rotate(0, +rotation, 0);
                total_rot -= rotation;
            }
            else if(ret_D && total_rot > 0)
            {
                transform.Rotate(0, -rotation, 0);
                total_rot -= rotation;
            }
            else
            {
                ret_A = ret_D = false;
            }
        }
        
    }
}
