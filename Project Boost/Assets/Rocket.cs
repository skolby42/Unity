using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float mainThrust = 750.0f;
    [SerializeField] private float rcsThrust = 250.0f;
    [SerializeField] private float levelLoadDelay = 2.0f;

    [SerializeField] private AudioClip mainEngineClip = null;
    [SerializeField] private AudioClip deathClip = null;
    [SerializeField] private AudioClip levelSuccessClip = null;

    [SerializeField] private ParticleSystem mainEngineParticles = null;
    [SerializeField] private ParticleSystem deathParticles = null;
    [SerializeField] private ParticleSystem levelSuccessParticles = null;

    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private State currentState;
    private bool collisionEnabled = true;

    enum State
    {
        Alive,
        Dying,
        Transcending
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        currentState = State.Alive;
    }

    void Update()
    {
        RespondToThrustInput();
        RespondToRotationInput();
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (!Debug.isDebugBuild) return;

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionEnabled = !collisionEnabled;
            print($"Collision enabled: {collisionEnabled}");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (currentState != State.Alive) return;
        if (!collisionEnabled) return;

        switch (collision.gameObject.tag)
        {
            case "Friendly": // Do nothing
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        currentState = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(levelSuccessClip);
        levelSuccessParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        currentState = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathClip);

        if (mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Stop();
        }
        
        deathParticles.Play();
        Invoke("LoadFirstLevel", levelLoadDelay);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        sceneIndex++;

        if (sceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            sceneIndex = 0;  // Restart game
        }

        SceneManager.LoadScene(sceneIndex);
    }

    private void RespondToThrustInput()
    {
        if (currentState != State.Alive) return;

        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            if (mainEngineParticles.isPlaying)
            {
                mainEngineParticles.Stop();
            }
        }
    }

    private void ApplyThrust()
    {
        float thrustThisFrame = mainThrust * Time.deltaTime;
        rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineClip);
        }

        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void RespondToRotationInput()
    {
        if (currentState != State.Alive) return;

        rigidBody.freezeRotation = true;  // Take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false;  // Resume physics control
    }
}
