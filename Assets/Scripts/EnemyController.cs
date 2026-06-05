using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    public float speedRotation = 10f;
    public float stoppingDistance = 3f;
    public float speedOriginal;
    public float speed = 3f;

    Animator animator;
    public GameObject player;
    Rigidbody rb;
    Collider coll;
    float speedChanger = 7f;
    private void Start()
    {
        animator = GetComponent<Animator>();
        speedOriginal = speed;
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }
    private void Update()
    {
        transform.LookAt(player.transform.position * speedRotation, Vector2.up);
        Vector2 distance = transform.position - player.transform.position;
        if (distance.sqrMagnitude <= stoppingDistance)
        {
            speed -= speedChanger * Time.deltaTime;
        }
        else
        {
            speed += speedChanger * Time.deltaTime;
        }
        speed = Mathf.Clamp(speed, 0, speedOriginal);
        animator.SetFloat("Velocity", speed);
    }

    public void Kill()
    {
        
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        { 
            rb.isKinematic = false;
        }
        rb.isKinematic = true;
        coll.isTrigger = true;
        animator.enabled = false;
        GameManager.instance.EnemyDead();
        Destroy(GetComponent<EnemyController>());
    }
}
