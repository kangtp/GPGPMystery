using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlay : MonoBehaviour
{

    [SerializeField]
    private AudioSource bgm;

    static public AudioPlay Instance;

    private void Awake()
    {
        Instance = this;
        var SoundManager = FindObjectsOfType<AudioPlay>();

        if (SoundManager.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        bgm.Play();
    }

}
