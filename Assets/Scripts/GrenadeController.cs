using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrenadeController : MonoBehaviour
{
    Rigidbody rb;
    public LayerMask mask;
    public float launchForce = 3f;
    public float timer;
    public float radius = 5f;
    public float explosionForce = 10f;
    float minRot = -5f;
    float maxRot = 10f;
    public GameObject particles;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * launchForce, ForceMode.Force);
        Destroy(gameObject, 3f);
        
    }

    void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            radius,
            mask
        );

        foreach (Collider hit in hits)
        {
            Rigidbody rb = hit.attachedRigidbody;

            if (rb != null)
            {
                float fuerzaRotacion = Random.Range(minRot, maxRot);
                rb.AddExplosionForce(
                    explosionForce,
                    transform.position,
                    radius
                );
                rb.AddTorque(transform.up * fuerzaRotacion);

                EnemyController enemy = rb.transform.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.Kill();
                }
                enemy = null;
            }
        }
    }
    private void OnDestroy()
    {
        Explode();
        Instantiate(particles);
    }
}
