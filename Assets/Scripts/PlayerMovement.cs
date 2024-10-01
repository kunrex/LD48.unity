using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 movement;
    [SerializeField] private Vector2 speedVectors;

    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform light;

    [SerializeField] private float min, max, speedBuffTime;
    [SerializeField] private float maxSpeed, leftAngle = 110;
    [SerializeField] private ParticleSystem particleSystem;
    public float speed { get; private set; }

    private bool flipX = false, speedBuffed;

    [SerializeField] private AudioSource bubbles;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = rigidbody == null ? GetComponent<Rigidbody2D>() : rigidbody;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;

        movement = Vector2.Scale(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized, speedVectors);

        if(((movement.x > 0 && !flipX) || (movement.x < 0 && flipX)) && !particleSystem.isPlaying)
        {
            particleSystem.Play();
        }
        else if(((movement.x <= 0 && !flipX) || (movement.x >= 0 && flipX)) && particleSystem.isPlaying)
        {
            particleSystem.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            UIManager.instance.PauseGame();
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(movement);
        if (movement != Vector2.zero && !bubbles.isPlaying)
            bubbles.Play();

        movement = Vector2.zero;

        Vector2 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dir = (mousePosition - rigidbody.position).normalized;

        float angle = Vector2.Angle(mousePosition.x > rigidbody.position.x ? Vector2.right : -Vector2.right, dir);

        if (angle > min && angle < max)
        {
            float rot_z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);


            if (mousePosition.x > rigidbody.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);

                light.localRotation = Quaternion.Euler(0, 0, 110);
                flipX = false;
            }
            else if (mousePosition.x < rigidbody.position.x)
            {
                transform.localScale = new Vector3(-1, -1, 1);

                light.localRotation = Quaternion.Euler(0, 0, leftAngle);
                flipX = true;
            }
        }

        speed = rigidbody.velocity.magnitude;
        if (speed > maxSpeed)
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;

        UIManager.instance.SetSpeed(Mathf.RoundToInt(speed));
    }

    public IEnumerator SpeedBuff(float val)
    {
        if (!speedBuffed)
        {
            speedVectors += new Vector2(val, val);
            maxSpeed += val / 2;
            speedBuffed = true;

            UIManager.instance.SendMessageToPlayer("Speed increased for " + speedBuffTime + " seconds");
            yield return new WaitForSeconds(speedBuffTime);
            UIManager.instance.SendMessageToPlayer("");

            speedBuffed = false;
            maxSpeed -= val / 2;
            speedVectors -= new Vector2(val, val);
        }
    }
}
