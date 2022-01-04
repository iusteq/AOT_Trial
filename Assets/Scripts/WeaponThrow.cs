using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class WeaponThrow : MonoBehaviour
{
    public ObjectPools kunai_pool;
    //public GameObject weapon;
    public Transform throwingPoint;
    public float speed = 50f;

    [SerializeField]
    private InputActionReference inputAction;

    bool launched=false;
    bool recuperated;

    public Animator anim;

    //[SerializeField] Camera camera;
    //[SerializeField] float range = 100f;


    List<UnityEngine.XR.InputDevice> devices;
    public XRNode controllerNode;

    private void Awake()
    {
        devices = new List<UnityEngine.XR.InputDevice>();
        inputAction.action.performed += Throw;
    }
    // Start is called before the first frame update
    void Start()
    {
        GetDevice();
        kunai_pool = ObjectPools.Instance;
        anim = GetComponent<Animator>();
    }

   

    // Update is called once per frame
    //void Update()
    //{
    //    GetDevice();
    //    foreach (var device in devices)
    //    {
    //        Debug.Log(device.name + " " + device.characteristics);

    //        if (device.isValid)
    //        {
    //            bool inputValue;

    //            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out inputValue) && inputValue) 
    //            {
    //                //launched=true;
    //                //Throw();
    //            }
    //        }

    //    }
    //}

    //private void FixedUpdate()
    //{
    //    if (launched)
    //    {
    //        Throw();
    //    }
    //}

    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(controllerNode, devices);
    }

    private void Throw(InputAction.CallbackContext context)
    {
        anim.SetTrigger("Throw");
        GameObject weaponInst = kunai_pool.ObjToThrow("kunai", throwingPoint.position, Quaternion.identity);
        //GameObject weaponInst = Instantiate(weapon, throwingPoint.position, weapon.transform.rotation);
        weaponInst.transform.rotation = Quaternion.LookRotation(throwingPoint.position);
        Rigidbody rb = weaponInst.GetComponent<Rigidbody>();

        rb.AddForce(throwingPoint.forward * speed, ForceMode.Impulse);
        launched=false;
    }
}
