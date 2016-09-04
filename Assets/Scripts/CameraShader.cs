using UnityEngine;
using System.Collections;

public class CameraShader : MonoBehaviour {

    private float delay;
    private bool shaded;

    private Material material;

    private Camera cam;

    void Start()
    {
        delay = 0;
        shaded = false;
        cam = GameManager.Instance.mainCamera;
    }

    public void setShader(Shader shader, float delay)
    {
        material = new Material(shader);
        shaded = true;
        this.delay = delay;
    }

    void FixedUpdate()
    {
        if (shaded)
        {
            if(delay <= 0)
            {
                shaded = false;
            }
            else
            {
                delay -= Time.deltaTime;
            }
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (!shaded)
        {
            Graphics.Blit(source, destination);
            return;
        }

        material.SetFloat("_Magnitude", Mathf.Min(1, delay));
        Graphics.Blit(source, destination, material);
    }
}
