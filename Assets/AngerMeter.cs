using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngerMeter : MonoBehaviour
{
    float currentAngerLevel = 0;
    [SerializeField]private float angerIncrese = 0.5f;
    [SerializeField] private Slider angerMeterUI;
    [SerializeField] private SceneManager sceneManager;
    private bool wasDropped;

    // Start is called before the first frame update
    void Start()
    {
        angerMeterUI.value = currentAngerLevel;
        wasDropped = false;
    }

    private void Update()
    {
        if (currentAngerLevel < 100)
        {
            currentAngerLevel += angerIncrese * Time.deltaTime;
            angerMeterUI.value = currentAngerLevel;
        }
        else if (currentAngerLevel >= 100 && !wasDropped)
        {
            DropTheGameOver();
            wasDropped = true;
        }
    }

    void IncreseAngerValue()        //set it to increse over time
    {
        angerIncrese += 0.02f;
    }

    public void LowerAngerValue(float lowerByMilk)      //use milk to lower anger
    {
        if (currentAngerLevel < lowerByMilk)
            currentAngerLevel = 0;
        else
            currentAngerLevel -= lowerByMilk;

        angerMeterUI.value = currentAngerLevel;
    }
    
    void DropTheGameOver()
    {
        sceneManager.GameOver();
    }

}
