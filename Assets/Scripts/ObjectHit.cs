using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectHit : MonoBehaviour
{
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip failSound;

    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticles;
    Movement mv;
    AudioSource audiSource;

   

    bool isTransitioning = false;
    bool disableCollision = false;

    private void Start() {
        mv = GetComponent<Movement>();
        audiSource = GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        DebugCheatKeys();

    }


    private void OnCollisionEnter(Collision other) {
        
        if(isTransitioning || disableCollision){return;}
        
        switch(other.gameObject.tag)
        {
            case "Friendly":
                break;

            case "Finish":
                StartSuccessRoutine();
                break;
            default:
                StartFailRoutine();
                break;
        }
        
    }
    private void DebugCheatKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {

           disableCollision = !disableCollision;
        }
    }

    void StartFailRoutine()
    {
        isTransitioning = true;
        crashParticles.Play();
        mv.enabled = false;
        audiSource.Stop();
        audiSource.PlayOneShot(failSound);
        Invoke("ReloadLevel",1f);
    }

    void StartSuccessRoutine()
    {
        isTransitioning = true;
        successParticle.Play();
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
