using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{


    private InputSystem input;
    public GameObject grenade;


    private void Start()
    {
        input = new InputSystem();
        input.Player.Enable();
    }

    void Update()
    {
        if (input.Player.Grenade.WasPressedThisFrame())
        {
            Instantiate(grenade, transform.position, transform.rotation);
        }
    }
}
