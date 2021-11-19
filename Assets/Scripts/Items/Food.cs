using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, ITileObject
{
    public int foodValue = 1;
    public Vector2Int coordinate;
    private Tile tile;

    private void Start()
    {
        EnterTile();
    }

    private void EnterTile()
    {
        Tile tile = GameManager.Grid[coordinate];
        tile.Enter(this);
    }

    public void OnCollision(ITileObject collision)
    {
        if (collision.GetGameObject().TryGetComponent(out Segment segment))
        {
            if (segment.transform.parent.TryGetComponent(out Snake snake))
            {
                snake.Grow(foodValue);
                Destroy(gameObject);
                GameManager.FoodSpawner.SpawnFood();
            }
        }
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
