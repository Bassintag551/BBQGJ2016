using UnityEngine;
using System.Collections;

public class PowerupSpawner : MonoBehaviour {

    public float radius;

    public float timeBetweenSpawns;
    public float variance;

    private float nextSpawn;

    public Transform[] powerups;

    void Start()
    {
        CalculateNextSpawn();
    }

    void CalculateNextSpawn()
    {
        nextSpawn = timeBetweenSpawns + Random.Range(-variance, variance);
    }

    void FixedUpdate() {
        nextSpawn -= Time.deltaTime;
        if(nextSpawn <= 0)
        {
            float sin = Random.Range(-1f, 1f);
            float cos = Random.Range(-1f, 1f);

            float factor = Random.Range(0f, radius);

            Vector2 pos = new Vector2(sin, cos);
            pos.Normalize();
            pos = new Vector2(pos.x * factor, pos.y * factor);

            int index = Random.Range(0, powerups.Length - 1);
            Transform powerup = Instantiate(powerups[index]);
            powerup.position = pos;

            CalculateNextSpawn();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
