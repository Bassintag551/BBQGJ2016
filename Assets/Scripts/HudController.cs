using UnityEngine;
using System.Collections;

public class HudController : MonoBehaviour {

    public HealthController[] playerHealthBarControllers;

    public void setAlive(bool alive, int id)
    {
        playerHealthBarControllers[id].setAlive(alive);
    }
}
