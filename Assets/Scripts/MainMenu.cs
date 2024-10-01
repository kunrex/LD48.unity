using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject howToPlay;
    [SerializeField] private UnityEngine.UI.Text timeText;
    [SerializeField] private Animator animator;

    private void Start()
    {
        Time.timeScale = 1;
        float bestTime = PlayerPrefs.GetFloat("BestTime", -1);

        if (bestTime == -1)
        {
            timeText.text = "none";
        }
        else
        {
            int seconds = Mathf.RoundToInt(bestTime) % 60;
            int minutes = Mathf.RoundToInt(bestTime) / 60;
            string time = "Time: " + minutes + ":" + seconds;

            timeText.text = time;
        }
    }

    public void PlayGame()
    {
        PlayerPrefs.SetInt("Hard", 0);
        StartCoroutine(Play());
    }

    public void PlayGameHard()
    {
        PlayerPrefs.SetInt("Hard", 1);
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        AudioManager.instance.PlaySound("Button");
        animator.SetTrigger("tran");

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        AudioManager.instance.PlaySound("Button");
        Application.Quit();
    }

    public void HowToPlay()
    {
        howToPlay.SetActive(!howToPlay.activeSelf);
        AudioManager.instance.PlaySound("Button");
    }
}
