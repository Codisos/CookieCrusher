using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsCollison : MonoBehaviour
{
    public GameObject particles;
    public CookieManager cookieManager;
    public UpgradeManager upManager;
    public SceneManager sceneManager;
    public Animation anim;
    public AudioClip cookieHit;
    private AudioSource cookieAudio;


    [Header("This buttons autoclickers")]
    [SerializeField] private GameObject[] autoclickers;
    private int currentAutoUpgrades = 0;
    private int currentBtnUpgrade = 0;


    private void Start()
    {
        if (cookieManager == null)
        {
            cookieManager = FindObjectOfType<CookieManager>();
        }
        currentAutoUpgrades = upManager.GetCurrentAutoCrushUpgrade();

        cookieAudio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if (collision.gameObject.CompareTag("AutoCrush"))         //if button is done by autoclicker
            cookieManager.AddCookiesByAuto(this);

        cookieManager.AddCookiesByHand(this);       //if btn click is by hand

        cookieAudio.PlayOneShot(cookieHit);
        SpawnParticles(pos, rot);
        CookieAnim();
    }

    private void SpawnParticles(Vector3 pos, Quaternion rot)
    {
        Instantiate(particles, pos, gameObject.transform.rotation, gameObject.transform);
    }

    private void CookieAnim()
    {
        anim.Play("CookieHit");
    }

    public int GetButtonUpgradeInfo()
    {
        return currentBtnUpgrade;
    }

    public void SetBtnUpgrade()
    {
        if(currentBtnUpgrade < 12)
            currentBtnUpgrade++;
    }
    //--------------------------------AUTOCLICKER UPGRADE--------------------------------------------------

    public void AddNewAutoclicker(int index)
    {
        autoclickers[index].SetActive(true);
        currentAutoUpgrades = upManager.GetCurrentAutoCrushUpgrade();
    }

}
