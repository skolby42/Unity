using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    private float mainThrust = 750.0f;
    [SerializeField]
    private float rcsThrust = 250.0f;
    [SerializeField]
    private float levelLoadTime = 1.0f;
    [SerializeField]
    private AudioClip mainEngineClip = null;
    [SerializeField]
    private AudioClip deathClip = null;
    [SerializeField]
    private AudioClip levelSuccessClip = null;

    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private State currentState;

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
    }

    void OnCollisionEnter(Collision collision)
    {
        if (currentState != State.Alive) return;

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
        Invoke("LoadNextLevel", levelLoadTime);
    }

    private void StartDeathSequence()
    {
        currentState = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathClip);
        Invoke("LoadFirstLevel", levelLoadTime);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        sceneIndex++;

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
