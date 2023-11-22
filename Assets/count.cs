using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class count : MonoBehaviour
{
    public TextMeshProUGUI countNum;
    private int leftNum;
    // Start is called before the first frame update
    void Start()
    {
        leftNum = FindObjectOfType<Boss>().boss_count;
        countNum.text = leftNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        countNum.text = FindObjectOfType<Boss>().boss_count.ToString();
    }
}
