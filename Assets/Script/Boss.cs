using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int boss_count;
    public GameObject boss;
    public GameObject player;
    public Animation anim;

    private float timer = 0;
    private float curTime = 0;
    private float period = 2;

    public bool move = false;

    // Start is called before the first frame update
    void Start()
    {
        boss = FindObjectOfType<TileArray>().boss;
        player = FindObjectOfType<TileArray>().player;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCount();
        //curTime += Time.deltaTime;
        //if (curTime > period)
        //{
        //    curTime -= curTime;
        //}

        //float xValue = x.Evaluate(curTime);
        //float yValue = y.Evaluate(curTime);

        //boss.transform.position = new Vector3(0, yValue, 0);
    }

    void CheckCount()
    {
        if (boss_count == -1)
        {
            //È£¶ûÀÌ °ø°Ý
            StartCoroutine(MoveBoss());
            GameOver();
            boss_count = 5;
        }
    }

    void GameOver()
    {
        Debug.Log("GameOver!!!!!!!!!!!!!");
    }

    IEnumerator MoveBoss()
    {
        float distance;
        while (true)
        {
            timer += Time.deltaTime;
            Vector3 direction = player.transform.position - boss.transform.position;
            direction.Normalize();
            distance = Vector3.Distance(boss.transform.position, player.transform.position);
            boss.transform.Translate(direction * 1 * Time.deltaTime);
            Debug.Log("ing~");
            if (distance < 0.1)
            {
                Debug.Log("break!!!!!");
                yield return null;
            }
        }

        

    }
}
