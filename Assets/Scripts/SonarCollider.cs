using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SonarCollider : MonoBehaviour
{
    [SerializeField] private Light2D light;
    [SerializeField] private int playerMaskInt;

    [SerializeField] private float damageDelta;
    [SerializeField] private AudioSource thud;

    // Start is called before the first frame update
    void Start()
    {
        light = light == null ?transform.GetChild(0).GetComponent<Light2D>() : light;
        light.gameObject.SetActive(false);

        if(PlayerPrefs.GetInt("Hard", 0) == 0)
            damageDelta = 0.4f;
        else
            damageDelta = 0.6f;

        thud = thud == null ? GetComponent<AudioSource>() : thud;
    }

    public void Enable()
    {
        light.gameObject.SetActive(true);

        StartCoroutine(Disable());
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(1f);

        light.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == playerMaskInt)
        {
            HealthScript script = collision.gameObject.GetComponent<HealthScript>();

            script.TakeDamage(Mathf.RoundToInt(collision.relativeVelocity.magnitude * damageDelta));
        }

        thud.Play();
    }
}
