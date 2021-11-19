using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileObject
{
    public void OnCollision(ITileObject collision);
    public GameObject GetGameObject();
}
