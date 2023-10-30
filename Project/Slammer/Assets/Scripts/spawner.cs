using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {
    public GameObject prefab;
    public float timer;
    public float secondTimer;
    public Vector2[] poss;

    void Update() {
        slime[] slimes = FindObjectsOfType<slime>();
        timer += Time.deltaTime;
        secondTimer += Time.deltaTime;
        if (secondTimer > 1.0f) {
            secondTimer %= 1;
            for (int i = 0; i < Mathf.Ceil(timer / 60); i++) {
                float chance = timer / (60 * (i + 1));
                if (slimes.Length < 5) { chance *= 2; }
                if (slimes.Length < 1) { chance *= 2; }
                if (slimes.Length > 25) { chance /= 2; }
                if (slimes.Length > 50) { chance /= 2; }
                if (slimes.Length > 100) { chance = 0; }
                if (Random.Range(0.0f, 1.0f) < chance) {
                    Spawn().spdmod = 0.8f + Random.Range(0.0f, timer / 180.0f);
                }
            }
        }
    }

    slime Spawn() {
        return Instantiate(prefab, poss[Random.Range(0, 2)], Quaternion.identity).GetComponent<slime>();
    }
}
