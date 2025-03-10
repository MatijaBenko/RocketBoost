using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(0f, 0f, 0f);
    [SerializeField] float speed = 2f;
    [SerializeField] float delay = 0f;
    float movementFactor = 0f;
    Vector3 startPosition;
    Vector3 endPosition;
    float timer = 0f;

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= delay)
        {
            movementFactor = Mathf.PingPong((timer - delay) * speed, 1f);
            transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);
        }
    }
}
