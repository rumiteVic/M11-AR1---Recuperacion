using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class RaycastGun : MonoBehaviour
{
    private InputSystem input;
    public LineRenderer line;
    public float lineFadeSpeed;
    public LayerMask mask;
    public float knockbackForce = -100f;
    GameObject enemy = null;
    EnemyController enemyController = null;
    Rigidbody cubo = null;
    private void Start()
    {
        input = new InputSystem();
        input.Player.Enable();
    }
    void Update()
    {
        line.startColor = new Color(line.startColor.r, line.startColor.g, line.startColor.b, line.startColor.a - Time.deltaTime * lineFadeSpeed);
        line.endColor = new Color(line.endColor.r, line.endColor.g, line.endColor.b, line.endColor.a - Time.deltaTime * lineFadeSpeed);

        if (input.Player.Attack.WasPressedThisFrame())
        {
            line.startColor = new Color(line.startColor.r, line.startColor.g, line.startColor.b, 1);
            line.endColor = new Color(line.endColor.r, line.endColor.g, line.endColor.b, 1);

            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position + transform.forward * 1000);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward * 1000, out hit, 100f, mask))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    enemy = hit.rigidbody.gameObject;
                    enemyController = enemy.GetComponent<EnemyController>();
                    if (enemyController != null)
                    {
                        enemyController.Kill();
                        enemyController = null;
                        enemy = null;
                    }

                }
                if (hit.rigidbody != null && hit.collider != null)
                {
                    cubo = hit.rigidbody;
                    Vector3 moveDirection = cubo.position - transform.position;
                    cubo.AddForce(moveDirection.normalized * knockbackForce, ForceMode.Force);
                    line.SetPosition(1, transform.position + hit.point);
                }
            }
        }
    }
}
