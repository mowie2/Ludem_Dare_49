using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    Dictionary<Vector2, List<GameObject>> worldMap;
    List<Vector2> pointsOfIntress;
    float timeRemaining = .5f;
    float tileDestructionTime = .2f;
    int mapSizeX = 20;
    int mapSizeY = 20;
    void Start()
    {
        worldMap = new Dictionary<Vector2, List<GameObject>>();
        pointsOfIntress = new List<Vector2>();
        GenerateTiles(mapSizeX, mapSizeY);

        GeneratePortal(new Vector2(Random.Range(1, mapSizeX-1), Random.Range(1, mapSizeY-1)));

        GenerateKey(new Vector2(Random.Range(1, mapSizeX-1), Random.Range(1, mapSizeY-1)));
        GenerateKey(new Vector2(Random.Range(1, mapSizeX-1), Random.Range(1, mapSizeY-1)));
        GenerateKey(new Vector2(Random.Range(1, mapSizeX-1), Random.Range(1, mapSizeY-1)));

    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }

        if (timeRemaining < 0)
        {
            RemoveRandomTile();
            timeRemaining = tileDestructionTime;
        }
    }

    public void GenerateTiles(int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (pointsOfIntress.Contains(new Vector2(x, y)))
                {
                    continue;
                }

                GenerateGrass(new Vector2(x, y));

                if (Random.Range(0, 10) == 1)
                {
                    GenerateRock(new Vector2(x, y));
                }

                if(x == 0)
                {
                    GenerateRock(new Vector2(x, y));
                }
                if (y == 0)
                {
                    GenerateRock(new Vector2(x, y));
                }
                if (x == mapSizeX-1)
                {
                    GenerateRock(new Vector2(x, y));
                }
                if (y == mapSizeY-1)
                {
                    GenerateRock(new Vector2(x, y));
                }

            }
        }
    }

    public void GenerateGrass(Vector2 location)
    {
        GameObject Tile = new GameObject("Tile");
        Tile.transform.position = location;
        SpriteRenderer TileSprite = Tile.AddComponent<SpriteRenderer>();
        TileSprite.sprite = Resources.Load<Sprite>("Grass");
        TileSprite.sortingLayerName = "Ground";

        AddTilesToWorldMap(location, Tile);
    }

    public void GenerateRock(Vector2 location)
    {
        GameObject Rock = new GameObject("Rock");

        SpriteRenderer RockSprite = Rock.AddComponent<SpriteRenderer>();
        RockSprite.sprite = Resources.Load<Sprite>("Rock");
        RockSprite.sortingLayerName = "Ground";
        RockSprite.sortingOrder = 1;

        Rock.AddComponent<PolygonCollider2D>();
        Rock.transform.position = location;

        AddTilesToWorldMap(location, Rock);
    }
    public void GeneratePortal(Vector2 location)
    {
        GameObject Portal = new GameObject("Portal");
        SpriteRenderer PortalSprite = Portal.AddComponent<SpriteRenderer>();
        PortalSprite.sprite = Resources.Load<Sprite>("Portal");
        PortalSprite.sortingLayerName = "Ground";
        PortalSprite.sortingOrder = 2;

        Portal.AddComponent<PolygonCollider2D>();
        Portal.transform.position = location;

        pointsOfIntress.Add(location);
    }

    public void GenerateKey(Vector2 location)
    {
        GameObject Key = new GameObject("Key");
        SpriteRenderer KeySprite = Key.AddComponent<SpriteRenderer>();
        KeySprite.sprite = Resources.Load<Sprite>("Key");
        KeySprite.sortingLayerName = "Ground";
        KeySprite.sortingOrder = 2;

        Key.AddComponent<PolygonCollider2D>();
        Key.transform.position = location;
        pointsOfIntress.Add(location);
    }

    private void AddTilesToWorldMap(Vector2 location, GameObject tile)
    {
        if (worldMap.ContainsKey(location))
        {
            List<GameObject> worldMapGameObjects;
            worldMap.TryGetValue(location, out worldMapGameObjects);
            worldMapGameObjects.Add(tile);
            worldMap.Remove(location);
            worldMap.Add(location, worldMapGameObjects);
        }
        else
        {
            List<GameObject> aa = new List<GameObject>();
            aa.Add(tile);
            worldMap.Add(location, aa);
        }
    }

    public void RemoveObjectsAtlocation(Vector2 location)
    {
        if (!worldMap.ContainsKey(location))
        {
            return;
        }
        worldMap.TryGetValue(location, out List<GameObject> worldMapGameObjects);

        worldMap.Remove(location);

        for (int i = worldMapGameObjects.Count; i-- > 0;)
        {
            Destroy(worldMapGameObjects[i]);
        }
    }

    public void RemoveRandomTile()
    {
        Vector2 location = GetRandomDictionarKey();
        RemoveObjectsAtlocation(location);
        GenerateVoid(location);
    }

    private void GenerateVoid(Vector2 location)
    {
        GameObject Void = new GameObject("Void");
        SpriteRenderer VoidSprite = Void.AddComponent<SpriteRenderer>();
        VoidSprite.sprite = Resources.Load<Sprite>("Void");
        VoidSprite.sortingLayerName = "Ground";
        VoidSprite.sortingOrder = 1;

        Void.AddComponent<PolygonCollider2D>();
        Void.transform.position = location;
    }

    private Vector2 GetRandomDictionarKey()
    {
        Vector2 RandomLocation = new Vector2(Random.Range(0, mapSizeX), Random.Range(0, mapSizeY));

        if (worldMap.ContainsKey(RandomLocation) && !pointsOfIntress.Contains(RandomLocation))
        {
            return RandomLocation;
        }
        else
        {
            return GetRandomDictionarKey();
        }
    }
}
