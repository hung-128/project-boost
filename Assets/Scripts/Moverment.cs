using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moverment : MonoBehaviour
{   
    [SerializeField] float mainThrust = 1000;
    [SerializeField] float rotateThrust = 1000f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem leftEngineParticle;
    [SerializeField] ParticleSystem rightEngineParticle;

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
           _StartThrust();
        }
        else {
           _StopThrust();
        }
    }

    
    void ProcessRotation()
    {    
        if(Input.GetKey(KeyCode.A)) {
           _Rotation(rotateThrust, rightEngineParticle);
        }
        else {
            _StopRotation(rightEngineParticle);
        }

        if(Input.GetKey(KeyCode.D)){
            _Rotation(-rotateThrust, leftEngineParticle);
        }
        else {
            _StopRotation(leftEngineParticle);
        }
    }

    // xu ly xoay trai xoay phai
    private void _Rotation(float rotateThrust, ParticleSystem engineParticle)
    {
         _ApplyRotation(rotateThrust);
        if(!engineParticle.isPlaying) {
            engineParticle.Play();
        }
    }

    // xu ly ngung xoay trai xoay phai
    private void _StopRotation(ParticleSystem engineParticle)
    {
        engineParticle.Stop();
    }

    // xu ly xoay them toc do xoay theo frame rate cua tung PC
    private void _ApplyRotation(float rotateThrust)
    { 
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotateThrust * Time.deltaTime);
        rb.freezeRotation = false;
    }

    private void _StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if(!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngine);
        }
        if(!mainEngineParticle.isPlaying) {
            mainEngineParticle.Play();
        }
    }

    private void _StopThrust(){
        mainEngineParticle.Stop();
        audioSource.Stop();
    }
}
