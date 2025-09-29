using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class PlatformGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile[] tiles;

    public int maxY = -1;
    public int minY = -5;

    public int posX = 9;
    public int posY = -5;

    // Start is called before the first frame update
    void Start()
    {
        createPlatform(3, 1, 6);
        StartCoroutine(generate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator generate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            createPlatform(distanceX(), distanceY(), width());
        }
    }

    private void createPlatform(int distX, int distY, int width)
    {
        for (int i = 0; i < width; i++)
        {
            while (yCheck(distY) == false)
            {
                distY = distanceY();
            }
            Tile tile;
            Vector3Int placementPosition = new Vector3Int(posX + distX, posY + distY, 0);

            if (i == 0)
            {
                tile = tiles[0];
            }

            else if (i == width - 1)
            {
                tile = tiles[3];
            }

            else
            {
                tile = tiles[tileSelector()];
            }

            tilemap.SetTile(placementPosition, tile);
            posX++;
        }

        posY += distY;
        posX += 2;
    }

    private bool yCheck(int y)
    {
        int newY = y + posY;
        if (newY > maxY || newY < minY)
        {
            return false;
        }

        else
        {
            return true;
        }
    }
    #region GENERATORS
    private int distanceX()
    {
        return Random.Range(1, 6);
    }
    private int distanceY()
    {
        return Random.Range(-2, 3);
    }
    private int width()
    {
        return Random.Range(5, 11);
    }
    private int tileSelector()
    {
        return Random.Range(1, 3);
    }
    #endregion
}
