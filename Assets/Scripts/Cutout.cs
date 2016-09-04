using UnityEngine;
using System.Collections;

public class Cutout : MonoBehaviour {

    public PlayerMove owner { get; set; }
    
    public float lived { get; private set; }

    void Start()
    {
        lived = 0;
    }

    void FixedUpdate()
    {
        lived += Time.deltaTime;
    }
}
