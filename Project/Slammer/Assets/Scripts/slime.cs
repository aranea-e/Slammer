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

    void Update() {
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
                    streak++;
                    FindObjectOfType<score>().s += 100 * streak;
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
        Vector2 direction = FindObjectOfType<control>().transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x);
        angle += Random.Range(-0.3f, 0.3f);
        Vector2 mods = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        rb.velocity = mods * distance;
    }
}
