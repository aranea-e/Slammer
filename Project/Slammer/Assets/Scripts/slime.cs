using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime : MonoBehaviour {
    public Rigidbody2D rb;
    public float t;
    public float friction;
    public float distance;
    public bool proj;
    public bool capt;
    public float rad;
    public int streak = 0;
    public float spdmod;
    public Animator a; 

    void Update() {
        a.SetBool("Left", transform.position.x > FindObjectOfType<control>().transform.position.x);
        t += Time.deltaTime * spdmod;
        float speed = Mathf.Sqrt((rb.velocity.x * rb.velocity.x) + (rb.velocity.y * rb.velocity.y));
        if (t / 3.0f > 1.0f && !proj && !capt && speed < 0.1f) {
            t %= 3;
            t += Random.Range(0.0f, 2.0f);
            Move();
        }
    }

    void FixedUpdate() {
        if (proj) {
            foreach (slime s in FindObjectsOfType<slime>()) {
                if (s != this && Vector2.Distance(s.transform.position, transform.position) < rad) {
                    Destroy(s.gameObject);
                    audioManager.play("kill");
                    streak++;
                    if (!(FindObjectOfType<transitions>().transitioning || FindObjectOfType<transitions>().detransitioning)) {
                        FindObjectOfType<score>().s += 100 * streak;
                        if (FindObjectOfType<score>().s > FindObjectOfType<score>().hs) {
                            FindObjectOfType<score>().hs = FindObjectOfType<score>().s;
                            FindObjectOfType<score>().newHS = true;
                        }
                    }
                }
            }
        }

        rb.velocity *= friction;
        float speed = Mathf.Sqrt((rb.velocity.x * rb.velocity.x) + (rb.velocity.y * rb.velocity.y));
        if (proj == true && speed <= 2f) {
            proj = false;
            streak = 0;
        }
    }

    void Move() {
        a.SetTrigger("Jump");
        audioManager.play("jump");
        Vector2 direction = FindObjectOfType<control>().transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x);
        angle += Random.Range(-0.3f, 0.3f);
        Vector2 mods = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        rb.velocity = mods * distance;
    }
}
