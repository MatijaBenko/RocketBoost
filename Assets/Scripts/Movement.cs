using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction roll;
    [SerializeField] float thrustForce = 10f;
    [SerializeField] float rollForce = 10f;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        // Allow player to move when the game starts
        thrust.Enable();
        roll.Enable();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRoll();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);
        }
    }

    private void ProcessRoll()
    {
        float rotationInput = roll.ReadValue<float>();
        if (rotationInput < 0)
        {
            ApplyRotation(rollForce);
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rollForce);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
    }
}
