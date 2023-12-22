using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovieOff : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject panel;

    void Awake()
    {
        StartCoroutine(screenOff());
        PlayerPrefs.DeleteKey("Stage");
        PlayerPrefs.DeleteKey("Exist");
    }


    public void SkipBtn()
    {
        StopCoroutine(screenOff());
        PlayerPrefs.SetInt("watched", 1);
        panel.SetActive(false);
    }

    IEnumerator screenOff()
    {
        yield return new WaitForSeconds(18.0f);
        panel.SetActive(false);
        PlayerPrefs.SetInt("watched", 1);
    }
    
}
