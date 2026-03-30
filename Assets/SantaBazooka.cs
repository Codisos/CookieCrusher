using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaBazooka : MonoBehaviour
{
    [Tooltip("First time the bazooka fires (is seconds)")][SerializeField] private float firstTime = 70;
    [SerializeField] private float minTime=50;
    [SerializeField] private float maxTime=120;
    private float explosionRadius=1.9f;
    [SerializeField] private GameObject[] impactAreas;
    [SerializeField] private AnimationClip[] animations;
    [SerializeField] private GameObject rocketVFX;
    [SerializeField] AudioClip rocketSFX;
    [SerializeField] private GameObject explosionVFX;
    [SerializeField] AudioClip santaLiftingSFX;
    [SerializeField] Transform CameraRef;
    private float time = 0;
    private float spawnTime;
    private int currentAreaID = 0;      //is also for aniamtion array
    private bool _isActive = false;
    [HideInInspector]public bool stopFire=false;
    private AudioSource rocketAudio;
    SceneManager sManager;

    private void Start()
    {
        spawnTime = firstTime;
        sManager = FindObjectOfType<SceneManager>();
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
        //Check if its the right time to spawn the object
        if (time >= spawnTime && !_isActive && !stopFire)
        {
            
            StartBazookaSequence();
            SetRandomTime();
            time = 0;
        }
    }

    public void HighlightArea()
    {
        //make the right area visible
        impactAreas[currentAreaID].SetActive(true);        
    }

    void StartBazookaSequence()     //if called, starts random animation
    {
        currentAreaID = Random.Range(0,3);
        _isActive = true;
        GetComponent<Animation>().Play(animations[currentAreaID].name);
        GetComponentInParent<AudioSource>().PlayOneShot(santaLiftingSFX);

    }

    void SetRandomTime ()
	{
		spawnTime = Random.Range (minTime, maxTime);
	}

    public void Explosion()
    {
        rocketVFX.SetActive(false);
        Destroy(rocketAudio);
        Instantiate(explosionVFX, impactAreas[currentAreaID].transform.position, impactAreas[currentAreaID].transform.rotation);
        Vector3 impactZoneCheck = new Vector3(impactAreas[currentAreaID].transform.position.x, impactAreas[currentAreaID].transform.position.y + 1, impactAreas[currentAreaID].transform.position.z);

        if (Vector3.Distance(CameraRef.position, impactZoneCheck) <= explosionRadius)     //if player is "near" expolsion game over
        {
            sManager.GameOverBazooka();
        }

        impactAreas[currentAreaID].SetActive(false);
        _isActive = false;
        currentAreaID = 0;       
    }

    public void IgniteRocket()
    {
        rocketVFX.SetActive(true);
        rocketAudio = rocketVFX.GetComponentInParent<Transform>().gameObject.AddComponent<AudioSource>();
        rocketAudio.playOnAwake = false;
        rocketAudio.spatialBlend = 1f;
        rocketAudio.volume = 0.8f;
        rocketAudio.PlayOneShot(rocketSFX);    
    }

}
