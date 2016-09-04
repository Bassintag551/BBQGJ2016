using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Animations;

public class CharacterSelect : MonoBehaviour {
    public AnimatorController[] characters;
    private bool[] wasActive;
    public RawImage[] selectors;

	void Start () {
        wasActive = new bool[4];
        foreach(RawImage img in selectors)
        {
            img.color = Color.black;
        }
	}

	void Update () {
        for (int i = 0; i < 4; i++)
        {
            bool b = ControllerManager.Instance.Active[i];
            if (!b)
                continue;
            else if (!wasActive[i])
            {
                selectors[i].color = Color.white;
                wasActive[i] = true;
            }

            if (ControllerManager.Instance.Y[i])
            {
                AnimatorController[] players = new AnimatorController[4];
                for(int j = 0; j < players.Length; j++)
                {
                    if (wasActive[j])
                    {
                        players[j] = characters[j];
                    }
                }

                this.gameObject.SetActive(false);
                GameManager.Instance.StartGame(players);
                return;
            }
        }
	}
}
