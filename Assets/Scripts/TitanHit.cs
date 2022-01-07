using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanHit : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            Destroy(gameObject);
            //or gameObject.SetActive(false);
        }
    }
}
