using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBoss : Boss
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce, force, groundRadius;
    public float shakeInensity, shakeTime;

    private BossHealth health;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private GameObject rockPrefab, groundCheckPos;
    public bool isGrounded { get; private set; }
    public float speed;

    [SerializeField] private AudioSource roar, thudSound;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<BossHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);

        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheckPos.transform.position, groundRadius, groundMask);

        if (!wasGrounded && isGrounded && !health.raged)
        {
            Game_Event_Manager.instance.CameraShake(shakeTime, shakeInensity);
            thudSound.Play();
        }
    }

    public override void Attack1()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public override void Attack2()
    {
        for(int i = 0; i < spawnPositions.Length;i++)
        {
            GameObject rock = Instantiate(rockPrefab, spawnPositions[i].position, Quaternion.identity);

            rock.GetComponent<Rigidbody2D>().AddForce((player.transform.position - spawnPositions[i].position).normalized * force, ForceMode2D.Impulse);
        }
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    public void PlayRoarSound()
    {
        roar.Play();
    }

    public void SetSpeed(float val)
    {
        speed = val;
    }
}
