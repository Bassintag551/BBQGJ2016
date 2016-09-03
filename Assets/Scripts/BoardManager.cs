using UnityEngine;
using System.Collections;


public class BoardManager : MonoBehaviour {

    public float pizzaRadius { private set; get; }

    public GameObject pizzaPrefab;

    private Texture2D mask;

    private Material maskMaterial;

    private GameManager gameManager;

    void Start() {
        GameManager gameManager = GetComponent<GameManager>();
        GameObject pizza = Instantiate(pizzaPrefab);
        pizza.name = "Pizza";

        SpriteRenderer pizzaSpriteRenderer = pizza.GetComponent<SpriteRenderer>();
        Sprite pizzaSprite = pizza.GetComponent<SpriteRenderer>().sprite;

        pizzaRadius = pizzaSprite.bounds.size.x / 2;

        float pixelPerUnit = pizzaSprite.pixelsPerUnit;
        float camHeight = gameManager.mainCamera.orthographicSize * 2;

        mask = new Texture2D((int)(pixelPerUnit * camHeight), (int)(pixelPerUnit * camHeight));
        Vector2 center = new Vector2(mask.width / 2, mask.height / 2);
        FillTexture(ref mask, Color.clear);
        FillCircle(ref mask, (int)center.x, (int)center.y, (int)(pizzaRadius * pixelPerUnit), Color.black);
        mask.Apply();

        Texture2D pizzaTexture = pizzaSprite.texture;

        maskMaterial = pizzaSpriteRenderer.material;

        maskMaterial.SetTexture("_Alpha", mask);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pizzaPrefab.transform.position, pizzaRadius);
    }

    void FillTexture(ref Texture2D texture, Color color)
    {
        Color[] pixels = texture.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = color;
        }
        texture.SetPixels(pixels);
    }

    void FillCircle(ref Texture2D tex, int cx, int cy, int r, Color col)
    {
        int x, y, px, nx, py, ny, d;

        for (x = 0; x <= r; x++)
        {
            d = (int)Mathf.Ceil(Mathf.Sqrt(r * r - x * x));
            for (y = 0; y <= d; y++)
            {
                px = cx + x;
                nx = cx - x;
                py = cy + y;
                ny = cy - y;

                tex.SetPixel(px, py, col);
                tex.SetPixel(nx, py, col);

                tex.SetPixel(px, ny, col);
                tex.SetPixel(nx, ny, col);

            }
        }
    }

    public Texture2D getCollisionMask()
    {
        return mask;
    }
}
