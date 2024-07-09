using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashSpawner : MonoBehaviour
{
    public static System.Action OnTrashClean;
    [SerializeField]
    private int maxTrash;
    [SerializeField]
    private float minSpawnTime = 15f;
    [SerializeField]
    private float maxSpawnTime = 25f;
    [SerializeField]
    private PolygonCollider2D boundCol;
    [SerializeField]
    private Transform cloneHolder;
    [SerializeField]
    private Transform holder;
    [SerializeField]
    private HouseTrash[] trashPrefabs;

    private float timeCounter = 0;
    private float spawnTime;
    private int trashCounter;
    private void Awake()
    {
        timeCounter = 0;
        trashCounter = 0;
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        OnTrashClean += OnTrashDetroy;
    }
    private void OnDestroy()
    {
        OnTrashClean -= OnTrashDetroy;
    }
    private void Update()
    {
        if (!IsFull())
        {
            timeCounter += Time.deltaTime;
            if (timeCounter > spawnTime)
            {
                SpawnTrash();
                timeCounter = 0;
            }
        }
        
    }
    public bool IsFull()
    {
        return trashCounter >= maxTrash;
    }
    public void SpawnTrash()
    {
        Vector2 spawnPos = GetRandomPointInPolygonCollider(boundCol);
        trashCounter++;
        var item = Instantiate(trashPrefabs[Random.Range(0, trashPrefabs.Length)], spawnPos, Quaternion.identity, HouseItemHolder.Instance.holder);
        item.OnSpawn(HouseItemHolder.Instance.canvasHolder);
        //item.transform.localScale
    }
    public void OnTrashDetroy()
    {
        timeCounter = 0;
        trashCounter--;
    }

    Vector2 GetRandomPointInTriangle(Vector2 a, Vector2 b, Vector2 c)
    {
        float r1 = Random.value;
        float r2 = Random.value;

        if (r1 + r2 > 1)
        {
            r1 = 1 - r1;
            r2 = 1 - r2;
        }

        return a + r1 * (b - a) + r2 * (c - a);
    }

    Vector2 GetRandomPointInPolygonCollider(PolygonCollider2D collider)
    {
        int pointCount = collider.points.Length;
        if (pointCount < 3)
        {
            Debug.LogError("The polygon collider must have at least 3 points.");
            return Vector2.zero;
        }

        int randomTriangleIndex = Random.Range(0, pointCount - 2);
        Vector2 a = collider.points[0];
        Vector2 b = collider.points[randomTriangleIndex + 1];
        Vector2 c = collider.points[randomTriangleIndex + 2];

        return GetRandomPointInTriangle(a, b, c);
    }

   

}
