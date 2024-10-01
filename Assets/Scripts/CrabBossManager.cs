using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBossManager : MonoBehaviour
{
    [SerializeField] private CrabBoss boss;
    [SerializeField] private List<Crabbie> crabbies = new List<Crabbie>();
    private int numberOfCrabbiesToKill;
    public bool crabbBossbeaten { get; private set; }

    [SerializeField] private Transform player;
    [SerializeField] private GameObject[] blockers;

    [SerializeField] private AudioSource[] NOOOOOOOO;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
            crabbies.Add(child.GetComponent<Crabbie>());

        foreach (Crabbie crabbie in crabbies)
            crabbie.gameObject.SetActive(false);

        numberOfCrabbiesToKill = transform.childCount;
        boss.gameObject.SetActive(false);

        Game_Event_Manager.instance.CrabbieCheckPoint += EnableCrabbies;
        Game_Event_Manager.instance.CrabBossCheckPoint += EnableBoss;
    }

    public void SetSpeeds(float val)
    {
        foreach(Transform crab in transform)
        {
            crab.GetComponent<Crabbie>().SetJumpForce(val);
        }
    }

    public void ChangeNumber(int val)
    {
        if (hasStarted) return;

        numberOfCrabbiesToKill -= val;
        UIManager.instance.SetCrabsToKill(numberOfCrabbiesToKill);

        if(numberOfCrabbiesToKill == 5)
            boss.GetComponent<BossHealth>().Rage();

        if (numberOfCrabbiesToKill == 0 || transform.childCount == 0)
        {
            Game_Event_Manager.instance.CrabbieDone();
        }
    }

    public void DeadBoss()
    {
        crabbBossbeaten = true;
        UIManager.instance.SetCrabUI(false);
        Game_Event_Manager.instance.WinCheck();

        foreach (AudioSource source in NOOOOOOOO)
            source.Play();
    }

    private void EnableBoss()
    {
        StartCoroutine(Enable_Boss());
    }

    IEnumerator Enable_Boss()
    {
        yield return new WaitForSeconds(2f);

        boss.gameObject.SetActive(true);
        UIManager.instance.SetCrabUI(true);

        boss.PlayRoarSound();
    }
    private bool hasStarted;

    private void EnableCrabbies()
    {
        StartCoroutine(_EnableCrabbies());
    }

    IEnumerator _EnableCrabbies()
    {
        yield return new WaitForSeconds(5f);

        AudioManager.instance.StopSound("Music");
        AudioManager.instance.PlaySound("Boss");

        foreach (Crabbie crabbie in crabbies)
            if (crabbie != null)
                crabbie.gameObject.SetActive(true);

        foreach (GameObject blocker in blockers)
            blocker.SetActive(true);

        StartCoroutine(CrabbieAttack());
        ChangeNumber(0);
    }

    public IEnumerator CrabbieAttack()
    {
        if (crabbies.Count != 0 && !hasStarted)
        {
            hasStarted = true;
            yield return new WaitForSeconds(1.5f);

            int index = 0; float minDistance = Mathf.Infinity;
            for(int i =0;i<crabbies.Count;i++)
            {
                if (crabbies[i] != null)
                {
                    float distance = Vector2.Distance(crabbies[i].transform.position, player.transform.position);
                    if (distance < minDistance)
                    {
                        index = i;
                        minDistance = distance;
                    }
                    else if (distance > minDistance)
                        break;
                }
            }

            if (crabbies[index] != null)
            {
                Crabbie crabbie = crabbies[index];
                crabbie._Attack();
            }

            crabbies.RemoveAt(index);
            hasStarted = false;
        }
    }
}
