using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieOnce : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panel;
    void Awake()
    {
        if(PlayerPrefs.GetInt("watched") == 1 && panel != null)
        {
            panel.SetActive(false);
        }
    }

 
}
