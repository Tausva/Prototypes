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

        CalculateChance();

        points = GetComponent<GenerateObjectPoints>().GetPoints();

        foreach (Vector2 point in points)
        {
            int choice = Random.Range(0, totalChance);

            for (int i = 0; i < objectsToSpawn.Count; i++)
            {
                if (choice < objectsToSpawn[i].ReturnChance())
                {
                    GameObject obj = Instantiate(objectsToSpawn[i].prefab, child.transform);
                    obj.transform.position = point;
                    obj.transform.SetParent(child.transform);
                    i += objectsToSpawn.Count;
                }
            }
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

    public void CalculateChance()
    {
        int value = 0;

        foreach ( ObjectPallete obj in objectsToSpawn)
        {
            value += obj.chance;
            obj.SetChance(value);
        }
    }
}

[System.Serializable]
public class ObjectPallete{
    public GameObject prefab;
    public int chance;
    private int chanceHeight;

    public int ReturnChance()
    {
        return chanceHeight;
    }

    public void SetChance(int value)
    {
        chanceHeight = value;
    }
}
