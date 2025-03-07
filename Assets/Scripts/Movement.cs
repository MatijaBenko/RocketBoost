using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction roll;
    [SerializeField] float thrustForce = 10f;
    [SerializeField] float rollForce = 10f;
    [SerializeField] AudioClip mainEngine;
    
    Rigidbody rb;
    AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            rb.AddRelativeForce(thrustForce * Time.fixedDeltaTime * Vector3.up);
        } else {
            audioSource.Stop();
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
