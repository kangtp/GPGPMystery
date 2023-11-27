using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class count : MonoBehaviour
{
   // public TextMeshProUGUI countNum;

    public static count Instance;
    private Slider bossGage;
    //public Font font;
    private int leftNum;
    public bool isOver = false;
    // Start is called before the first frame update
    private void Awake() {
        Instance = this;
    }
    private void Start()
    {
        bossGage = GetComponent<Slider>();
        //countNum.text = leftNum.ToString();
        bossGage.minValue = 0;
    }

    public void fixMaxValue(int count)
    {
        bossGage.maxValue = count;
    }

    public void fillBar()
    {
        bossGage.value += 1;
    }

    // Update is called once per frame
    /*
    void Update()
    {
        
        if (!isOver)
        {
            countNum.text = FindObjectOfType<Boss>().boss_count.ToString();
        }
        else
        {
            countNum.text = "��";
        }
    }
    */
}
