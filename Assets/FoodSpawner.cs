using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    private Grid grid;
    [SerializeField] private float delay;
    [SerializeField] private GameObject foodPrefab;
    
    private void Start()
    {
        grid = GameManager.Grid;
        SpawnFood();
    }
    public void SpawnFood()
    {
        Food food = Instantiate(foodPrefab).GetComponent<Food>();
        int length = GameManager.EmptyTiles.Count;
        Tile tile = GameManager.EmptyTiles[Random.Range(0, length)];
        food.coordinate = tile.coordinates;
        food.transform.position = tile.position;
    }
}
