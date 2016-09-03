using UnityEngine;
using System.Collections;

public class ControllerManager : MonoBehaviour
{

    public bool[] Active { get; private set; }

    public bool[] A { get; private set; }
    public bool[] B { get; private set; }
    public bool[] X { get; private set; }
    public bool[] Y { get; private set; }

    public bool[] L { get; private set; }
    public bool[] R { get; private set; }

    public float[] Horizontal { get; private set; }
    public float[] Vertical { get; private set; }

    public float[] AimHorizontal { get; private set; }
    public float[] AimVertical { get; private set; }

    public static ControllerManager Instance;

    void Start()
    {
        if (Instance != null) Destroy(this.gameObject);
        Instance = this;

        DontDestroyOnLoad(this);

        Active = new bool[4];
        A = new bool[4];
        B = new bool[4];
        X = new bool[4];
        Y = new bool[4];
        L = new bool[4];
        R = new bool[4];
        Horizontal = new float[4];
        Vertical = new float[4];
        AimHorizontal = new float[4];
        AimVertical = new float[4];

        Active[0] = true;
    }

    void Update()
    {
        Debug.Log(Active[0] + " " + Active[1]);

        for (int i = 0; i < 2; i++)
        {
            int id = i + 1;

            bool A = Input.GetButton("joystick " + id + " button 0");

            if (!Active[i])
            {
                if (A)
                {
                    Active[i] = true;
                }
                else
                {
                    continue;
                }
            }

            this.A[i] = A;
            this.B[i] = Input.GetButtonDown("joystick " + id + " button 1");
            this.X[i] = Input.GetButtonDown("joystick " + id + " button 2");
            this.Y[i] = Input.GetButtonDown("joystick " + id + " button 3");

            this.Horizontal[i] = Input.GetAxis("Horizontal " + id);
            this.Vertical[i] = Input.GetAxis("Vertical " + id);

            this.AimHorizontal[i] = Input.GetAxis("Aim Horizontal " + id);
            this.AimVertical[i] = Input.GetAxis("Aim Vertical " + id);

            float triggers = Input.GetAxis("Triggers " + id);
            this.L[i] = triggers < 0;
            this.R[i] = triggers > 0;
        }
    }
}
