using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectHit : MonoBehaviour
{
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip failSound;
    Movement mv;
    AudioSource audiSource;

    bool isTransitioning = false;

    private void Start() {
        mv = GetComponent<Movement>();
        audiSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) {
        
        if(isTransitioning){return;}
        
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;

            case "Finish":
                Debug.Log("Finish");
                StartSuccessRoutine();
                break;
            default:
                Debug.Log("DEAD");
                StartFailRoutine();
                break;
        }
        
    }


    void StartFailRoutine()
    {
        isTransitioning = true;
        mv.enabled = false;
        audiSource.Stop();
        audiSource.PlayOneShot(failSound);
        Invoke("ReloadLevel",1f);
    }

    void StartSuccessRoutine()
    {
        isTransitioning = true;
        mv.enabled = false;
        audiSource.Stop();
        audiSource.PlayOneShot(successSound);
        Invoke("LoadNextLevel",1f);
    }


    void ReloadLevel()
    {
        isTransitioning = false;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Debug.Log("RELOAD");
    }


    void LoadNextLevel()
    {
        isTransitioning = false;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentSceneIndex + 1;
        Debug.Log("NEXT LEVEL");
        if(nextIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextIndex = 0;
        } 

        SceneManager.LoadScene(nextIndex);
    }




}
