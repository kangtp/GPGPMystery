using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartBtn : MonoBehaviour
{
    AudioSource paper;
    public Image[] buttons;

    public GameObject helpImage;

    public GameObject settingUI;

    public GameObject soundOnBtn;
    public GameObject soundOffBtn;
    // Start is called before the first frame update
    void Start()
    {
        paper = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    public void GameStart()
    {
        //���ӽ��۹�ư�� ������ ��ư UI �Ⱥ��̰��ϰ� �η縶�� �ִϸ��̼��� ����
        for(int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].enabled = false;
        }
        GameObject.Find("Ani").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Manage").GetComponent<AudioSource>().Stop();
        paper.Play();
        StartCoroutine(StartGame());
    }

    public void Setting()
    {
        for(int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].enabled = false;
        }
        GameObject.Find("Ani").transform.GetChild(0).gameObject.SetActive(true);
        paper.Play();
        //settingUI.SetActive(true);
        StartCoroutine(Setimage());
    }

    public void SonudStopBtn()
    {
        soundOnBtn.SetActive(false);
        soundOffBtn.SetActive(true);
        GameObject.Find("Manage").GetComponent<AudioSource>().Stop();
    }
    public void SonudStartBtn()
    {
        soundOnBtn.SetActive(true);
        soundOffBtn.SetActive(false);
         GameObject.Find("Manage").GetComponent<AudioSource>().Play();
    }

    public void Help()
    {
        for(int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].enabled = false;
        }
        GameObject.Find("Ani").transform.GetChild(0).gameObject.SetActive(true);
        paper.Play();
        StartCoroutine(Helpimage());
    }

    public void HelpBack()
    {
        for(int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].enabled = true;
        }
        GameObject.Find("Ani").transform.GetChild(0).gameObject.SetActive(false);
        helpImage.SetActive(false);
    }

    public void setBack()
    {
        for(int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].enabled = true;
        }
        GameObject.Find("Ani").transform.GetChild(0).gameObject.SetActive(false);
        settingUI.SetActive(false);
    }


    IEnumerator StartGame()
    {
        //1.5�� �ڿ� �����޴��� �̵�
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Level Menu");
    }

    IEnumerator Helpimage()
    {
        yield return new WaitForSeconds(1.5f);
        helpImage.SetActive(true);
    }

     IEnumerator Setimage()
    {
        yield return new WaitForSeconds(1.5f);
        settingUI.SetActive(true);
    }
}
