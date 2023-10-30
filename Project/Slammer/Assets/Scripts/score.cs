using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score : MonoBehaviour {
    public int s;
    public TextMeshProUGUI t; 

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        t.text = s.ToString();
    }
}
