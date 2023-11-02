using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score : MonoBehaviour {
    public int s;
    public int hs;
    public TextMeshProUGUI t;
    public TextMeshProUGUI menus;
    public TextMeshProUGUI menuh;
    public TextMeshProUGUI ht;
    public bool first = true;
    public bool newHS;

    void Awake() {
        if (Object.FindObjectsOfType<score>().Length > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update() {
        FindText();
        if (t != null) {
            t.text = s.ToString();
        }
        if (menus != null) {
            menus.text = "Score: " + s.ToString();
        }
        if (menuh != null) {
            menuh.text = "High Score: " + hs.ToString();
            if (newHS) {
                menuh.text = "";
            }
        }
        if (ht != null) {
            if (first) {
                ht.text = "";
            } else {
                if (newHS) {
                    ht.text = "New High Score!";
                } else {
                    ht.text = "High Score: " + hs.ToString();
                }
            }
        }
    }

    void FindText() {
        foreach (TextMeshProUGUI temp in FindObjectsOfType<TextMeshProUGUI>()) {
            if (temp.text == "0") {
                t = temp;
            }
            if (temp.text == "1") {
                menus = temp;
            }
            if (temp.text == "2") {
                menuh = temp;
            }
            if (temp.text == "3") {
                ht = temp;
            }
        }
    }
}
