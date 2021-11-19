using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject[] levelPrefabs;
    private int currentLevel = 0; 

    private void Start()
    {
        CreateLevel();
    }
    private void CreateLevel() //Only purpose is for visuals
    {
        for (int i = 0; i < grid.Width; i++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                Transform platform = Instantiate(levelPrefabs[currentLevel], 
                    grid[i, y].position, Quaternion.identity).transform;
                platform.parent = transform;
            }
        }
    }
}
