using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GenerateTiles(10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateTiles(int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject Tile = new GameObject("Tile");
                Tile.transform.position = new Vector2(x, y);
                SpriteRenderer TileSprite = Tile.AddComponent<SpriteRenderer>();
                TileSprite.sprite = Resources.Load<Sprite>("Grass");
                TileSprite.sortingLayerName = "Ground";

            }
        }
    }
}
