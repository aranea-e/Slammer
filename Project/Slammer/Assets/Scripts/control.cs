using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour {
    public Rigidbody2D rb;
    public float mod;

    void FixedUpdate() {
        Vector2 v = new Vector2();
        if (Input.GetKey("w")) v.y += 1;
        if (Input.GetKey("a")) v.x -= 1;
        if (Input.GetKey("s")) v.y -= 1;
        if (Input.GetKey("d")) v.x += 1;

        if (!(v.x == 0 || v.y == 0)) {
            v *= Mathf.Sqrt(2) / 2;
        }

        v *= mod;
        rb.velocity = v;

        foreach (slime s in FindObjectsOfType<slime>()) {
            if (Vector2.Distance(s.transform.position, transform.position) < 0.25f) {
                Lose();
            }
        }
    }

    void Lose() {
        Debug.Log("L");
    }
}
