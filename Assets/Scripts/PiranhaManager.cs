using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PiranhaManager : MonoBehaviour
{
    [SerializeField] private List<Piranha> piranhas = new List<Piranha>();
    [SerializeField] private GameObject[] destroyables;
    private int numberOfPiranhasToKill;
    [SerializeField] private GameObject rockToPreventPlayerGoingBack;
    [SerializeField] private GameObject blocker;

    [SerializeField] private int numToSpawn1;
    [SerializeField] private Light2D light;
    private int nextSpawnNum;

    [SerializeField] private Transform extraPiranhasList;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
            piranhas.Add(child.GetComponent<Piranha>());

        foreach(Piranha piranha in piranhas)
        {
            piranha.gameObject.SetActive(false);
        }

        Game_Event_Manager.instance.PiranhasCheckPoint += EnablePiranhas;
    }

    public void SpawnMorePiranhas(int firstSpawnvalue)
    {
        List<Piranha> secondaryPiranhas = new List<Piranha>();
        foreach (Transform piranha in extraPiranhasList)
        {
            secondaryPiranhas.Add(piranha.GetComponent<Piranha>());
        }

        foreach (Piranha secondary in secondaryPiranhas)
            secondary.transform.SetParent(transform);

        piranhas.AddRange(secondaryPiranhas);

        numToSpawn1 = firstSpawnvalue;
    }

    private void EnablePiranhas()
    {
        StartCoroutine(Enable());
        StartCoroutine(EnableLight());
    }

    IEnumerator Enable()
    {
        yield return new WaitForSeconds(2f);

        UIManager.instance.SetPiranhaUI(true);
        rockToPreventPlayerGoingBack.SetActive(true);

        numberOfPiranhasToKill = transform.childCount;
        UIManager.instance.SetPiranhasToKill(numberOfPiranhasToKill);

        int numSpawned = 0;
        foreach (Piranha piranha in piranhas)
        {
            if (piranha != null)
            {
                piranha.gameObject.SetActive(true);
                numSpawned++;

                if (numSpawned == numToSpawn1)
                    break;
            }
        }
        nextSpawnNum = numberOfPiranhasToKill - numSpawned;
    }

    public void ChangeNumber(int val)
    {
        numberOfPiranhasToKill -= val;
        UIManager.instance.SetPiranhasToKill(numberOfPiranhasToKill);

        if(numberOfPiranhasToKill == nextSpawnNum)
        {
            StartCoroutine(SpawnSecondWave());
        }
        else if (numberOfPiranhasToKill == 0)
        {
            Game_Event_Manager.instance.PiranhaDone();
            UIManager.instance.SetPiranhaUI(false);
            blocker.GetComponent<Destroyable>().Destroy();
        }
    }

    IEnumerator SpawnSecondWave()
    {
        yield return new WaitForSeconds(3f);

        foreach (Piranha piranha in piranhas)
        {
            if (piranha != null)
            {
                piranha.gameObject.SetActive(true);
            }
        }
    }

    IEnumerator EnableLight()
    {
        yield return new WaitForSeconds(1f);

        while(light.intensity < 1)
        {
            light.intensity += Time.deltaTime;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
