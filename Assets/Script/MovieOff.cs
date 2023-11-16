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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SkipBtn()
    {
        StopCoroutine(screenOff());
        panel.SetActive(false);
    }

    IEnumerator screenOff()
    {
        yield return new WaitForSeconds(18.0f);
        panel.SetActive(false);

    }
    
}
