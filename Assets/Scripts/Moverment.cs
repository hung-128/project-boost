using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moverment : MonoBehaviour
{   
    [SerializeField] float mainThrust = 1000;
    [SerializeField] float rotateThrust = 1000f;
    [SerializeField] AudioClip mainEngine;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space)){
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if(!audioSource.isPlaying) {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else {
            audioSource.Stop();
        }
    }


    void ProcessRotation()
    {    
        if(Input.GetKey(KeyCode.A)) {
            ApplyRotation(rotateThrust);
        }
        if(Input.GetKey(KeyCode.D)){
            ApplyRotation(-rotateThrust);
        }
    }

    void ApplyRotation(float rotateThrust){ 
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotateThrust * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
