using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueSystem : MonoBehaviour
{
    public string nextScene;
    public TMP_Text txtName;
    public TMP_Text txtSentence;

    public GameObject txtImage;

    Queue<string> sentences = new Queue<string>();
    Queue<string> names = new Queue<string>();

    Queue<GameObject> images = new Queue<GameObject>();
    Fadeinout fadeinout;

    public int nextStage;
    void Awake()
    {
        fadeinout = FindAnyObjectByType<Fadeinout>();
        fadeinout.fadeOut();
    }

    public void Begin(Dialogue info)
    {
        sentences.Clear();

        foreach (var name in info.name)
        {
            names.Enqueue(name);
        }

        foreach (var sentence in info.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach (var image in info.images)
        {
            images.Enqueue(image);
        }

        Next();

    }

    public void Next()
    {
        if(sentences.Count == 0) // 현재남은 문장들이 없을 경우
        {
            End();
            return;
        }
        //txtSentence.text = sentences.Dequeue();
        txtName.text = string.Empty;
        txtSentence.text = string.Empty;
        txtImage.SetActive(false);
        txtName.text = names.Dequeue();
        txtImage = images.Dequeue();
        txtImage.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentences.Dequeue()));
    }

    IEnumerator TypeSentence(string sentence)
    {
        foreach (var letter in sentence)
        {
            //audioSource.Play();
            txtSentence.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void End()
    {
        PlayerPrefs.SetInt("Stage", nextStage);
        Debug.Log("save : " + nextStage);
        SceneManager.LoadScene(nextScene);
    }

}
