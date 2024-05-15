using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{ 
    public static UIManager Instance;
    public TextMeshProUGUI modeTitle;
    public TextMeshProUGUI roundText;
    [SerializeField] private TextMeshProUGUI alarmText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
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
