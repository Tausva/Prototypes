using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTrim : MonoBehaviour
{
    public Sprite bottom;
    public Sprite left;
    public Sprite top;
    public Sprite right;
    public Sprite bottomLeft;
    public Sprite bottomRight;
    public Sprite topLeft;
    public Sprite topRgiht;

    // Start is called before the first frame update
    void Start()
    {
        int height = GameObject.Find("World").GetComponent<PerlinNoise>().height;
        int width = GameObject.Find("World").GetComponent<PerlinNoise>().width;

        Tuple<int, int>[,] map = new Tuple<int, int>[height, width];
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        for (int i =  0; i < tiles.Length; i++)
        {
            map[(int)tiles[i].transform.position.y, (int)tiles[i].transform.position.x] = new Tuple<int, int>(i, tiles[i].layer);
        }

        for (int i = 1; i < height - 1; i++)
        {
            for (int j = 1; j < width - 1; j++)
            {
                if (map[i, j].Item2 == 4)
                {
                    if (map[i + 1, j].Item2 != 4)
                    {
                        tiles[map[i, j].Item1].GetComponent<SpriteRenderer>().sprite = bottom;
                    }
                    else if (map[i - 1, j].Item2 != 4)
                    {
                        tiles[map[i, j].Item1].GetComponent<SpriteRenderer>().sprite = top;
                    }

                    if (map[i, j + 1].Item2 != 4)
                    {
                        tiles[map[i, j].Item1].GetComponent<SpriteRenderer>().sprite = left;
                    }
                    else if (map[i, j - 1].Item2 != 4)
                    {
                        tiles[map[i, j].Item1].GetComponent<SpriteRenderer>().sprite = right;
                    }
                }
            }
        }
    }
}
