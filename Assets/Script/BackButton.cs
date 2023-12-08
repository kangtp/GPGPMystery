using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    private GameObject stage;
    AudioSource audioSource;
    public GameObject currentWindow;
    // Start is called before the first frame update
    void Start()
    {
        stage = GameObject.Find("Stages");
        audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
    }
    public void Back()
    {
        
        audioSource.clip = FindObjectOfType<AudioManager>().back;
        audioSource.Play();

        for(int i = 0; i < stage.transform.childCount; i++)
        {
            stage.transform.GetChild(i).gameObject.SetActive(true);
        }
        currentWindow.SetActive(false);
    }
}
