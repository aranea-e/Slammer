using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chain : MonoBehaviour {
    public GameObject prefab;
    public List<GameObject> links;
    public float linksLength = Mathf.Sqrt(2) * 3 / 16;
    public control player;

    void Update() {
        for (int i = 0; i < links.Count; i++) {
            Destroy(links[i]);
        }
        links.Clear();
        
        float distance = Vector3.Distance(player.transform.position, transform.position);
        int numLinks = (int) Mathf.Ceil(distance / linksLength);
        Vector2 direction = player.transform.position - transform.position;
        Vector3 rotation = new Vector3(0, 0, (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 135f);
        Quaternion q = new Quaternion();
        q.eulerAngles = rotation;
        transform.rotation = q;

        for (int i = 0; i < numLinks; i++) {
            float p = i / (float) numLinks;
            links.Add(Instantiate(prefab, (player.transform.position * (1 - p)) + (transform.position * p), q));
            links[i].transform.parent = this.transform;
        }
    }
}
