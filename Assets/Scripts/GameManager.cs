using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static List<Controller> SnakeControllers = new List<Controller>();
    public static Grid Grid;
    public static GameObject text;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject foodSpawnerPrefab;
    [SerializeField] private GameObject snakePrefabPlayer;
    [SerializeField] private GameObject snakePrefabAI;
    [SerializeField] private GameObject gridPrefab;
    
    public static FoodSpawner FoodSpawner;
    public readonly static List<Tile> EmptyTiles = new List<Tile>();
    private void Awake()
    {
        text = gameOverText;
        Grid = Instantiate(gridPrefab).GetComponent<Grid>();
        GameObject snake = Instantiate(snakePrefabPlayer);
        SnakeControllers.Add( snake.GetComponent<Controller>());
        FoodSpawner = Instantiate(foodSpawnerPrefab).GetComponent<FoodSpawner>();
    }
}
