using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(createIcon());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Stage1()
    {
        GameObject.Find("Intro").transform.GetChild(0).gameObject.SetActive(true);
    }

    public void GoStage1_1()
    {
        StartCoroutine(EnterStage1_1());
    }

    public void Stage2()
    {
        GameObject.Find("Intro").transform.GetChild(1).gameObject.SetActive(true);
    }

    public void GoStage1_2()
    {
        StartCoroutine(EnterStage1_2());
    }

    public void Stage3()
    {
        GameObject.Find("Intro").transform.GetChild(2).gameObject.SetActive(true);
    }

    public void GoStage1_3()
    {
        StartCoroutine(EnterStage1_3());
    }

    public void Stage4()
    {
        GameObject.Find("Intro").transform.GetChild(3).gameObject.SetActive(true);
    }

    public void GoStage1_4()
    {
        StartCoroutine(EnterStage1_4());
    }

    IEnumerator EnterStage1_1()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Stage1-1");
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

    IEnumerator createIcon()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("StageSelectMenu").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("StageSelectMenu").transform.GetChild(2).gameObject.SetActive(true);
        GameObject.Find("StageSelectMenu").transform.GetChild(3).gameObject.SetActive(true);
        GameObject.Find("StageSelectMenu").transform.GetChild(4).gameObject.SetActive(true);
        GameObject.Find("StageSelectMenu").transform.GetChild(5).gameObject.SetActive(true);
        GameObject.Find("StageSelectMenu").transform.GetChild(6).gameObject.SetActive(true);

    }

}
