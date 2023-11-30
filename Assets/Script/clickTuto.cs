using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickTuto : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void appear()
    {
        StartCoroutine(delayAppear());
    }

    public void disappear()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }

    IEnumerator delayAppear()
    {
        yield return new WaitForSeconds(1f);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    
}
