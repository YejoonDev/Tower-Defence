using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    // static variables
    public static MoneyManager Instance;
    // public variables
    public int currentMoney;

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

    private void Start()
    {
        UIManager.Instance.moneyText.text = currentMoney.ToString();
    }

    public void GiveMoney(int amountToGive)
    {
        currentMoney += amountToGive;
        UIManager.Instance.moneyText.text = currentMoney.ToString();
    }

    public bool SpendMoney(int amountToSpend)
    {
        bool canSpend = false;
        if (amountToSpend <= currentMoney)
        {
            currentMoney -= amountToSpend;
            canSpend = true;
        }
        UIManager.Instance.moneyText.text = currentMoney.ToString();
        return canSpend;
    }
}
