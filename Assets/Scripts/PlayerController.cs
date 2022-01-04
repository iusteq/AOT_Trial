using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference jumpReference;
    [SerializeField] private float jumpForce =500.0f;
   // [SerializeField] private WeaponsPool weaponsPool;

    private Rigidbody body;

    private bool isGrounded => Physics.Raycast(new Vector2(transform.position.x, transform.position.y + 2.0f),
                                               Vector3.down, 2.0f);

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        jumpReference.action.performed += OnJump;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        if (!isGrounded) return;
        body.AddForce(Vector3.up * jumpForce);
    }

    //private void PlayerAttack()
    //{
    //    //..attack
    //    GameObject go = weaponsPool.GetWeapon();
    //    //position,rotation
    //    go.SetActive(true);
    //}
}
