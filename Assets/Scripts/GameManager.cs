using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public Camera mainCamera;

    public GameObject[] players { private set; get; }

    public Vector2[] spawnpoints = new Vector2[4]; 

    public bool paused { set; get; }

    public BoardManager boardManager { private set; get; }

    public Status status { private set; get; }

    public enum Status
    {
        COUNTDOWN,
        STARTED,
        ENDING,
        PAUSED
    }

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
    }

	void Start () {
        boardManager = GetComponent<BoardManager>();
        status = Status.PAUSED;
	}

    public void StartGame(Transform[] players)
    {
        boardManager.setupPizza();

        this.players = new GameObject[players.Length];

        for(int i = 0; i < players.Length; i++)
        {
            if(players[i] != null)
            {
                Transform t = Instantiate(players[i]);
                t.GetComponent<PlayerMove>().joystickId = i + 1;
                t.position = spawnpoints[i];
                t.name = "Player " + i;
                this.players[i] = t.gameObject;
            }
        }
    }

    public void KillPlayer(int id)
    {
        GameObject player = players[id];
        Destroy(player);
        players[id] = null;
    }
}
