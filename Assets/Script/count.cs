using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class count : MonoBehaviour
{
   // public TextMeshProUGUI countNum;

    public static count Instance;

    public TMP_Text count_text;
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
        leftNum = 0;
    }

    public void fixMaxValue(int count)
    {
        bossGage.maxValue = count;
        leftNum = count + 1;
        count_text.text = leftNum.ToString();
    }

    public void fillBar()
    {
        bossGage.value += 1;
        leftNum -= 1;
        count_text.text = leftNum.ToString();

        if(leftNum == 0)
        {
            switch (TileArray.Instance.bossType)
            {
                case "T":
                Boss.Instance.Boss_Die();
                break;

                case "L":
                Boss_littleHunter.Instance.Boss_Die();
                break;

                case "A":
                Boss_AdultHunter.Instance.Boss_Die();
                break;

                case "G":
                Boss_goblinKing.Instance.Boss_Die();
                break;
                
                default:
                break;
            }
        }
    }
}
