using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class transitions : MonoBehaviour {
    public int con;
    public float time;
    public bool transitioning;
    public bool detransitioning;
    public GameObject transitionBlock;
    public GameObject[][] blocks;
    public GameObject blockParent;
    public Vector2[][] blockTargets;
    public Vector2[][] blockInit;
    public TextMeshProUGUI text;
    public Vector2 textInit;
    public string[] tips;
    public string tip;

    void Awake() {
        if (Object.FindObjectsOfType<transitions>().Length > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update() {
        if (transitioning) {
            time += Time.deltaTime;
            for (int x = 0; x < blocks.Length; x++) {
                for (int y = 0; y < blocks[x].Length; y++) {
                    float delay = (((17 - x) + (40 - y * 4)) / 57f);
                    float adjusted = time - delay;
                    if (adjusted < 0f) {
                        adjusted = 0f;
                    }
                    if (adjusted > 0.5f) {
                        adjusted = 0.5f;
                    }
                    float tPercent = adjusted / 0.5f;
                    float iPercent = (0.5f - adjusted) / 0.5f;
                    Vector2 target = blockInit[x][y] * iPercent + blockTargets[x][y] * tPercent;
                    blocks[x][y].GetComponent<RectTransform>().anchoredPosition = target;
                }
            }
            text.text = "";
            if (time > 1f) {
                switch (con) {
                    case 0:
                        text.text = tip;
                        break;
                    case 1:
                        text.text = tip;
                        break;
                }
                float adjusted = time - 1f;
                if (adjusted > 0.5f) {
                    adjusted = 0.5f;
                }
                float percent = (0.5f - adjusted) / 0.5f;
                text.GetComponent<RectTransform>().anchoredPosition = textInit * percent;
            }
            if (time > 3f) {
                transitioning = false;
                detransitioning = true;
                time = 2f;
                switch (con) {
                    case 0:
                        SceneManager.LoadScene(0);
                        FindObjectOfType<score>().s = 0;
                        break;
                    case 1:
                        SceneManager.LoadScene(1);
                        break;
                }
            }
        }
        if (detransitioning) {
            time -= Time.deltaTime;
            for (int x = 0; x < blocks.Length; x++) {
            for (int y = 0; y < blocks[x].Length; y++) {
                float delay = (((17 - x) + (40 - y * 4)) / 57f);
                float adjusted = time - delay;
                if (adjusted < 0f) {
                    adjusted = 0f;
                }
                if (adjusted > 0.5f) {
                    adjusted = 0.5f;
                }
                float tPercent = adjusted / 0.5f;
                float iPercent = (0.5f - adjusted) / 0.5f;
                Vector2 target = blockInit[x][y] * iPercent + blockTargets[x][y] * tPercent;
                blocks[x][y].GetComponent<RectTransform>().anchoredPosition = target;
            }
            }
            text.text = "";
            if (time > 1f) {
                switch (con) {
                    case 0:
                        text.text = tip;
                        break;
                    case 1:
                        text.text = tip;
                        break;
                }
                float adjusted = time - 1f;
                if (adjusted > 0.5f) {
                    adjusted = 0.5f;
                }
                float percent = (0.5f - adjusted) / 0.5f;
                text.GetComponent<RectTransform>().anchoredPosition = textInit * percent;
            }
            if (time <= 0f) {
                time = 0f;
                detransitioning = false;
                foreach (GameObject[] arr in blocks)
                {
                    foreach (GameObject block in arr) {
                        Destroy(block);
                    }
                }
            }
        }
    }

    public static void Transition(int c) {
        if (!(Object.FindObjectOfType<transitions>().transitioning || Object.FindObjectOfType<transitions>().detransitioning)) {
            Object.FindObjectOfType<transitions>().trans(c);
        }
    }

    void trans(int c) {
        this.con = c;
        time = 0f;
        transitioning = true;
        tip = tips[Random.Range(0, tips.Length)];
        blocks = new GameObject[17][];
        blockTargets = new Vector2[17][];
        blockInit = new Vector2[17][];
        for (int i = 0; i < blocks.Length; i++) {
            blocks[i] = new GameObject[11];
            blockTargets[i] = new Vector2[11];
            blockInit[i] = new Vector2[11];
        }
        for (int x = 0; x < blocks.Length; x++) {
            for (int y = 0; y < blocks[x].Length; y++) {
                blocks[x][y] = Instantiate(transitionBlock, Vector3.zero, Quaternion.identity, blockParent.transform);
                blockInit[x][y] = new Vector2(50 * x, -100f);
                blockTargets[x][y] = new Vector2(50 * x, 50 * y);
                blocks[x][y].GetComponent<RectTransform>().anchoredPosition = blockInit[x][y];
            }
        }
    }

    [ContextMenu("test")]
    public void test() {
        Transition(0);
    }
}
