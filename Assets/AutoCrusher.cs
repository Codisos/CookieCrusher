using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCrusher : MonoBehaviour
{
    public CookieManager cookieManager;
    public HandsCollison B1;
    public HandsCollison B2;
    public HandsCollison B3;

    public GameObject particleSpawner;
    public GameObject cookieCrushParticles;

    public void AddCookieB1()
    {
        cookieManager.AddCookiesByAuto(B1);
        Instantiate(cookieCrushParticles, particleSpawner.transform);
    }
    public void AddCookieB2()
    {
        cookieManager.AddCookiesByAuto(B2);
        Instantiate(cookieCrushParticles, particleSpawner.transform);
    }
    public void AddCookieB3()
    {
        cookieManager.AddCookiesByAuto(B3);
        Instantiate(cookieCrushParticles, particleSpawner.transform);
    }
}
