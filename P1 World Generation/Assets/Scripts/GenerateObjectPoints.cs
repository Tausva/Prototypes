using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObjectPoints : MonoBehaviour
{
    private List<Vector2> points;

    public int pointCount;
    public LayerMask landToSpawn;

    private int failSave;

    private GameObject[] tiles;

    void Start()
    {
        points = new List<Vector2>();
        failSave = pointCount * 10;

        tiles = GameObject.FindGameObjectsWithTag("Tile");

        int height = GetComponent<PerlinNoise>().height;
        int width = GetComponent<PerlinNoise>().width;

        while (pointCount > 0 && failSave > 0)
        {
            Vector2 point = new Vector2(Random.Range(transform.position.x, transform.position.x + width), Random.Range(transform.position.y, transform.position.y + height));

            if (landToSpawn == (landToSpawn | (1 << GetClosestTileLayer(point))))
            {
                points.Add(point);
                pointCount--;
            }

            failSave--;
        }
    }

    public List<Vector2> GetPoints ()
    {
        return points;
    }

    private int GetClosestTileLayer(Vector2 position)
    {
        GameObject closest = tiles[0];
        float distance = Mathf.Infinity;

        foreach (GameObject tile in tiles)
        {
            if (distance > Vector2.Distance(position, tile.transform.position))
            {
                closest = tile;
                distance = Vector2.Distance(position, tile.transform.position);
            }
        }
        return closest.layer;
    }
}
