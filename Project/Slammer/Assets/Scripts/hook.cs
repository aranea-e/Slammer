using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hook : MonoBehaviour {
    public float rad;
    public Rigidbody2D rb;
    public string state = "retracted";
    public Vector2 target;
    public float friction;
    public float distance;
    public slime hooked;
    public float pullmod;
    public SpriteRenderer sprite;
    public Vector3 playerPos;

    void Update() {
        sprite.enabled = state != "retracted";
        playerPos = FindObjectOfType<control>().transform.position - new Vector3 (0.25f, 0.25f, 0);
        switch(state) {
            case "retracted":
                Retracted();
                break;
            case "throw":
                Throw();
                break;
            case "hooked":
                Hooked();
                break;
            case "idle":
                Idle();
                break;
            case "retract":
                Retract();
                break;
        }
    }

    void FixedUpdate() {
        switch (state) {
            case "throw":
            case "idle":
                rb.velocity *= friction;
                break;
        }
    }

    void Retracted() {
        rb.velocity = Vector2.zero;
        if (Input.GetMouseButtonDown(0)) {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = target - (Vector2) transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x);
            Vector2 mods = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            rb.velocity = mods * distance;
            state = "throw";
            audioManager.play("throw");
        } else {
            transform.position = playerPos;
        }
    }

    void Throw() {
        if (!Input.GetMouseButton(0)) {
            state = "retract";
            return;
        }

        foreach (slime s in FindObjectsOfType<slime>()) {
            if (Vector2.Distance(s.transform.position, transform.position) < rad) {
                hooked = s;
                hooked.capt = true;
            }
            if (hooked != null) {
                state = "hooked";
                transform.position = s.transform.position;
                rb.velocity = Vector2.zero;
                return;
            }
        }

        float speed = Mathf.Sqrt((rb.velocity.x * rb.velocity.x) + (rb.velocity.y * rb.velocity.y));
        if (speed <= 0.5) {
            state = "idle";
        }
    }

    void Hooked() {
        transform.position = hooked.transform.position;
        if (!Input.GetMouseButton(0)) {
            Vector2 direction = playerPos - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x);
            Vector2 mods = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            hooked.rb.velocity = mods * distance * pullmod 
                * Vector2.Distance(playerPos, transform.position);
            hooked.capt = false;
            hooked.proj = true;
            hooked = null;
            state = "retract";
            audioManager.play("pull");
            return;
        }
    }

    void Idle() {
        if (!Input.GetMouseButton(0)) {
            state = "retract";
        }
    }

    void Retract() {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = playerPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x);
        Vector2 mods = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        rb.velocity = mods * distance;
        
        if (Vector2.Distance(playerPos, transform.position) < rad) {
            state = "retracted";
        }
    }
}
