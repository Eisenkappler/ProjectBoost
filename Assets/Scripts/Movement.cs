using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrust = 1000;
    [SerializeField] float rotation = 100;
    [SerializeField] AudioClip mainEngine;


    Rigidbody rb;
    AudioSource AudSource;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        AudSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if(!AudSource.isPlaying)
            {
                AudSource.PlayOneShot(mainEngine);
            }
           
        }
        else
        {
            AudSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
           ApplyRotation(rotation);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotation);
        }
    }

   
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;  
    }


}
