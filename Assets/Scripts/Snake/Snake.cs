using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    [SerializeField] private GameObject headPrefab;
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private Vector2Int startPosition;
    [SerializeField] private int startLength;

    private Grid grid;
    public Vector2Int direction;
    public float moveDelay = 0.6f;

    private LinkedList<Segment> linkedSegments = new LinkedList<Segment>();
    private void Awake()
    {
        GameManager.text.SetActive(false);
        grid = GameManager.Grid;
        SetupSnake();
    }
    private void SetupSnake()
    {
        SpawnHead();
        Grow(startLength);
    }
    private void SpawnHead()
    {
        linkedSegments = new LinkedList<Segment>();
        linkedSegments.AddLast(Instantiate(headPrefab, Vector3.zero, 
            Quaternion.identity).GetComponent<Segment>());
        SetSegmentPosition(linkedSegments.First.Value, startPosition);
        linkedSegments.First.Value.transform.parent = transform;
        linkedSegments.First.Value.isHead = true;
        OccupyTile(linkedSegments.First.Value.Coordinate);
    }
    public void Grow(int growAmount)
    {
        for (int i = 0; i < growAmount; i++)
        {
            AddSegment();
        }
    }
    private void AddSegment()
    {
        Segment lastSegment = linkedSegments.Last.Value;

        Segment newSegment = Instantiate(bodyPrefab, Vector2.zero, 
            Quaternion.identity).GetComponent<Segment>();

        SetSegmentPosition(newSegment, lastSegment.Coordinate);
        newSegment.transform.parent = transform;
        linkedSegments.AddLast(newSegment);
        OccupyTile(newSegment.Coordinate);
    }

    private void OccupyTile(Vector2Int coordinate)
    {
        if(GameManager.EmptyTiles.Contains(GameManager.Grid[coordinate]))
            GameManager.EmptyTiles.Remove(GameManager.Grid[coordinate]);
    }
    private void FreeUpTile(Vector2Int coordinate)
    {
        if(!GameManager.EmptyTiles.Contains(GameManager.Grid[coordinate]))
            GameManager.EmptyTiles.Add(GameManager.Grid[coordinate]);
    }
    public void SetDirection(Vector2Int direction)
    {
        if(IsValidDirection(direction))
            this.direction = direction;
    }
    private bool IsValidDirection(Vector2Int direction)
    {
        if (direction == -this.direction || direction == this.direction)
            return false;
        return true;
    }
    public void MoveSnake()
    {
        Vector2Int headPosition = linkedSegments.First.Value.Coordinate;
        linkedSegments.First.Value.isHead = false;
        Vector2Int newPosition = headPosition + direction;
        Segment tail = linkedSegments.Last.Value;
        ExitTile(tail);
        FreeUpTile(tail.Coordinate);
        SetSegmentPosition(tail, newPosition);
        tail.isHead = true;
        EnterTile(tail);
        OccupyTile(tail.Coordinate);
        linkedSegments.MoveLastNodeToFront();
    }
    private void EnterTile(Segment segment)
    {
        Tile tile = grid[segment.Coordinate];
        tile.Enter(segment);
    }
    private void ExitTile(Segment segment)
    {
        Tile tile = grid[segment.Coordinate];
        tile.Exit(segment);
    }
    private void SetSegmentPosition(Segment segment, Vector2Int coordinates)
    {
        coordinates = WrapValues(coordinates);
        segment.SetPosition(grid[coordinates].position);
        segment.SetGridCordinate(coordinates);
    }
    private Vector2Int WrapValues(Vector2Int coordinates)
    {
        if (coordinates.x > GameManager.Grid.Width - 1)
            coordinates.x = 0;
        if (coordinates.x < 0)
            coordinates.x = GameManager.Grid.Width - 1;
        if (coordinates.y > GameManager.Grid.Height - 1)
            coordinates.y = 0;
        if (coordinates.y < 0)
            coordinates.y = GameManager.Grid.Height - 1;
        return coordinates;
    }
    public void Death()
    {
        GameManager.SnakeControllers.Remove(GetComponent<Controller>());
        Destroy(gameObject);
        GameManager.text.SetActive(true);
    }
}
