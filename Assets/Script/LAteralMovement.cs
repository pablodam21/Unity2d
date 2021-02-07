using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LAteralMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 destination;
    public float time;
// Use this for initialization
    void Start ()
    {
        iTween.MoveTo (gameObject, iTween.Hash ("position", destination,
            "time", time,
            "easetype", iTween.EaseType.easeInOutSine,
            "looptype", iTween.LoopType.pingPong));
    }
}
