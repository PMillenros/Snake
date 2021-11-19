using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile 
{
    public Vector2Int coordinates;
    public Vector2 position;
    public ITileObject TileObjectSlot;
    public List<Segment> Segments = new List<Segment>();
    public bool IsEmpty()
    {
        if (TileObjectSlot == null)
            return true;
        return false;
    }
    public void Enter(ITileObject enteringObject)
    {
        if (enteringObject.GetGameObject().TryGetComponent(out Segment segment))
        {
            if (segment.isHead && Segments.Count > 0)
            {
                segment.transform.parent.GetComponent<Snake>().Death();
                return;
            }
            if (!IsEmpty())
            {
                TileObjectSlot.OnCollision(enteringObject);
                TileObjectSlot = null;
            }
            else
                Segments.Add(segment);
        }
        else
            TileObjectSlot = enteringObject;
        if (GameManager.EmptyTiles.Contains(this))
        {
            GameManager.EmptyTiles.Remove(this);
        }
    }

    public void Exit(ITileObject exitingObject)
    {
        if (exitingObject == TileObjectSlot)
        {
            TileObjectSlot = null;
        }
        if (exitingObject.GetGameObject().TryGetComponent(out Segment segment))
        {
            Segments.Remove(segment);
        }
        if (Segments.Count == 0 && !GameManager.EmptyTiles.Contains(this))
        {
            GameManager.EmptyTiles.Add(this);
        }
    }
    public void EmptySlot()
    {
        TileObjectSlot = null;
    }
}
