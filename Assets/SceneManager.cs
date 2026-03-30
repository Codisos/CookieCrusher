using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using KetosGames.SceneTransition;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform gameOverSpawn;
    [SerializeField] private GameObject Drop;
    [SerializeField] private TMP_Text FinalScoreText;
    [SerializeField] private Canvas HighScoreCanvas;
    [SerializeField] private AudioSource santaSource;
    [SerializeField] private SantaBazooka bazookaRef;

    public void GameOver()
    {
        bazookaRef.stopFire = true;
        santaSource.PlayOneShot(santaSource.clip);
        StartCoroutine(WaitForVoice());

        

    }

    public void GameOverBazooka()
    {
        bazookaRef.stopFire = true;
        Player.transform.SetPositionAndRotation(gameOverSpawn.position, gameOverSpawn.rotation);
        int newScore = FindObjectOfType<CookieManager>().GetCookieCount();
        FinalScoreText.text = newScore.ToString();
        ScoreTable(newScore);
    }

    IEnumerator WaitForVoice()
    {
        //play voice 
        yield return new WaitForSeconds(2.8f);
        Drop.GetComponent<Animation>().Play("DropPresent");
        StartCoroutine(WaitForAnimation());
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(0.5f);

        Player.transform.SetPositionAndRotation(gameOverSpawn.position, gameOverSpawn.rotation);

        int newScore = FindObjectOfType<CookieManager>().GetCookieCount();
        FinalScoreText.text = newScore.ToString();
        ScoreTable(newScore);
    }

    public void BackToMenu()
    {
        SceneLoader.LoadScene("MainMenu");
    }

    void ScoreTable(int newScore)
    {
        TMP_Text[] scoreTexts = HighScoreCanvas.GetComponentsInChildren<TMP_Text>();
        SaveSystem.CheckScore(newScore);
        int[] updatedScore = SaveSystem.GetScore();
        for (int i = 0; i <= 4; i++)
        {
            
            scoreTexts[i].text = updatedScore[i].ToString();
        }
    }

}
