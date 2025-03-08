using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] Vector3 rotationDirection = Vector3.up;
    [SerializeField] float rotationSpeed = 1f;

    void Update()
    {
        transform.Rotate(rotationDirection * rotationSpeed * Time.deltaTime);
    }
}
