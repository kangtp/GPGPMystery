using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Stage1()
    {
        GameObject.Find("StageSelectMenu").transform.GetChild(6).gameObject.SetActive(true);
        StartCoroutine(EnterStage1());
    }

    public void Stage2()
    {
        GameObject.Find("StageSelectMenu").transform.GetChild(7).gameObject.SetActive(true);
        StartCoroutine(EnterStage2());
    }

    public void Stage3()
    {
        GameObject.Find("StageSelectMenu").transform.GetChild(8).gameObject.SetActive(true);
        StartCoroutine(EnterStage3());
    }

    public void Stage4()
    {
        GameObject.Find("StageSelectMenu").transform.GetChild(9).gameObject.SetActive(true);
        StartCoroutine(EnterStage4());
    }

    IEnumerator EnterStage1()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Stage1");
    }

    IEnumerator EnterStage2()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Stage2");
    }

    IEnumerator EnterStage3()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Stage3");
    }

    IEnumerator EnterStage4()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Stage4");
    }

}
