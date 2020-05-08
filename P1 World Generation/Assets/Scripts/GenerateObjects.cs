using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObjects : MonoBehaviour
{
    private List<Vector2> points;

    public List<ObjectPallete> objectsToSpawn;
    
    void Start()
    {
        GameObject child = new GameObject("Objects");
        child.transform.SetParent(transform);
        int totalChance = GetTotalAmount();

        points = GetComponent<GenerateObjectPoints>().GetPoints();

        foreach (Vector2 point in points)
        {
            int choice = Random.Range(0, totalChance);
            float lastHeight = -1;

            //foreach (ObjectPallete tileHeight in objectsToSpawn)
            //{
            //    if (choice < tileHeight.chance && choice > lastHeight)
            //    {
            //        GameObject obj = Instantiate(tileHeight.tile);
            //        obj.transform.position = new Vector2(x, y);
            //        obj.transform.SetParent(child.transform);
            //        lastHeight = sample;
            //    }
            //}
            //if ()
            //{
                GameObject ins = Instantiate(objectsToSpawn[0].prefab, child.transform);
                ins.transform.position = point;
            //}
            
        }
    }

    private int GetTotalAmount()
    {
        int result = 0;

        foreach (ObjectPallete element in objectsToSpawn)
        {
            result += element.chance;
        }

        return result;
    }
}

[System.Serializable]
public class ObjectPallete{
    public GameObject prefab;
    public int chance;
}
