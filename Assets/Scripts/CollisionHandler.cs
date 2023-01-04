using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] const float LEVEL_DELAY_LOAD = 1f; 
    [SerializeField] AudioClip success; 
    [SerializeField] AudioClip explosive;
    AudioSource audioSource;
    bool isTransitioning = false;

    void Start()
    {

        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other) {
        if (isTransitioning)
        {
            return;
        }
        switch (other.gameObject.tag)
        {
            case "Enemy":
                StartCrashSequence();
                break;
            case "Friendly":
                break;
            case "Finish":
                AfterLandingSucess();
                break;    
            case "Fuel":
                break;
        }    
    }

    void StartCrashSequence()
    { 
        isTransitioning = true;
        audioSource.PlayOneShot(explosive);
        GetComponent<Moverment>().enabled = false;
        Invoke("ReloadLevel", LEVEL_DELAY_LOAD);
    }

    void AfterLandingSucess()
    {
        isTransitioning = true;
        audioSource.PlayOneShot(success);
        GetComponent<Moverment>().enabled = false;
        Invoke("NextLevel", LEVEL_DELAY_LOAD);
    }

    void NextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextScene == SceneManager.sceneCountInBuildSettings) {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(currentScene);
        SceneManager.LoadScene(currentScene);
    }
}
