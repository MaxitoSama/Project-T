using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK_Handler : MonoBehaviour
{
    Animator anim;

    GameObject IKs;

    public GameObject head;
    public GameObject torso;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject leftFoot;
    public GameObject rightFoot;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1.0f);
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1.0f);

        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1.0f);
        anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1.0f);

        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.transform.position);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHand.transform.position);
        anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFoot.transform.position);
        anim.SetIKPosition(AvatarIKGoal.RightFoot, rightFoot.transform.position);

        anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHand.transform.rotation);
        anim.SetIKRotation(AvatarIKGoal.RightHand, rightHand.transform.rotation);
        anim.SetIKRotation(AvatarIKGoal.LeftFoot, leftFoot.transform.rotation);
        anim.SetIKRotation(AvatarIKGoal.RightFoot, rightFoot.transform.rotation);

        anim.SetLookAtWeight(1.0f);
        anim.SetLookAtPosition(head.transform.position);

        anim.bodyPosition = torso.transform.position;
        anim.bodyRotation = torso.transform.rotation;
    }
}