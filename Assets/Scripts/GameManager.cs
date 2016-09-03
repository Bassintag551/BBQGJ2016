using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public Camera mainCamera;

    public GameObject[] players { private set; get; }

    public Transform[] spawnpoints = new Transform[4]; 

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
        this.players = new GameObject[players.Length];

        for(int i = 0; i < players.Length; i++)
        {
            this.players[i] = Instantiate(players[i]).gameObject;
        }
    }
}
