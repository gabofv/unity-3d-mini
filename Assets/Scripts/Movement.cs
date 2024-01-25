using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{

    // PARAMS
    // CACHE
    // STATE

    [SerializeField] float mainThrust = 100f;
    
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip thrustAudio;

    Rigidbody rigidBody;

    AudioSource audioSource;

    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {

        bool spaceIsPressed = Input.GetKey(KeyCode.Space);

        Vector3 thrustVec = Vector3.up * mainThrust * Time.deltaTime;

        if (spaceIsPressed) {
            
            rigidBody.AddRelativeForce(thrustVec);
            
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(thrustAudio);
        
        }
        else {
            audioSource.Stop();
        }
        
    }

    void ProcessRotation() {

        bool dPressed = Input.GetKey(KeyCode.D);
        bool aPressed = Input.GetKey(KeyCode.A);

        if (aPressed && !dPressed) {
            ApplyRotation(rotationThrust);
        }
        else if (dPressed && !aPressed) {
            ApplyRotation(rotationThrust * -1);
        }
    }
    
    void ApplyRotation(float rotationThrustArg) {
        rigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThrustArg * Time.deltaTime);
        rigidBody.freezeRotation = false;
    }
}
