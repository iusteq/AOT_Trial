using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ThrowAnim : MonoBehaviour
{
    List<UnityEngine.XR.InputDevice> devices;
    public XRNode controllerNode;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        GetDevice();
        anim = GetComponent<Animator>();
    }

    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(controllerNode, devices);
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

                if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out inputValue) && inputValue)
                {
                    //anim.ResetTrigger("KunaiThrow");
                    anim.Play("WeaponThrow");
                }
            }

        }
    }
}
