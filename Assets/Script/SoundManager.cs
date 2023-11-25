using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource buttonClick;
    // Start is called before the first frame update
    void Start()
    {
        buttonClick = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonClickSound()
    {
        buttonClick.Play();
    }
}
