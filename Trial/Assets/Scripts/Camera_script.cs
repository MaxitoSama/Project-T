using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_script : MonoBehaviour
{
    public GameObject Bike;

    public float speed = 5;

    Vector3 offset;
    Vector3 playerPrevPos, playerMoveDir;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - Bike.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Bike.transform.position + new Vector3(Bike.transform.forward.x*offset.magnitude,offset.y, Bike.transform.forward.z*offset.magnitude);

        transform.LookAt(Bike.transform.position);
    }
}
