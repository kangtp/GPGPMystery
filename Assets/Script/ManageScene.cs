using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageScene : MonoBehaviour
{
    AudioSource audioSource;
    public Image stage1;
    public Image stage2;
    public Image stage3;
    public Image stage4;
    public Image stage5;
    public Image stage6;

    public GameObject road1;

    private bool stage1open = true;
    public bool stage2open = false;
    private bool stage3open = false;
    private bool stage4open = false;
    private bool stage5open = false;
    private bool stage6open = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        StartCoroutine(createIcon());
        
        if (PlayerPrefs.HasKey("Stage") && PlayerPrefs.GetInt("Stage") >= 2)
        {
            stage2open = true;
        }
        if (PlayerPrefs.HasKey("Stage") && PlayerPrefs.GetInt("Stage") >= 3)
        {
            stage3open = true;
        }
        if (PlayerPrefs.HasKey("Stage") && PlayerPrefs.GetInt("Stage") >= 4)
        {
            stage4open = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckOpenStage()
    {
        if (PlayerPrefs.HasKey("Stage") && PlayerPrefs.GetInt("Stage") >= 2)
        {
            stage2open = true;
        }
        if (PlayerPrefs.HasKey("Stage") && PlayerPrefs.GetInt("Stage") >= 3)
        {
            stage3open = true;
        }
        if (PlayerPrefs.HasKey("Stage") && PlayerPrefs.GetInt("Stage") >= 4)
        {
            stage4open = true;
        }
    }

    public void Go2()
    {
        StartCoroutine(OpenStage2());
    }

    public void SelectSound()
    {
        audioSource.clip = FindObjectOfType<AudioManager>().select;
        audioSource.Play();
    }

    public void Stage1()
    {
        hideButton();
        GameObject.Find("Intro").transform.GetChild(0).gameObject.SetActive(true);
    }

    public void GoStage1_1()
    {
        Debug.Log("1_1");
        StartCoroutine(EnterStage1_1());
    }
    public void GoStage1_2()
    {
        Debug.Log("1_2");
        StartCoroutine(EnterStage1_2());
    }
    public void GoStage1_3()
    {
        Debug.Log("1_3");
        StartCoroutine(EnterStage1_3());
    }
    public void GoStage1_4()
    {
        Debug.Log("1_4");
        StartCoroutine(EnterStage1_4());
    }

    public void Stage2()
    {
        if(PlayerPrefs.HasKey("Stage"))
        {
            hideButton();
            GameObject.Find("Intro").transform.GetChild(1).gameObject.SetActive(true);
            return;
        }
        Debug.Log("Not open");
    }

    public void GoStage2_1()
    {
        StartCoroutine(EnterStage2_1());
    }
    public void GoStage2_2()
    {
        StartCoroutine(EnterStage2_2());
    }
    public void GoStage2_3()
    {
        StartCoroutine(EnterStage2_3());
    }
    public void GoStage2_4()
    {
        StartCoroutine(EnterStage2_4());
    }

    public void Stage3()
    {
        if (stage3open)
        {
            hideButton();
            GameObject.Find("Intro").transform.GetChild(2).gameObject.SetActive(true);
        }
        Debug.Log("Not open");
    }

    public void GoStage3_1()
    {
        StartCoroutine(EnterStage3_1());
    }
    public void GoStage3_2()
    {
        StartCoroutine(EnterStage3_2());
    }
    public void GoStage3_3()
    {
        StartCoroutine(EnterStage3_3());
    }
    public void GoStage3_4()
    {
        StartCoroutine(EnterStage3_4());
    }
    public void Stage4()
    {
        if (stage4open)
        {
            hideButton();
            GameObject.Find("Intro").transform.GetChild(3).gameObject.SetActive(true);
        }
        Debug.Log("Not open");
    }

    public void GoStage4_1()
    {
        StartCoroutine(EnterStage4_1());
    }
    public void GoStage4_2()
    {
        StartCoroutine(EnterStage4_2());
    }
    public void GoStage4_3()
    {
        StartCoroutine(EnterStage4_3());
    }
    public void GoStage4_4()
    {
        StartCoroutine(EnterStage4_4());
    }

    public void Stage5()
    {
        if (stage5open)
        {
            hideButton();
            GameObject.Find("Intro").transform.GetChild(4).gameObject.SetActive(true);
        }
        Debug.Log("Not open");
    }

    public void GoStage5_1()
    {
        StartCoroutine(EnterStage5_1());
    }

    public void Stage6()
    {
        if (stage6open)
        {
            hideButton();
            GameObject.Find("Intro").transform.GetChild(5).gameObject.SetActive(true);
        }
        Debug.Log("Not open");
    }

    public void GoStage6_1()
    {
        StartCoroutine(EnterStage6_1());
    }

    IEnumerator EnterStage1_1()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage1_talk");
    }
    IEnumerator EnterStage1_2()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage1-2");
    }
    IEnumerator EnterStage1_3()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage1-3");
    }
    IEnumerator EnterStage1_4()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage1-4");
    }

    IEnumerator EnterStage2_1()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage2-1");
    }
    IEnumerator EnterStage2_2()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage2-2");
    }
    IEnumerator EnterStage2_3()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage2-3");
    }
    IEnumerator EnterStage2_4()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage2-4");
    }
    IEnumerator EnterStage3_1()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage3-1");
    }
    IEnumerator EnterStage3_2()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage3-2");
    }
    IEnumerator EnterStage3_3()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage3-3");
    }
    IEnumerator EnterStage3_4()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage3-4");
    }
    IEnumerator EnterStage4_1()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage4-1");
    }
    IEnumerator EnterStage4_2()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage4-2");
    }
    IEnumerator EnterStage4_3()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage4-3");
    }
    IEnumerator EnterStage4_4()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage4-4");
    }
    IEnumerator EnterStage5_1()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage5-1");
    }

    IEnumerator EnterStage6_1()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage6-1");
    }
    IEnumerator createIcon()
    {
        float fadeCount = 0;
        while(fadeCount < 1.0f)
        {
            fadeCount += 0.005f;
            yield return new WaitForSeconds(0.01f);
            stage1.color = new Color(0, 0, 0, fadeCount);
            stage2.color = new Color(0, 0, 0, fadeCount * 0.2f);
            stage3.color = new Color(0, 0, 0, fadeCount * 0.2f);
            stage4.color = new Color(0, 0, 0, fadeCount * 0.2f);
            stage5.color = new Color(0, 0, 0, fadeCount * 0.2f);
            stage6.color = new Color(0, 0, 0, fadeCount * 0.2f);
        }
        StopCoroutine(createIcon());
    }

    public IEnumerator OpenStage2()
    {
        //열리는 효과음 넣기
        float fadeCount = 0.2f;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.005f;
            yield return new WaitForSeconds(0.01f);
            stage2.color = new Color(0, 0, 0, fadeCount);
        }
        StopCoroutine(OpenStage2());
    }

    public void hideButton()
    {
        GameObject.Find("Stage1").SetActive(false);
        GameObject.Find("Stage2").SetActive(false);
        GameObject.Find("Stage3").SetActive(false);
        GameObject.Find("Stage4").SetActive(false);
        GameObject.Find("Stage5").SetActive(false);
        GameObject.Find("Stage6").SetActive(false);
    }

    
}
