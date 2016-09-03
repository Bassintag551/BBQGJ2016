using UnityEngine;
using System.Collections;

public class Trail : MonoBehaviour
{
    public int timeAlive = 5;

    private ArrayList waypoints;

    public float gapBetweenPoints = .1f;

    void Start()
    {
        waypoints = new ArrayList();
    }

    void FixedUpdate()
    {
        checkCollisions();
        handleWaypoints();
    }

    void checkCollisions()
    {
        if (waypoints.Count < 4)
            return;

        Waypoint wFirst = (Waypoint)waypoints[waypoints.Count - 1];
        Vector2 wFirstPos = wFirst.position;

        Vector2 pos = transform.position;

        Vector2 segment1 = new Vector2(wFirstPos.x - pos.x, wFirstPos.y - pos.y);

        for (int i = 0; i < waypoints.Count - 2; i++)
        {
            Waypoint w = (Waypoint)waypoints[i];
            Vector2 wPos = w.position;

            Waypoint wNext = (Waypoint)waypoints[i + 1];
            Vector2 wNextPos = wNext.position;

            Vector2 segment2 = new Vector2(wNextPos.x - wPos.x, wNextPos.y - wPos.y);

            float s, t;

            s = (-segment1.y * (pos.x - wPos.x) + segment1.x * (pos.y - wPos.y)) / (-segment2.x * segment1.y + segment1.x * segment2.y);
            t = (segment2.x * (pos.y - wPos.y) - segment2.y * (pos.x - wPos.x)) / (-segment2.x * segment1.y + segment1.x * segment2.y);

            if(s >= 0 && s <= 1 && t >= 0 && t <= 1)
            {
                waypoints.Clear();
                break;
            }
        }
    }

    void handleWaypoints()
    {
        float delta = Time.deltaTime;

        ArrayList toRemove = new ArrayList();

        for (int i = 0; i < waypoints.Count; i++)
        {
            Waypoint w = (Waypoint)waypoints[i];
            if (w.lived > timeAlive)
            {
                toRemove.Add(w);
            }
            w.lived += delta;
        }

        foreach (Waypoint w in toRemove)
        {
            waypoints.Remove(w);
        }

        if(waypoints.Count < 1)
        {
            waypoints.Add(new Waypoint(transform.position));
        }
        else
        {
            Waypoint last = (Waypoint)waypoints[waypoints.Count - 1];
            if(Vector2.Distance(transform.position, last.position) > gapBetweenPoints)
            {
                waypoints.Add(new Waypoint(transform.position));
            }
        }
    }

    void OnDrawGizmos()
    {
        if (waypoints == null)
            return;
        Gizmos.color = Color.red;
        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            Waypoint current = (Waypoint)waypoints[i];
            Waypoint next = (Waypoint)waypoints[i + 1];
            Gizmos.DrawLine(current.position, next.position);
        }
    }

    class Waypoint
    {
        public float lived;
        public Vector2 position;

        public Waypoint(Vector2 pos)
        {
            this.position = pos;
        }
    }
}
