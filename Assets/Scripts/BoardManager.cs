using UnityEngine;
using System.Collections;


public class BoardManager : MonoBehaviour {

    public Transform cutoutMesh;

    public float pizzaRadius { private set; get; }

    public GameObject pizzaPrefab;

    private Texture2D mask;

    private Material maskMaterial;

    private GameManager gameManager;

    private GameObject pizza;

    public void setupPizza()
    {
        GameManager gameManager = GetComponent<GameManager>();
        pizza = Instantiate(pizzaPrefab);
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

    public void CreateCutout(Vector2[] points)
    {
        Transform cutout = Instantiate(cutoutMesh);

        cutout.name = "Cutout";
        cutout.parent = pizza.transform;

        PolygonCollider2D collider = cutout.GetComponent<PolygonCollider2D>();
        collider.SetPath(0, points);

        MeshFilter filter = cutout.GetComponent<MeshFilter>();

        Mesh mesh = new Mesh();

        Triangulator tr = new Triangulator(points);
        int[] indices = tr.Triangulate();

        Vector3[] vertices = new Vector3[points.Length];
        Vector2[] uvs = new Vector2[points.Length];
        Sprite pizzaSprite = pizza.GetComponent<SpriteRenderer>().sprite;
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 vert = points[i];
            vertices[i] = new Vector3(vert.x, vert.y, -.1f);
            Vector2 size = pizzaSprite.bounds.size;
            uvs[i] = new Vector2((vert.x + size.x / 2) / size.x, (vert.y + size.y / 2) / size.y);
        }

        mesh.vertices = vertices;
        mesh.triangles = indices;
        mesh.uv = uvs;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        filter.mesh = mesh;
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
