using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
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
                loadNextLevel();
                break;
            default:
                Debug.Log("Player hit something else");
                ReloadLevel();
                break;
        }

        void ReloadLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex, LoadSceneMode.Single);
        }

        void loadNextLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if(currentSceneIndex == SceneManager.sceneCountInBuildSettings) 
            {
                currentSceneIndex = 0;
            }
            SceneManager.LoadScene(currentSceneIndex, LoadSceneMode.Single);
        }
    }
}
