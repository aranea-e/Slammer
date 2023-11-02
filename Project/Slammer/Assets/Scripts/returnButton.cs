using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnButton : MonoBehaviour {
    public void onPress() {
        audioManager.play("restart");
        transitions.Transition(0);
    }
}
