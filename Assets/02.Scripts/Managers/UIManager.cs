using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{ 
    // static variables
    public static UIManager Instance;
    // public variables
    public TextMeshProUGUI modeTitle;
    public TextMeshProUGUI roundText;
    public TMP_Text moneyText; 
    [SerializeField] private TextMeshProUGUI alarmText;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void DisplayAlarmText(string message)
    {
        StartCoroutine(DisplayAlarmTextCoroutine(message));
    }
    
    IEnumerator DisplayAlarmTextCoroutine(string message)
    {
        alarmText.text = message;
        alarmText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        alarmText.gameObject.SetActive(false);
    }
}
