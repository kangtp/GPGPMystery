using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss_AdultHunter : MonoBehaviour
{
    AudioSource audioSource;
    public int boss_count;
    public GameObject boss;
    public GameObject player;
    private bool move = false;
    private float origin;

    static public Boss_AdultHunter Instance;

 
    private Animator Boss_AdultHunter_animator;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Boss_AdultHunter_animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        boss = FindObjectOfType<TileArray>().boss;
        player = FindObjectOfType<TileArray>().player;
        origin = Vector3.Distance(boss.transform.position, player.transform.position);
        count.Instance.fixMaxValue(boss_count);
    }

    public void Boss_Die()
    {
            TileArray.Instance.Touchable = false;
            FindObjectOfType<count>().isOver = true;
            audioSource.Play();

            move = true;
            StartCoroutine(AttackBoss());
    }
    IEnumerator AttackBoss()
    {
       
        Boss_AdultHunter_animator.SetBool("Attack",true);
        yield return new WaitForSeconds(0.5f);
         ShakeScreen.Instance.Callshake(); // 화면 흔들림 함수 호출
        PathManager.Instance.startThrow();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
