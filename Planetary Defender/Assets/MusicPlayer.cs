using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] float loadDelay = 2f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Invoke("LoadFirstLevel", loadDelay);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }
}
