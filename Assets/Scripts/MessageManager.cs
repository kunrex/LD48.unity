using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Game_Event_Manager.instance.PiranhasCheckPoint += _PiranhaMesssage;
        Game_Event_Manager.instance.CrabBossCheckPoint += _BossMessage;
        Game_Event_Manager.instance.CrabbiesDone += _BossMessage2;
        Game_Event_Manager.instance.WinMessage += _WinMessage;
        Game_Event_Manager.instance.DodgeCheck += _DodgeMessage;
        Game_Event_Manager.instance.PiranhasDone += _PiranhaWinMessage;
        Game_Event_Manager.instance.DodgeDone += _DodgeDone;

        StartCoroutine(StartMessage());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _PiranhaMesssage() => StartCoroutine(PiranhaMessage());
    public void _BossMessage() => StartCoroutine(BossMessage());
    public void _BossMessage2() => StartCoroutine(BossMessage2());
    public void _WinMessage() => StartCoroutine(WinMessage());
    public void _DodgeMessage() => StartCoroutine(DodgeTime());
    public void _PiranhaWinMessage() => StartCoroutine(PiranhaWinMessage());
    public void _DodgeDone() => StartCoroutine(DodgeDone());

    IEnumerator StartMessage()
    {
        UIManager.instance.SendMessageToPlayer("");
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
        UIManager.instance.SendMessageToPlayer("Hello explorer! Welcome to the depths.");
        AudioManager.instance.PlaySound("Message");
        yield return new WaitForSecondsRealtime(3f);
        UIManager.instance.SendMessageToPlayer("The Treasure seems to be at the cave of the Giant Crab.");
        AudioManager.instance.PlaySound("Message");
        yield return new WaitForSecondsRealtime(3f);
        UIManager.instance.SendMessageToPlayer("Take any of the 2 paths to get there.");
        AudioManager.instance.PlaySound("Message");
        yield return new WaitForSecondsRealtime(3f);
        UIManager.instance.SendMessageToPlayer("");
        Time.timeScale = 1;
    }

    IEnumerator PiranhaMessage()
    {
        UIManager.instance.SendMessageToPlayer("The Sonar is detecting multiple life forms around your location.");
        AudioManager.instance.PlaySound("Message");
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 0;
        UIManager.instance.SendMessageToPlayer("They're underwater Piranhas. Make quick work of them, or they'll make quick work of you");
        AudioManager.instance.PlaySound("Message");
        yield return new WaitForSecondsRealtime(4f);
        UIManager.instance.SendMessageToPlayer("Use the reflective surfaces and the items at your disposal, your lasers bounce off of them");
        AudioManager.instance.PlaySound("Message");
        yield return new WaitForSecondsRealtime(4f);
        UIManager.instance.SendMessageToPlayer("");
        Time.timeScale = 1;
    }

    IEnumerator PiranhaWinMessage()
    {
        UIManager.instance.SendMessageToPlayer("");
        yield return new WaitForSecondsRealtime(2f);
        UIManager.instance.SendMessageToPlayer("Great Job Explorer!");
        AudioManager.instance.PlaySound("Message");
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2f);
        UIManager.instance.SendMessageToPlayer("According to the sonar, the Crab Cave is East of your current location.");
        AudioManager.instance.PlaySound("Message");
        yield return new WaitForSecondsRealtime(3f);
        UIManager.instance.SendMessageToPlayer("Use some of the items to boost your stats!");
        AudioManager.instance.PlaySound("Message");
        yield return new WaitForSecondsRealtime(3f);
        UIManager.instance.SendMessageToPlayer("");
        Time.timeScale = 1;
    }

    IEnumerator BossMessage()
    {
        UIManager.instance.SendMessageToPlayer("");
        yield return new WaitForSecondsRealtime(4f);
        Time.timeScale = 0;
        UIManager.instance.SendMessageToPlayer("That must be the Giant Crab!");
        AudioManager.instance.PlaySound("Message");
        yield return new WaitForSecondsRealtime(3f);
        UIManager.instance.SendMessageToPlayer("Watch out for the Crabbies, they jump out on your face with extreme force.");
        AudioManager.instance.PlaySound("Message");
        yield return new WaitForSecondsRealtime(3f);
        UIManager.instance.SendMessageToPlayer("Also don't get too close to the Giant Crab, that might not end well");
        AudioManager.instance.PlaySound("Message");
        yield return new WaitForSecondsRealtime(3f);
        UIManager.instance.SendMessageToPlayer("");
        Time.timeScale = 1;
    }

    IEnumerator BossMessage2()
    {
        UIManager.instance.SendMessageToPlayer("");
        Time.timeScale = 0;
        UIManager.instance.SendMessageToPlayer("Good job on clearing the Crabbies!");
        AudioManager.instance.PlaySound("Message");
        yield return new WaitForSecondsRealtime(3f);
        UIManager.instance.SendMessageToPlayer("Time to take out the big one!");
        AudioManager.instance.PlaySound("Message");
        yield return new WaitForSecondsRealtime(2f);
        UIManager.instance.SendMessageToPlayer("");
        Time.timeScale = 1;
    }

    IEnumerator WinMessage()
    {
        UIManager.instance.SendMessageToPlayer("");
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 0;
        UIManager.instance.SendMessageToPlayer("Amazing! Now get the treasure and get out!");
        AudioManager.instance.PlaySound("Message");
        yield return new WaitForSecondsRealtime(3f);
        UIManager.instance.SendMessageToPlayer("");
        Time.timeScale = 1;
    }

    IEnumerator DodgeTime()
    {
        UIManager.instance.SendMessageToPlayer("");
        Time.timeScale = 0;
        UIManager.instance.SendMessageToPlayer("This Area doesn't seem to have much light.");
        AudioManager.instance.PlaySound("Message");
        yield return new WaitForSecondsRealtime(3f);
        UIManager.instance.SendMessageToPlayer("Use the front light to navigate yourself and don't hit random obstacles.");
        AudioManager.instance.PlaySound("Message");
        yield return new WaitForSecondsRealtime(3f);
        UIManager.instance.SendMessageToPlayer("");
        Time.timeScale = 1;
    }

    IEnumerator DodgeDone()
    {
        UIManager.instance.SendMessageToPlayer("");
        Time.timeScale = 0;
        UIManager.instance.SendMessageToPlayer("Great Work!");
        AudioManager.instance.PlaySound("Message");
        yield return new WaitForSecondsRealtime(2f);
        UIManager.instance.SendMessageToPlayer("Use some of these items to boost your stats");
        AudioManager.instance.PlaySound("Message");
        yield return new WaitForSecondsRealtime(3f);
        UIManager.instance.SendMessageToPlayer("");
        Time.timeScale = 1;
    }
}
