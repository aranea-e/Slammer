using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour {
    public Animator a;  
    
    public void manualUpdate(Vector2 veloc) {
        a.SetFloat("veloc x", veloc.x);
        a.SetFloat("veloc y", veloc.y);
        a.SetBool("stop x", veloc.x == 0);
        a.SetBool("stop y", veloc.y == 0);
    }
}
