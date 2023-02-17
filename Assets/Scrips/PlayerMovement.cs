using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables
    public float amountOfBoost = 1000f;
    public float sideWayForce = 100f;

    public AudioClip clipBoost;
    public ParticleSystem horizontalParticles;
    public ParticleSystem leftParticles;
    public ParticleSystem rightParticles;

    Rigidbody rb;
    AudioSource audioSource;
    PlayerCollision playerCollision;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        playerCollision = GetComponent<PlayerCollision>();
    }

    void Update()
    {
        PlayerBoost();
        PlayerInput();
    }

    // Extra methods
    void PlayerBoost()
    {
        // Boost
        if (Input.GetKey(KeyCode.Space))
        {
            StartBoost();
        }
        else
        {
            StopBoost();
        }
    }

    void StartBoost()
    {
        rb.AddRelativeForce(Vector3.up * amountOfBoost * Time.deltaTime);
        if (!audioSource.isPlaying) audioSource.PlayOneShot(clipBoost);
        if (!horizontalParticles.isPlaying) horizontalParticles.Play();
    }

    void StopBoost()
    {
        horizontalParticles.Stop();
        audioSource.Stop();
    }

    
    void PlayerInput()
    {
        // Left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
        {
            if (!leftParticles.isPlaying) leftParticles.Play();
            RotatePlayer(sideWayForce);
        }
        else
        {
            leftParticles.Stop();
        }
        // Right 
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (!rightParticles.isPlaying) rightParticles.Play();
            RotatePlayer(-sideWayForce);
        }
        else
        {
            rightParticles.Stop();
        }
    }

    void RotatePlayer(float rotateWay)
    {
        rb.freezeRotation = true; // freezing rotation so we manually rotate
        transform.Rotate(Vector3.forward * rotateWay * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing
    }
}
