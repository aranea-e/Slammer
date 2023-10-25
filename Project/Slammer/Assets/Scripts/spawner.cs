using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {
    public GameObject prefab;
    public List<slime> slimes;
    public float timer;
    public float secondTimer;
    public Vector2[] poss;

    void Start() {
        slimes = new List<slime>();
    }

    void Update() {
        timer += Time.deltaTime;
        secondTimer += Time.deltaTime;
        if (secondTimer > 1.0f) {
            secondTimer %= 1;
            for (int i = 0; i < Mathf.Ceil(timer / 60); i++) {
                float chance = timer / (60 * (i + 1));
                if (slimes.Count < 5) { chance *= 2; }
                if (slimes.Count < 1) { chance *= 2; }
                if (slimes.Count < 25 && Random.Range(0.0f, 1.0f) < chance) {
                    slimes.Add(Spawn());
                }
            }
        }
    }

    slime Spawn() {
        return Instantiate(prefab, poss[Random.Range(0, 2)], Quaternion.identity).GetComponent<slime>();
    }
}
