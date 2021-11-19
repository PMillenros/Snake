using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int offsetWidth;
    [SerializeField] private int offsetHeight;

    private Tile[,] grid;
    public int Width
    {
        get => width;
        set => width = value;
    }
    public int Height
    {
        get => height;
        set => height = value;
    }
    public Tile this[int column, int row]
    {
        get => grid[column, row];
    }

    public Tile this[Vector2Int coordinates]
    {
        get => grid[coordinates.x, coordinates.y];
    }
    void Awake()
    {
        SetupGrid();
    }
    private void SetupGrid()
    {
        grid = new Tile[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[i, y] = new Tile();
                SetTilePosition(i, y);
                GameManager.EmptyTiles.Add(grid[i, y]);
            }
        }
    }
    private void SetTilePosition(int column, int row)
    {
        this[column, row].coordinates = new Vector2Int(column, row);
        this[column, row].position = new Vector2Int(column * offsetWidth, row * offsetHeight);
    }
}
