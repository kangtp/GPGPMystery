using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    AudioSource audioSource;
    public GameObject currentWindow;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        
        audioSource.clip = FindObjectOfType<AudioManager>().back;
        audioSource.Play();

        GameObject.Find("Stages").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Stages").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("Stages").transform.GetChild(2).gameObject.SetActive(true);
        GameObject.Find("Stages").transform.GetChild(3).gameObject.SetActive(true);
        GameObject.Find("Stages").transform.GetChild(4).gameObject.SetActive(true);
        GameObject.Find("Stages").transform.GetChild(5).gameObject.SetActive(true);
        currentWindow.SetActive(false);
    }
}
