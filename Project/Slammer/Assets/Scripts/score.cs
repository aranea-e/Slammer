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
        }
    }
}
