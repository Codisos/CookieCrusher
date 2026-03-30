using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MilkCannon : MonoBehaviour
{
    [SerializeField] private CookieManager cookieManager;
    [SerializeField] private AngerMeter angerMeter;
    [SerializeField] private float angerDecreseByMilk = 50f;
    private int fireCost = 0;
    [SerializeField] private Animation anim;
    public GameObject particles;
    public Transform milkSplashSpawner;
    public AudioClip cannonShot;
    public AudioSource milkAudioSource;

    private void Start()
    {
    }

    public void Fire()
    {
        if (CheckFunds())
        {
            GetComponent<AudioSource>().PlayOneShot(cannonShot);
            anim.Play("Fire");     
        }


    }

    bool CheckFunds()
    {
        //cost is a 1/3 of cookies that player has
        int cookies = cookieManager.GetCookieCount();
        if (cookies != 0 && ((cookies - GetCost()) > 0))
        {
            cookieManager.MilkBought(cookies-GetCost());
            return true;
        }
        return false;
    }

    int GetCost()
    {
        int cookies = cookieManager.GetCookieCount();
        fireCost = cookies - (cookies / 4);
        return fireCost;
    }

    public void OnMilk()
    {
        angerMeter.LowerAngerValue(angerDecreseByMilk);
        Instantiate(particles, milkSplashSpawner.position, milkSplashSpawner.rotation, milkSplashSpawner);
        milkAudioSource.Play();
    }
}
