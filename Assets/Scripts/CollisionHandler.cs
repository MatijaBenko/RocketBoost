using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 3f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] Transform playerRocket;
    [SerializeField] float explosionForce = 500f;
    [SerializeField] float explosionRadius = 5f;
    AudioSource audioSource;
    bool isControllable = true;
    bool isCollidable = true;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // void Update()
    // {
    //     RespondToDebugKeys();
    // }

    // private void RespondToDebugKeys()
    // {
    //     if (Keyboard.current.lKey.wasPressedThisFrame)
    //     {
    //         LoadNextLevel();
    //     }
    //     else if (Keyboard.current.cKey.wasPressedThisFrame)
    //     {
    //         isCollidable = !isCollidable;
    //     }
    // }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isControllable || !isCollidable) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Player hit a Friendly object");
                break;
            case "Fuel":
                Debug.Log("Player hit a Fuel object");
                break;
            case "Finish":
                Debug.Log("Player reached the finish line");
                StartSuccessSeqeuence();
                break;
            default:
                Debug.Log("Player hit something else");
                StartCrashSequence();
                break;
        }
    }

    private void StartSuccessSeqeuence()
    {
        audioSource.Stop();
        isControllable = false;
        audioSource.PlayOneShot(successSFX);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), levelLoadDelay);
    }

    private void StartCrashSequence()
    {
        audioSource.Stop();
        isControllable = false;
        audioSource.PlayOneShot(crashSFX);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Explode();
        Invoke(nameof(ReloadLevel), levelLoadDelay);
    }

    private void Explode()
    {
        foreach (Transform child in playerRocket)
        {
            //Debug.Log("Exploding: " + child.name);
            child.SetParent(null); // Detach from parent
            Rigidbody rb = child.gameObject.AddComponent<Rigidbody>(); // Add Rigidbody if not already present
            rb.useGravity = false;
            rb.AddExplosionForce(explosionForce, playerRocket.position, explosionRadius); // Apply explosion force
        }
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex, LoadSceneMode.Single);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (currentSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            currentSceneIndex = 0;
        }
        SceneManager.LoadScene(currentSceneIndex, LoadSceneMode.Single);
    }
}
