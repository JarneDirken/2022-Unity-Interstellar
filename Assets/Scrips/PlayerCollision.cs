using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    // Variables
    public AudioClip clipExplosion;
    public AudioClip clipSuccess;
    public ParticleSystem successParticles;
    public ParticleSystem failureParticles;

    PlayerMovement playerMovement;
    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisable = false;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Finish":
                StartCompletion();
                break;
            case "Friendly":
                break;   
            default:
                Crach();
                break;
        }
    }

    void StartCompletion()
    {
        if (!isTransitioning && !collisionDisable)
        {
            audioSource.Stop();
            if (!audioSource.isPlaying) audioSource.PlayOneShot(clipSuccess);
            successParticles.Play();
            playerMovement.enabled = false;
            Invoke("NextLevel", 2f);
            isTransitioning = true;
        }
        
    }

    public void NextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextLevel);
    }

    void Crach()
    {
        if (!isTransitioning && !collisionDisable)
        {
            audioSource.Stop();
            if (!audioSource.isPlaying) audioSource.PlayOneShot(clipExplosion);
            failureParticles.Play();
            playerMovement.enabled = false;
            Invoke("ReloadLevel", 2f);
            isTransitioning = true;
        }
        
    }

    void ReloadLevel()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(level);
    }

}
