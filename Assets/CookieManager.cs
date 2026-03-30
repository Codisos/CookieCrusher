using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CookieManager : MonoBehaviour
{
    private int cookieCount = 0;
    [SerializeField] private TMP_Text counter;
    [SerializeField] private int cookiesForOneClick = 1;
    [SerializeField] private UpgradeManager upgrades;

    private int currentAutoCrushers = 0;

    //COUNTING
    public int GetCookieCount()
    {
        return cookieCount;
    }

    public void SetCookieCount(int input)  //use to hard set cookies
    {
        cookieCount = input < 0 ? cookieCount - input * (-1) : cookieCount + input;
        UpdateCounter();
    }

    public void MilkBought(int input)
    {
        cookieCount -= input;
        UpdateCounter();
    }

    public void AddCookiesByHand(HandsCollison pushedButton)     //button adding
    {
        int[] btnuptemp = upgrades.GetButtonUpgradeArray();
        cookiesForOneClick = upgrades.GetCurrentCrushUpgrade() + btnuptemp[pushedButton.GetButtonUpgradeInfo() == 0 ? 0 : pushedButton.GetButtonUpgradeInfo() - 1]; //adding up hand upgrade and btn upgrade
        cookieCount += cookiesForOneClick;
        UpdateCounter();
    }

    public void AddCookiesByAuto(HandsCollison pushedButton)     //button adding
    {
        int[] btnuptemp = upgrades.GetButtonUpgradeArray();
        cookiesForOneClick = upgrades.GetCurrentAutoCrushUpgrade() * 10 + btnuptemp[pushedButton.GetButtonUpgradeInfo() == 0 ? 0 : pushedButton.GetButtonUpgradeInfo() - 1]; //adding up auto upgrade and btn upgrade
        cookieCount += cookiesForOneClick;
        UpdateCounter();
    }

    public void UpdateCounter()
    {
        counter.text = cookieCount.ToString();
    }

    public void AddOneToActiveAutoCrushers()
    {
        currentAutoCrushers++;
    }

}
