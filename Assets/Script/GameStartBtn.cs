using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartBtn : MonoBehaviour
{
    AudioSource paper;
    public Image[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        paper = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    IEnumerator StartGame()
    {
        //1.5�� �ڿ� �����޴��� �̵�
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Level Menu");
    }
}
