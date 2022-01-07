using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanMovement : MonoBehaviour
{
    public float speed = 12f;
    [SerializeField] GameObject target;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //CharacterController enemy = GetComponent<CharacterController>();
        //Vector3 inputValue = new Vector3(speed, 0, 0);
        //enemy.Move(inputValue* Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
    }
}
