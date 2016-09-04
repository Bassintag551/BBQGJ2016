using UnityEngine;
using System.Collections;

public class Chorizo : MonoBehaviour {

    private bool activated;

    public Shader shader;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerMove>())
        {
            Destroy(gameObject);
            GameManager.Instance.mainCamera.GetComponent<CameraShader>().setShader(shader, 5);
        }

        if(collider.GetComponent<Cutout>())
            Destroy(gameObject);
    }
    
    void FixedUpdate()
    {
        if (!activated && GetComponent<Fall>() && GetComponent<Fall>().ready)
        {
            activated = true;
            Destroy(gameObject, .1f);
        }
    }
}
