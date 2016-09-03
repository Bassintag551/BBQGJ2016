using UnityEngine;

public class CharacterSelect : MonoBehaviour {

    public PlayerSelector[] characterAvailable;
    public int[] characterIndex;
    public GameObject characterSelected;

	// Use this for initialization
	void Start () {
       characterIndex = new int[4];
	}
	
	// Update is called once per frame
	void Update () {
        foreach(bool b in ControllerManager.Instance.Active)
        {
            if (!b)
                continue;

        }
	}
}
