using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private CookieManager cookieManager;
    [SerializeField] private TMP_Text CrushCostText;
    [SerializeField] private TMP_Text AutoCostText;
    [SerializeField] private TMP_Text ButtonCostText;
    [SerializeField] private HandsCollison cookieButton1;
    [SerializeField] private HandsCollison cookieButton2;
    [SerializeField] private HandsCollison cookieButton3;
    [SerializeField] private GameObject cookieBtn2Parent;
    [SerializeField] private GameObject cookieBtn3Parent;

    //----------------------CRUSH UPGRADES---------------------------------------------
    [SerializeField] private int[] CrushUpgrades = { 1, 2, 3, 5, 10, 100 };
    [SerializeField] private int[] CrushUpgradesPrice = { 0, 250, 1000, 2500, 5000, 10000 };
    private int currentCrushUpgrade = 0;

    //----------------------BUTTON UPGRADES---------------------------------------------
    [SerializeField] private int[] BtnUpgrades = { 1, 10, 50, 150 };       //table with button upgrades
    //current crush upgrade is stored in individual button(HandsCollision)
    private int currentButton1Upgrade = 0;
    private int currentButton2Upgrade = 0;
    private int currentButton3Upgrade = 0;
    HandsCollison buttonToUpgrade = null;
    int upgradeAmmount = 0;
    private int currentUpgradePrice;
    [SerializeField] private int[] ButtonUpgradePrice1 = { 5000, 15000, 35000, 50000 };
    [SerializeField] private int[] ButtonUpgradePrice2 = { 5000, 15000, 35000, 50000 };
    [SerializeField] private int[] ButtonUpgradePrice3 = { 5000, 15000, 35000, 50000 };


    //----------------------AUTO CRUSH UPGRADES-----------------------------------------
    [SerializeField] private int autoCrushUpgrade = 10;
    [SerializeField] private int[] autoCrushUpgradePrice1 = { 7000, 10000, 18000, 25000 };
    [SerializeField] private int[] autoCrushUpgradePrice2 = { 7000, 10000, 18000, 25000 };
    [SerializeField] private int[] autoCrushUpgradePrice3 = { 7000, 10000, 18000, 25000 };
    private int currentAutoCrush = 0;
    private int currentAutoUpgradePrice;

    private void Start()
    {
        CrushCostText.text = CrushUpgradesPrice[currentCrushUpgrade + 1].ToString();    //set crush upgrade price text
        AutoCostText.text = autoCrushUpgradePrice1[currentAutoCrush].ToString();
        ButtonCostText.text = ButtonUpgradePrice1[0].ToString();
        currentAutoUpgradePrice = autoCrushUpgradePrice1[0];
    }


    //----------------------CRUSH UPGRADES---------------------------------------------
    public int GetCurrentCrushUpgrade()     //for upgrade text etc.
    {
        return currentCrushUpgrade;
    }

    public void SetCrushUpgrade()
    {
        if (CrushFundsCheck())
        {
            if (currentCrushUpgrade == 5)
                return;
            cookieManager.SetCookieCount(CrushUpgradesPrice[currentCrushUpgrade + 1] * (-1));
            currentCrushUpgrade++;
            string mess = "NO UPGRADES";
            CrushCostText.text = (currentCrushUpgrade + 1) == 6? mess : CrushUpgradesPrice[currentCrushUpgrade + 1].ToString();
            CrushCostText.color = (currentCrushUpgrade + 1) == 6 ? new Color(1, 0, 0, 1) : Color.white;
            CrushCostText.fontSize = (currentCrushUpgrade + 1) == 6 ? 20f : 23.6f;
        }
        cookieManager.UpdateCounter();
    }

    private bool CrushFundsCheck()
    {
        if ((currentCrushUpgrade + 1 != 6) && cookieManager.GetCookieCount() > CrushUpgradesPrice[currentCrushUpgrade + 1])
            return true;
        return false;
    }


    //----------------------BUTTON UPGRADES--------------------------------------------

    public int[] GetButtonUpgradeArray()
    {
        return BtnUpgrades;
    }

    public void SetButtonUpgrade()
    {
        if (ButtonFundsCheck())
        {
            cookieManager.SetCookieCount(currentUpgradePrice * (-1));

            if (buttonToUpgrade == cookieButton1)
            {
                cookieButton1.SetBtnUpgrade();
                upgradeAmmount++;
                ButtonCostText.text = ButtonUpgradePrice1[upgradeAmmount].ToString();   //set new cost text
            }
            else if (buttonToUpgrade == cookieButton2)
            {
                cookieButton2.SetBtnUpgrade();
                upgradeAmmount++;
                if (upgradeAmmount - 4 == 0)
                    cookieBtn2Parent.SetActive(true);       //if button has no upgrades, activate it

                ButtonCostText.text = ButtonUpgradePrice2[upgradeAmmount - 4].ToString(); //set new cost text
            }
            else if (buttonToUpgrade == cookieButton3)
            {
                if(!(upgradeAmmount > 12))
                    cookieButton3.SetBtnUpgrade();
                upgradeAmmount++;
                if (upgradeAmmount - 8 == 0)
                    cookieBtn3Parent.SetActive(true);       //if button has no upgrades, activate it

                string mess = "NO UPGRADES";
                ButtonCostText.text = (upgradeAmmount - 7 == 4) ? mess : ButtonUpgradePrice3[upgradeAmmount - 7].ToString(); //set new cost text
                ButtonCostText.color = (upgradeAmmount - 7 == 4) ? new Color(1, 0, 0, 1) : Color.white;
                ButtonCostText.fontSize = (upgradeAmmount - 7 == 4) ? 20f : 23.6f;
            }
        }
        cookieManager.UpdateCounter();
    }

    private bool ButtonFundsCheck()
    {
        if (upgradeAmmount >= 12)
            return false;

        if (cookieBtn3Parent.activeInHierarchy) //upgrades for 1st button
        {
            if ((upgradeAmmount >= 8 && upgradeAmmount < 12) && cookieManager.GetCookieCount() >= ButtonUpgradePrice3[upgradeAmmount-8])    //upgrade 3th button check
            {
                currentUpgradePrice = ButtonUpgradePrice3[upgradeAmmount-8];
                buttonToUpgrade = cookieButton3;
                return true;
            }
            else 
            {
                return false;
            }
        }
        else if (cookieBtn2Parent.activeInHierarchy)
        {
            if ((upgradeAmmount < 7 && upgradeAmmount >= 4) && cookieManager.GetCookieCount() >= ButtonUpgradePrice2[upgradeAmmount-4])    //upgrade 2nd button check
            {
                currentUpgradePrice = ButtonUpgradePrice2[upgradeAmmount-4];
                buttonToUpgrade = cookieButton2;
                return true;
            }
            else if (upgradeAmmount == 7 && cookieManager.GetCookieCount() >= ButtonUpgradePrice2[upgradeAmmount-4])     //buy a new button check
            {
                currentUpgradePrice = ButtonUpgradePrice2[upgradeAmmount-4];
                buttonToUpgrade = cookieButton3;
                return true;
            }
            return false;
        }
        else //upgrades for 1st button
        {
            if (upgradeAmmount < 3 && cookieManager.GetCookieCount() >= ButtonUpgradePrice1[upgradeAmmount])    //upgrade 1st button check
            {
                currentUpgradePrice = ButtonUpgradePrice1[upgradeAmmount];
                buttonToUpgrade = cookieButton1;
                return true;
            }
            else if (upgradeAmmount == 3 && cookieManager.GetCookieCount() >= ButtonUpgradePrice1[upgradeAmmount])     //buy a new button check
            {
                currentUpgradePrice = ButtonUpgradePrice1[upgradeAmmount];
                buttonToUpgrade = cookieButton2;
                return true;
            }
            return false;
        }

    }




    //---------------------AUTOCLICKER UPGRADE------------------------------------------


    public int GetCurrentAutoCrushUpgrade()
    {
        return currentAutoCrush;
    }

    public void SetAutoCrushUpgrade()
    {

        if (AutoFundsCheck())
        {
            if (currentAutoCrush >= 12)
                return;

            

            if (currentAutoCrush < 4)
            {
                cookieManager.SetCookieCount(currentAutoUpgradePrice * (-1));
                cookieButton1.AddNewAutoclicker(currentAutoCrush);
                AutoCostText.text = (currentAutoCrush + 1) == 4 ? autoCrushUpgradePrice2[0].ToString() : autoCrushUpgradePrice1[currentAutoCrush + 1].ToString();
                RunUpgradeCostAndActivation();
            }
            else if ((currentAutoCrush >= 4 && currentAutoCrush < 8 && cookieButton2.enabled)) //and if there is a second button check, button upgrade check removed
            {
                cookieManager.SetCookieCount(currentAutoUpgradePrice * (-1));
                cookieButton2.AddNewAutoclicker(currentAutoCrush - 4);
                AutoCostText.text = (currentAutoCrush - 3) == 4 ? autoCrushUpgradePrice3[0].ToString() : autoCrushUpgradePrice2[currentAutoCrush - 3].ToString();
                RunUpgradeCostAndActivation();
            }
            else if(currentAutoCrush >= 8 && cookieButton3.enabled) //and if there is a third button check, also btn chk removed
            {
                cookieManager.SetCookieCount(currentAutoUpgradePrice * (-1));
                cookieButton3.AddNewAutoclicker(currentAutoCrush - 8);
                string mess = "NO UPGRADES";
                AutoCostText.text = (currentAutoCrush - 8) == 3 ? mess : autoCrushUpgradePrice3[currentAutoCrush - 8].ToString();
                AutoCostText.color = (currentAutoCrush - 8) == 3 ? new Color(1, 0, 0, 1) : Color.white;
                AutoCostText.fontSize = (currentAutoCrush - 8) == 3 ? 20f : 23.6f;
                RunUpgradeCostAndActivation();
            }

            
        }
        cookieManager.UpdateCounter();
    }

    void RunUpgradeCostAndActivation()
    {
        
        currentAutoCrush++;
        cookieManager.AddOneToActiveAutoCrushers();
        currentAutoUpgradePrice = 0;
    }

    private bool AutoFundsCheck()
    {
        if (currentAutoCrush < 4)
        {
            if (cookieManager.GetCookieCount() >= autoCrushUpgradePrice1[currentAutoCrush])
            {
                currentAutoUpgradePrice = autoCrushUpgradePrice1[currentAutoCrush];
                return true;
            }
            return false;
        }
        else if (currentAutoCrush >= 4 && currentAutoCrush < 8)
        {
            if (cookieManager.GetCookieCount() >= autoCrushUpgradePrice2[currentAutoCrush - 4])
            {
                currentAutoUpgradePrice = autoCrushUpgradePrice2[currentAutoCrush - 4];
                return true;
            }
            return false;
        }
        else
        {

            if (cookieManager.GetCookieCount() >= autoCrushUpgradePrice3[currentAutoCrush - 8])
            {
                currentAutoUpgradePrice = autoCrushUpgradePrice3[currentAutoCrush - 8];
                return true;
            }
            return false;
        }

    }
}
