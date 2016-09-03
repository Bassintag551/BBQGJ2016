using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour {
    
    public Texture2D[] charactersIcons;
    public Transform[] characters;
    private bool[] selected;
    public RawImage[] selectors;

	// Use this for initialization
	void Start () {
        selected = new bool[4];
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 4; i++)
        {
            bool b = ControllerManager.Instance.Active[i];
            if (!b)
                continue;
            else if (ControllerManager.Instance.A[i])
            {
                selected[i] = true;
            }
        }
	}
}
