using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Game_Event_Manager : MonoBehaviour
{
    public static Game_Event_Manager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    [SerializeField] private Transform bossBattleLookAt, player;
    [SerializeField] private float bossBattleSize;
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private GameObject dodgeRock;
    [SerializeField] private Vector3 positionForDodgeRock;

    [SerializeField] private PiranhaManager piranhaManager;
    [SerializeField] private CrabBossManager crabBossManager;

    [SerializeField] private float hardJumpForce = 120;
    [SerializeField] private int newFirstWave = 15;
    [SerializeField] private CrabBoss boss;

    public delegate void GameEvent();

    public GameEvent CrabBossCheckPoint;
    public GameEvent PiranhasCheckPoint;
    public GameEvent CrabbieCheckPoint;
    public GameEvent CrabbiesDone;
    public GameEvent PiranhasDone;
    public GameEvent WinMessage;
    public GameEvent DodgeCheck;
    public GameEvent DodgeDone;

    // Start is called before the first frame update
    void Start()
    {
        CrabbieCheckPoint += CamEffects;
        DodgeDone += RepositionRock;

        if (PlayerPrefs.GetInt("Hard", 0) == 1)
        {
            crabBossManager.SetSpeeds(hardJumpForce);
            piranhaManager.SpawnMorePiranhas(newFirstWave);
            boss.SetSpeed(45);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SmoothTransition(float time)
    {
        yield return new WaitForSeconds(2f);

        Vector3 pos = bossBattleLookAt.position;
        float start = Time.timeSinceLevelLoad;
        float og = cam.m_Lens.OrthographicSize;

        cam.Follow = bossBattleLookAt;
        cam.LookAt = bossBattleLookAt;
        while (Time.timeSinceLevelLoad < start + time)
        {
            float completion = (Time.timeSinceLevelLoad - start) / time;
            cam.m_Lens.OrthographicSize = Mathf.Lerp(og, bossBattleSize, completion);
            bossBattleLookAt.position = Vector3.Lerp(player.position, pos, completion);
            yield return null;
        }
    }

    private void RepositionRock()
    {
        dodgeRock.transform.position = positionForDodgeRock;
        dodgeRock.GetComponent<AudioSource>().Play();
    }

    private void CamEffects()
    {
        StartCoroutine(SmoothTransition(2f));
        CrabBossCheckPoint = null;
        CrabbieCheckPoint = null;
    }

    public void PiranhaDone()
    {
        if (PiranhasDone != null)
            PiranhasDone();
    }

    public void PiranhaCheck()
    {
        if (PiranhasCheckPoint != null)
            PiranhasCheckPoint();
    }

    public void BossCheck()
    {
        if (CrabBossCheckPoint != null)
            CrabBossCheckPoint();
    }

    public void CrabbieCheck()
    {
        if (CrabbieCheckPoint != null)
            CrabbieCheckPoint();
    }

    public void DodgsCheck()
    {
        if (DodgeCheck != null)
            DodgeCheck();
    }

    public void DodgsDone()
    {
        if (DodgeDone != null)
            DodgeDone();
    }

    public void WinCheck()
    {
        if (WinMessage != null)
            WinMessage();
    }

    public void CrabbieDone()
    {
        if (CrabbiesDone != null)
            CrabbiesDone();
    }

    public void CameraShake(float time, float intensity)
    {
        StartCoroutine(CamShake(intensity, time));
    }

    IEnumerator CamShake(float inensity, float time)
    {
        while(time >= 0)
        {
            time -= Time.deltaTime;
            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = inensity;
            yield return null;
        }

        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
}
