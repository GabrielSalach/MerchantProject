using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MoneyBag : MonoBehaviour
{
    // Singleton instance
    public static MoneyBag instance;
    UInt16 moneyCount;

    Text moneyText;

    void Awake() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    void Start() {
        moneyText = transform.GetComponentInChildren<Text>();
        moneyCount = BitConverter.ToUInt16(SavefileManager.ReadSavefile(SavefileManager.moneyCountOffset, 2), 0);
        UpdateMoneyText();
    }

    public void AddMoney(int amount) {
        moneyCount = Convert.ToUInt16(moneyCount + amount);
        SavefileManager.WriteSavefile(BitConverter.GetBytes(moneyCount), SavefileManager.moneyCountOffset, 2);
        UpdateMoneyText();
    }

    public void RemoveMoney(int amount) {
        moneyCount = Convert.ToUInt16(moneyCount - amount);
        SavefileManager.WriteSavefile(BitConverter.GetBytes(moneyCount), SavefileManager.moneyCountOffset, 2);
        UpdateMoneyText();
    }

    public UInt16 GetMoney() {
        return moneyCount;
    }

    void UpdateMoneyText() {
        moneyText.text = moneyCount.ToString(); 
    } 
}
