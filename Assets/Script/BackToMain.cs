using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMain : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
    }

    public void Back()
    {
        audioSource.clip = FindObjectOfType<AudioManager>().back;
        audioSource.Play();
        SceneManager.LoadScene("Main_Menu");
    }
}
