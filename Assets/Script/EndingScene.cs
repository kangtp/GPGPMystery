using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingScene : MonoBehaviour
{
    public TextMeshProUGUI text, text2;

    void Start()
    {
        StartCoroutine(Credit());
    }

    public IEnumerator Credit()
    {
        float fadeCount = 0.0f;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.005f;
            yield return new WaitForSeconds(0.01f);
            text.color = new Color(255, 255, 255, fadeCount);
            text2.color = new Color(255, 255, 255, fadeCount);
        }
        yield return new WaitForSeconds(2f);
        while (fadeCount > 0)
        {
            fadeCount -= 0.005f;
            yield return new WaitForSeconds(0.01f);
            text.color = new Color(255, 255, 255, fadeCount);
            text2.color = new Color(255, 255, 255, fadeCount);
        }
        SceneManager.LoadScene("Main_Menu");
    }
}
