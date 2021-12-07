using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WeaponThrow : MonoBehaviour
{
    public ObjectPools kunai_pool;
    //public GameObject weapon;
    public Transform throwingPoint;
    public float speed = 50f;

    bool launched=false;
    bool recuperated;

    //[SerializeField] Camera camera;
    //[SerializeField] float range = 100f;


    List<InputDevice> devices;
    public XRNode controllerNode;

    private void Awake()
    {
        devices = new List<InputDevice>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GetDevice();
        kunai_pool = ObjectPools.Instance;
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

                if (device.TryGetFeatureValue(CommonUsages.triggerButton, out inputValue) && inputValue) 
                {
                    launched=true;
                    //Throw();
                }
            }

        }
    }

    private void FixedUpdate()
    {
        if (launched)
        {
            Throw();
        }
    }

    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(controllerNode, devices);
    }

    private void Throw()
    {
        GameObject weaponInst = kunai_pool.ObjToThrow("kunai", throwingPoint.position, Quaternion.identity);
        //GameObject weaponInst = Instantiate(weapon, throwingPoint.position, weapon.transform.rotation);
        weaponInst.transform.rotation = Quaternion.LookRotation(throwingPoint.position);
        Rigidbody rb = weaponInst.GetComponent<Rigidbody>();

        rb.AddForce(throwingPoint.forward * speed, ForceMode.Impulse);
        launched=false;
    }
}
