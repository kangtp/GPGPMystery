using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(appearText());
    }


    IEnumerator appearText()
    {
        yield return new WaitForSeconds(2f);
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
