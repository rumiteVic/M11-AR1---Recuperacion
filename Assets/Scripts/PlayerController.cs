using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Transform target;

    private InputSystem input;

    Rigidbody rb;
    Vector2 move;
    Vector3 direction;
    private void Start()
    {
        input = new InputSystem();
        input.Player.Enable();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {        
        move = input.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        direction = (transform.forward * move.y + transform.right * move.x).normalized;
        rb.MovePosition(transform.position + direction * Time.fixedDeltaTime * speed);
    }
}
