using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    public SwingingEquip swinging;

    private Quaternion desiredRotation;
    private float rotationSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!swinging.RopeExtended()) desiredRotation = transform.parent.rotation;
        else
        {
            desiredRotation = Quaternion.LookRotation(swinging.GetGrabbingPoint() - transform.position);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
    }
}
