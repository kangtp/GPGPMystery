using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip back;
    public AudioClip select;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        audioSource.clip = back;
        audioSource.Play();
    }

    public void Select()
    {
        audioSource.clip = select;
        audioSource.Play();
    }
}
