using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

    public GameObject aliveObject;
    public GameObject deadObject;

    public void setAlive(bool alive)
    {
        aliveObject.SetActive(alive);
        deadObject.SetActive(!alive);
    }
}
