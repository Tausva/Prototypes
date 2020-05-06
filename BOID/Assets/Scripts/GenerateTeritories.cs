using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTeritories : MonoBehaviour
{
    private List<GameObject> boidList = new List<GameObject>();

    public string boidTag;

    private static int rowCount;
    private static int columnCount;
    public int rowCountSet;
    public int columnCountSet;


    private float left;
    private float bottom;
    private float length;
    private float height;

    private List<GameObject>[] regions = new List<GameObject>[rowCount * columnCount];

    // Start is called before the first frame update
    void Start()
    {
        rowCount = rowCountSet;
        columnCount = columnCountSet;

        boidList = new List<GameObject>(GameObject.FindGameObjectsWithTag(boidTag));

        bottom = Camera.main.transform.position.y - Camera.main.orthographicSize;
        left = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect;
        length = Camera.main.orthographicSize * Camera.main.aspect * 2;
        height = Camera.main.orthographicSize * 2;
    }

    // Update is called once per frame
    void Update()
    {
        regions = new List<GameObject>[rowCount * columnCount];
        for (int i = 0; i < regions.Length; i++)
        {
            regions[i] = new List<GameObject>();
        }

        foreach (GameObject boid in boidList)
        {
            int y = (int)(Mathf.Ceil((boid.transform.position.y - bottom) / height * rowCount)) - 1;
            int x = (int)(Mathf.Ceil((boid.transform.position.x - left) / length * columnCount)) - 1;
            int regionID = y * columnCount + x;
            
            if (regionID >= 0 && regionID < regions.Length)
            {
                regions[regionID].Add(boid);
            }
        }
    }

    public List<GameObject> ReturnRegionBoids(GameObject boid)
    {
        for (int i = 0; i < rowCount * columnCount; i++)
        {
            if (regions[i].Contains(boid)) return regions[i];
        }
        return new List<GameObject>();
    }
}
