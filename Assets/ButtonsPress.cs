using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ButtonsPress : MonoBehaviour
{
    public GameObject button;
    public float ColWait = 1;
    public AudioClip ButtonSFX;
    public UnityEvent OnPressed;
    

    Collider buttonCol;
    SceneManager manager;

    private void Start()
    {
        buttonCol = button.GetComponent<Collider>();
        manager = FindObjectOfType<SceneManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "btn")
        {
            buttonCol.enabled = false;
            StartCoroutine(EnableCol());
            OnPressed.Invoke();
            GetComponent<AudioSource>().PlayOneShot(ButtonSFX);
        }
    }

    IEnumerator EnableCol()
    {
        yield return new WaitForSeconds(ColWait);
        buttonCol.enabled = true;
    }
    
}
