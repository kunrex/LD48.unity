using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    [SerializeField] private float maxRange, speed;
    [SerializeField] private Transform effect;
    bool sonarActive = false;
    private float range;
    private List<Collider2D> alreadyCollided = new List<Collider2D>();
    [SerializeField] private Collider2D[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            sonarActive = !sonarActive;

        if(sonarActive)
        {
            effect.gameObject.SetActive(true);

            range += Time.deltaTime * speed;
            if(range >= maxRange)
            {
                range = 0f;
                alreadyCollided = new List<Collider2D>();
            }

            effect.localScale = new Vector3(range, range);

            RaycastHit2D[] rays = Physics2D.CircleCastAll(effect.transform.position, range, Vector2.zero);
            foreach(RaycastHit2D collider in rays)
            {
                Debug.Log("test");
                /*if (collider != null)
                {
                    Debug.Log("test");
                    SonarCollider sonar = collider.GetComponent<SonarCollider>();

                    if (sonar != null && !alreadyCollided.Contains(collider))
                    {
                        sonar.Enable();
                        alreadyCollided.Add(collider);
                    }
                }*/
            }
        }
        else
        {
            effect.gameObject.SetActive(false);
            alreadyCollided = new List<Collider2D>();
        }
    }
}
