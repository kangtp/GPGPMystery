using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    AudioSource audioSource;
    public int boss_count;
    public GameObject boss;
    public GameObject player;
    private bool move = false;
    private float origin;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        boss = FindObjectOfType<TileArray>().boss;
        player = FindObjectOfType<TileArray>().player;
        origin = Vector3.Distance(boss.transform.position, player.transform.position);
        count.Instance.fixMaxValue(boss_count);
    }

    //private void FixedUpdate()
    //{
    //    if (move)
    //    {
    //        const float scaleFactor = 1.1f;
    //        const float minScaleFactor = 0.9f;
    //        const float moveSpeed = 10f;
    //        //const float waitTime = 0.1f;
    //        const float minDistance = 1f;
    //        Vector3 direction = player.transform.position - boss.transform.position;
    //        direction.Normalize();
    //        float distance = Vector3.Distance(boss.transform.position, player.transform.position);

    //        if (distance > (origin * 0.5))
    //        {
    //            boss.transform.localScale *= scaleFactor;
    //        }
    //        else
    //        {
    //            boss.transform.localScale *= minScaleFactor;
    //        }

    //        boss.transform.Translate(direction * moveSpeed);

    //        if (distance < minDistance)
    //        {
    //            move = false;
    //            ShakeScreen.Instance.Callshake();
    //            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //        }
    //    }
    //}

    public void reduceCount()
    {
        boss_count -= 1;
        count.Instance.fillBar();
        CheckCount();
    }

    void CheckCount()
    {
        if (boss_count == -1)
        {
            TileArray.Instance.Touchable = false;
            FindObjectOfType<count>().isOver = true;
            audioSource.Play();
            
            move = true;
            StartCoroutine(MoveBoss());
            GameOver();
            boss_count = -2;
        }
    }

    void GameOver()
    {
        Debug.Log("GameOver!!!");
    }

    IEnumerator MoveBoss()
    {
        while (true)
        {
            //yield return new WaitForSeconds(0.05f);
            Vector3 direction = player.transform.position - boss.transform.position;
            direction.Normalize();
            //float distance = Vector3.Distance(boss.transform.position, player.transform.position);
            yield return new WaitForSeconds(0.03f);
            float distance = Vector3.Distance(boss.transform.position, player.transform.position);
            if (distance > (origin * 0.5))
            {
                boss.transform.localScale *= 1.1f;
            }
            else
            {
                boss.transform.localScale *= 0.9f;
            }
            //yield return new WaitForSeconds(0.05f);
            boss.transform.Translate(direction * 100 * Time.deltaTime);
            
            if (distance < 1)
            {
                move = false;
                ShakeScreen.Instance.Callshake(); // 화면 흔들림 함수 호출
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            yield return null;
        }
    }

}
