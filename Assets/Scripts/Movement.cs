using UnityEngine;

public class Movement : MonoBehaviour
{

    // PARAMS
    // CACHE
    // STATE

    [SerializeField] float mainThrust = 100f;
    
    [SerializeField] float rotationThrust = 1f;

    [SerializeField] AudioClip thrustAudio;

    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

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

        if (spaceIsPressed)
        {
            ApplyThrust();

        }
        else {
            // Stop thrust
            audioSource.Stop();
            mainThrustParticles.Stop();
        }
        
    }

    private void ApplyThrust()
    {
        Vector3 thrustVec = Vector3.up * mainThrust * Time.deltaTime;

        rigidBody.AddRelativeForce(thrustVec);

        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(thrustAudio);

        if (!mainThrustParticles.isPlaying)
            mainThrustParticles.Play();
    }

    void ProcessRotation() {
        bool dPressed = Input.GetKey(KeyCode.D);
        bool aPressed = Input.GetKey(KeyCode.A);

        if (aPressed && !dPressed)
        {
            RotateLeft();

        }
        else if (dPressed && !aPressed)
        {
            RotateRight();
        }
        else {
            rightThrustParticles.Stop();
            leftThrustParticles.Stop();
        }
    }

    private void RotateRight()
    {
        // Stop opposite thrust particles if active
        if (rightThrustParticles.isPlaying) {
            rightThrustParticles.Stop();
        }

        ApplyRotation(rotationThrust * -1);

        if (!leftThrustParticles.isPlaying) {
            leftThrustParticles.Play();
        }
    }

    private void RotateLeft()
    {
        // Stop opposite thrust particles if active
        if (leftThrustParticles.isPlaying) {
            leftThrustParticles.Stop();
        }

        ApplyRotation(rotationThrust);

        if (!rightThrustParticles.isPlaying)
            rightThrustParticles.Play();
    }

    void ApplyRotation(float rotationThrustArg) {
        rigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThrustArg * Time.deltaTime);
        rigidBody.freezeRotation = false;
    }

}
