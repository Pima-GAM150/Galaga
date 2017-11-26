using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Timer : MonoBehaviour
{

    public float countDown = 180;
    public Text timerText;

    // Use this for initialization
    void Start()
    {
        timerText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        timerText.text = countDown.ToString("f0");
        if (countDown <= 0)
        {
            Application.LoadLevel("Lv3");
        }
    }
}