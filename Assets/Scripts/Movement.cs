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
    AudioSource rocketThrust;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rocketThrust = GetComponent<AudioSource>();
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
            if(!rocketThrust.isPlaying)
            {
                rocketThrust.Play();
            }
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);
        } else {
            rocketThrust.Stop();
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
        rb.freezeRotation = true; // Take manual control of rotation
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false; // Resume physics control of rotation
    }
}
