using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class SwingingEquip : MonoBehaviour
{
    private LineRenderer rope;
    private Vector3 grabbingPoint;
    public LayerMask treesToGrab;
    public Transform ropeTip,camera,player;
    private float ropeDistance = 60f;
    private SpringJoint joint;
    public Rigidbody rb;

    List<InputDevice> devices;
    public XRNode controllerNode;

    bool isSwinging = false;
    public Text test;
    private Vector3 currentGrabbedPosition;

    private void Awake()
    {
        rope = GetComponent<LineRenderer>();
        devices = new List<InputDevice>();
        //joint = GetComponent<SpringJoint>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GetDevice();
    }

    // Update is called once per frame
    void Update()
    {
        GetDevice();
        foreach (var device in devices)
        {
            Debug.Log(device.name + " " + device.characteristics);

            if (device.isValid)
            {
                bool inputValue;

                if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out inputValue) && inputValue) //butonul y/b
                {
                    rb.isKinematic = true;
                    isSwinging = true;
                    StartSwinging();
                }
                else if(device.TryGetFeatureValue(CommonUsages.primaryButton, out inputValue) && inputValue && isSwinging==true)
                {
                    rb.isKinematic = false;
                    rb.useGravity = true;
                    isSwinging = false;
                    StopSwinging();
                }

            }

        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(controllerNode, devices);
    }

    void StartSwinging()
    {
        //test.text = "I'm in startSwinging";
        RaycastHit hit;
        if(Physics.Raycast(camera.position,camera.forward,out hit, ropeDistance,treesToGrab))
        {
            StartSwinging(hit.point);
        }
    }

    //[ContextMenu("Start")]
    //void TestStarTSwinging()
    //{
    //    var pos = new Vector3();
    //    pos.x = Random.Range(2f, 5f);
    //    pos.y = Random.Range(2f, 5f);
    //    pos.z = Random.Range(2f, 5f);
    //    StartSwinging(pos);
    //}

    //[ContextMenu("Stop")]
    //void TestStopSwinging()
    //{
    //    StopSwinging();
    //}

    void StartSwinging(Vector3 pos)
    {
        grabbingPoint = pos;
        joint = player.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = grabbingPoint;

        float distanceFromPoint = Vector3.Distance(player.position, grabbingPoint);

        joint.maxDistance = distanceFromPoint * 0.3f;
        joint.minDistance = distanceFromPoint * 0.04f;

        joint.spring = 6f;
        joint.damper = 5f;
        joint.massScale = 4.5f;
        joint.enableCollision = true;

        rope.positionCount = 2;
        currentGrabbedPosition = ropeTip.position;
    }

    void StopSwinging()
    {
        rope.positionCount = 0;
        //joint.connectedBody = null;
        Destroy(joint);
    }

    void DrawRope()
    {
        if (!joint) return;

        currentGrabbedPosition = Vector3.Lerp(currentGrabbedPosition, grabbingPoint, Time.deltaTime * 8f);

        rope.SetPosition(0, ropeTip.position);
        rope.SetPosition(1, currentGrabbedPosition);
    }

    public bool RopeExtended()
    {
        return joint != null;
    }

    public Vector3 GetGrabbingPoint()
    {
        return grabbingPoint;
    }
}
