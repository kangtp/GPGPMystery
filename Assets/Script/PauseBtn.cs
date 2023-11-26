using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseBtn : MonoBehaviour
{
    public GameObject pauseUI;

    public GameObject paperAnimation;

    private AudioSource audioSource;
    private Animator animator;

    public Button[] buttons;
    private void Start() 
    {
        paperAnimation.SetActive(false);
        pauseUI.SetActive(false);
        animator = paperAnimation.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }


    public void Pause()
    {
        audioSource.Play();
        TileArray.Instance.Touchable = false;
        buttons[0].interactable = false;
        buttons[1].interactable = false;
        paperAnimation.SetActive(true);
        paperAnimation.GetComponent<AudioSource>().Play();
        StartCoroutine(showlater());
    }

    IEnumerator showlater()
    {
        yield return new WaitForSeconds(0.6f);
        pauseUI.SetActive(true);  
         
    }

    public void GoMenu()
    {
        audioSource.Play();
        SceneManager.LoadScene("Level Menu");
    }

    public void Resume()
    {
        audioSource.Play();
        buttons[0].interactable = true;
        buttons[1].interactable = true;
        pauseUI.SetActive(false);
        animator.SetBool("Back",true);
        StartCoroutine(backani());
    }

    IEnumerator backani()
    {
         paperAnimation.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.6f);
        paperAnimation.SetActive(false);
        TileArray.Instance.Touchable = true;
    }

    public void Restart()
    {
        audioSource.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
