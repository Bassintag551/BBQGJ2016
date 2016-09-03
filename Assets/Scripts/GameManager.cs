using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public Camera mainCamera;

    public int players { private set; get; }

    public bool paused { set; get; }

    public BoardManager boardManager { private set; get; }

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
	}

    public void StartGame(int numPlayers)
    {
        this.players = numPlayers;
        
    }
}
