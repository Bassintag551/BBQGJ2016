using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Animations;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public Transform samplePlayer;

    public Camera mainCamera;

    public GameObject[] players { private set; get; }

    public Vector2[] spawnpoints = new Vector2[4];

    public bool paused { set; get; }

    public BoardManager boardManager { private set; get; }

    public PowerupSpawner powerupSpawner { private set; get; }

    public GameObject HUD;

    private bool started;

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
        powerupSpawner = GetComponent<PowerupSpawner>();
        powerupSpawner.gameObject.SetActive(false);
	}

    public void StartGame(AnimatorController[] players)
    {
        started = true;

        boardManager.setupPizza();

        HUD.SetActive(true);
        powerupSpawner.gameObject.SetActive(true);

        this.players = new GameObject[players.Length];

        for(int i = 0; i < players.Length; i++)
        {
            if(players[i] != null)
            {
                Transform t = Instantiate(samplePlayer);
                t.GetComponent<PlayerMove>().joystickId = i + 1;
                t.GetComponent<Animator>().runtimeAnimatorController = players[i];
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

        HUD.GetComponent<HudController>().setAlive(false, id);
    }

    void FixedUpdate()
    {
        if(started)
        for(int i = 0; i < players.Length; i++)
        {
            if (!players[i]) continue;
            Transform player = players[i].transform;
            if(Vector2.Distance(transform.position, player.position) > boardManager.pizzaRadius)
            {
                KillPlayer(i);
            }
        }
    }
}
