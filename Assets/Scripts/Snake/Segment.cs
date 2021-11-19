using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour, ITileObject
{
    private Vector2Int coordinate;
    private Vector2Int startPosition = new Vector2Int(0, 0);
    public bool isHead = false;
    public Vector2Int Coordinate
    {
        get => coordinate; 
        set => coordinate = value;
    }

    public void SetPosition(Vector2 newPosition)
    {
        transform.position = newPosition;
    }

    public void SetGridCordinate(Vector2Int coordinate)
    {
        if (!OutOfBounds(coordinate))
        {
            this.coordinate = ScreenWrap(coordinate);
            return;
        }
        this.coordinate = new Vector2Int(coordinate.x, coordinate.y);
    }
    private bool OutOfBounds(Vector2Int coordinate)
    {
        return true;
    }
    private Vector2Int ScreenWrap(Vector2Int coordinate)
    {
        int maxY = GameManager.Grid.Height;
        int maxX = GameManager.Grid.Width;
        return new Vector2Int ((coordinate.x + maxX) % maxX, (coordinate.y + maxY) % maxY);
    }
    public void OnCollision(ITileObject collision)
    {
        if (collision.GetGameObject().TryGetComponent(out Segment segment))
            if(segment.transform.parent.TryGetComponent(out Snake snake))
                snake.Death();
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }
    private void OnDestroy()
    {
        Tile tile = GameManager.Grid[Coordinate];
        tile.Exit(this);
    }
}
