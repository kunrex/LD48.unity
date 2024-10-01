using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private HealthBar bossBar;
    [SerializeField] private HealthBar playerBar;
    [SerializeField] private Instruction[] instructions;
    [SerializeField] private Text speedText, crabsToKillText, piranhasToKillText;

    [SerializeField] private GameObject piranhasUI, crabBossUI;
    [SerializeField] private Text messageText;

    [SerializeField] private GameObject pauseMenu, gameOver;

    [SerializeField] private Text resultText;
    [SerializeField] private Text timeTextRegular;
    [SerializeField] private Text timeTextGameEnd;
    [SerializeField] private GameObject bestTimeGO;
    [SerializeField] private Animator animator;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        int totalSeconds = Mathf.RoundToInt(Time.timeSinceLevelLoad);
        int seconds = totalSeconds % 60;
        int minutes = totalSeconds / 60;
        string time = "Time: " + minutes + ":" + seconds;

        timeTextRegular.text = time;
    }

    public void SetBossHealth(float health, bool max = false)
    {
        if (max)
            bossBar.SetMaxHealth(health);
        else
            bossBar.SetHealth(health);
    }

    public void SetPlayerHealth(float health, bool max = false)
    {
        if (max)
            playerBar.SetMaxHealth(health);
        else
            playerBar.SetHealth(health);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);

        AudioManager.instance.PlaySound("Button");
        PlayButtonSound();
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);

        AudioManager.instance.PlaySound("Button");
    }

    public void Retry()
    {
        AudioManager.instance.PlaySound("Button");
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void MainMenu()
    {
        AudioManager.instance.PlaySound("Button");
        StartCoroutine(LoadScene(0));
    }

    private IEnumerator LoadScene(int index)
    {
        animator.SetTrigger("tran");
        Time.timeScale = 1;

        AudioManager.instance.StopSound("Boss");
        AudioManager.instance.PlaySound("Music");
        yield return new WaitForSecondsRealtime(1.5f);

        SceneManager.LoadScene(index);
    }

    public void EndGame(int result)
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);

        AudioManager.instance.PlaySound("Button");

        switch(result)
        {
            case 0:
                resultText.text = "You Won!";
                break;
            case 1:
                resultText.text = "The sub exploded";
                break;
        }

        int totalSeconds = Mathf.RoundToInt(Time.timeSinceLevelLoad);
        int seconds = totalSeconds % 60;
        int minutes = totalSeconds / 60;
        string time = "Time: " + minutes + ":" + seconds;

        timeTextGameEnd.text = time;

        if(result == 0)
        {
            float bestTime = PlayerPrefs.GetFloat("BestTime", Mathf.Infinity);

            if(Time.timeSinceLevelLoad < bestTime)
            {
                PlayerPrefs.SetFloat("BestTime", Time.timeSinceLevelLoad);
                bestTimeGO.SetActive(true);
            }
        }
    }

    public void EnableInstruction(int index) => instructions[index].Enable();

    public void DisableInstruction(int index) => instructions[index].Disable();

    public void SetSpeed(float value) => speedText.text = value.ToString();

    public void SetCrabsToKill(int val) => crabsToKillText.text = val.ToString();

    public void SetPiranhasToKill(int val) => piranhasToKillText.text = val.ToString();

    public void SetPiranhaUI(bool toSet) => piranhasUI.SetActive(toSet);

    public void SetCrabUI(bool toSet) => crabBossUI.SetActive(toSet);

    public void SendMessageToPlayer(string value)
    {
        messageText.text = value;
    }

    public void PlayButtonSound() => AudioManager.instance.PlaySound("Button");
}
