using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageScene : MonoBehaviour
{
    AudioSource audioSource;

    private GameObject stage;
    public Button[] levels;
    public Image stage1;
    public Image stage2;
    public Image stage3;
    public Image stage4;
    public Image stage5;

    public GameObject road1;
    public GameObject road2;
    public GameObject road3;
    public GameObject road4;

    private int currentLevel = 0;

    void Start()
    {
        stage = GameObject.Find("Stages");
        audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        StartCoroutine(createIcon());

        currentLevel = PlayerPrefs.GetInt("currentLevel");
        print(currentLevel);
        for (int i = currentLevel + 1; i < levels.Length; i++)
        {
            levels[i].interactable = false;
        }

        if (PlayerPrefs.HasKey("Stage") && PlayerPrefs.GetInt("Stage") >= 2)
        {
            if(PlayerPrefs.HasKey("Exist") && PlayerPrefs.GetInt("Exist") >= 2)
            {
                StartCoroutine(ExistStage(stage2, road1));
            }
            else
            {
                Go(2);
            }
        }
        if (PlayerPrefs.HasKey("Stage") && PlayerPrefs.GetInt("Stage") >= 3)
        {
            if (PlayerPrefs.HasKey("Exist") && PlayerPrefs.GetInt("Exist") >= 3)
            {
                StartCoroutine(ExistStage(stage3, road2));
            }
            else
            {
                Go(3);
            }
        }
        if (PlayerPrefs.HasKey("Stage") && PlayerPrefs.GetInt("Stage") >= 4)
        {
            if (PlayerPrefs.HasKey("Exist") && PlayerPrefs.GetInt("Exist") >= 4)
            {
                StartCoroutine(ExistStage(stage4, road3));
            }
            else
            {
                Go(4);
            }
        }
        if (PlayerPrefs.HasKey("Stage") && PlayerPrefs.GetInt("Stage") >= 5)
        {
            if (PlayerPrefs.HasKey("Exist") && PlayerPrefs.GetInt("Exist") >= 5)
            {
                StartCoroutine(ExistStage(stage5, road4));
            }
            else
            {
                Go(5);
            }
        }
    }

    public void levelUpdate()
    {
        int level = PlayerPrefs.GetInt("currentLevel");
        level += 1;
        Debug.Log("next level: " + level);
        PlayerPrefs.SetInt("currentLevel", level);
    }


    public void Go(int num)
    {
        switch (num)
        {
            case 2: 
                StartCoroutine(OpenStage(stage2, road1));
                break;
            case 3:
                StartCoroutine(OpenStage(stage3, road2));
                break;
            case 4:
                StartCoroutine(OpenStage(stage4, road3));
                break;
            case 5:
                StartCoroutine(OpenStage(stage5, road4));
                break;
            default: break;
        }
        
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
        if(PlayerPrefs.HasKey("Stage") && PlayerPrefs.GetInt("Stage") >= 2)
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
        if (PlayerPrefs.HasKey("Stage") && PlayerPrefs.GetInt("Stage") >= 3)
        {
            hideButton();
            GameObject.Find("Intro").transform.GetChild(2).gameObject.SetActive(true);
            return;
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
        if (PlayerPrefs.HasKey("Stage") && PlayerPrefs.GetInt("Stage") >= 4)
        {
            hideButton();
            GameObject.Find("Intro").transform.GetChild(3).gameObject.SetActive(true);
            return;
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
        if (PlayerPrefs.HasKey("Stage") && PlayerPrefs.GetInt("Stage") >= 5)
        {
            hideButton();
            GameObject.Find("Intro").transform.GetChild(4).gameObject.SetActive(true);
            return;
        }
        Debug.Log("Not open");
    }

    public void GoStage5_1()
    {
        StartCoroutine(EnterStage5_1());
    }
    public void GoStage5_2()
    {
        StartCoroutine(EnterStage5_2());
    }
    public void GoStage5_3()
    {
        StartCoroutine(EnterStage5_3());
    }
    public void GoStage5_4()
    {
        StartCoroutine(EnterStage5_4());
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
        SceneManager.LoadScene("Stage2_talk");
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
        SceneManager.LoadScene("Stage3_talk");
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
        SceneManager.LoadScene("Stage4_talk");
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
        SceneManager.LoadScene("Stage5_talk");
    }
    IEnumerator EnterStage5_2()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage5-2");
    }
    IEnumerator EnterStage5_3()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage5-3");
    }
    IEnumerator EnterStage5_4()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage5-4");
    }

    IEnumerator createIcon()
    {
        stage.transform.GetChild(stage.transform.childCount - 1).gameObject.SetActive(true);
        float fadeCount = 0;
        while(fadeCount < 1.0f)
        {
            fadeCount += 0.005f;
            yield return new WaitForSeconds(0.01f);
            stage1.color = new Color(0, 0, 0, fadeCount);

            //Stage�� ������ ����. �� ���������� ���ٴ� ��
            if (!PlayerPrefs.HasKey("Stage") || PlayerPrefs.GetInt("Stage") == 1)
            {
                //�� ���������� ������ ���� 0.2
                stage2.color = new Color(0, 0, 0, fadeCount * 0.2f);
                stage3.color = new Color(0, 0, 0, fadeCount * 0.2f);
                stage4.color = new Color(0, 0, 0, fadeCount * 0.2f);
                stage5.color = new Color(0, 0, 0, fadeCount * 0.2f);
            }
            //Stage�� �־�
            else if (PlayerPrefs.HasKey("Stage"))
            {
                if(PlayerPrefs.GetInt("Stage") == 2)
                {
                    stage3.color = new Color(0, 0, 0, fadeCount * 0.2f);
                    stage4.color = new Color(0, 0, 0, fadeCount * 0.2f);
                    stage5.color = new Color(0, 0, 0, fadeCount * 0.2f);
                }
                else if(PlayerPrefs.GetInt("Stage") == 3)
                {
                    stage4.color = new Color(0, 0, 0, fadeCount * 0.2f);
                    stage5.color = new Color(0, 0, 0, fadeCount * 0.2f);
                }
                else if (PlayerPrefs.GetInt("Stage") == 4)
                {
                    stage5.color = new Color(0, 0, 0, fadeCount * 0.2f);
                }
            }
        }
    }

    public IEnumerator OpenStage(Image img, GameObject road)
    {
        //������ ȿ���� �ֱ�
        float fadeCount = 0.0f;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.005f;
            yield return new WaitForSeconds(0.01f);
            img.color = new Color(0, 0, 0, fadeCount);
        }
        
        for(int i = 0; i < road.transform.childCount; i++)
        {
            yield return new WaitForSeconds(0.3f);
            road.transform.GetChild(i).GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }

        PlayerPrefs.SetInt("Exist", PlayerPrefs.GetInt("Stage"));
    }

    public IEnumerator ExistStage(Image img, GameObject road)
    {
        StartCoroutine(ExistRoad(road));
        float fadeCount = 0.0f;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.005f;
            yield return new WaitForSeconds(0.01f);
            img.color = new Color(0, 0, 0, fadeCount);
        }
        
    }
    public IEnumerator ExistRoad(GameObject road)
    {
        float fadeCount = 0.0f;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.005f;
            yield return new WaitForSeconds(0.01f);
            for(int i = 0; i < road.transform.childCount; i++)
            {
                road.transform.GetChild(i).GetComponent<Image>().color = new Color(0, 0, 0, fadeCount);
            }
        }
    }

    public void hideButton()
    {
        for(int i = 0; i < stage.transform.childCount; i++)
        {
            stage.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
