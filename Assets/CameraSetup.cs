using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    private Grid levelGrid;
    [SerializeField] private int distanceToLevel;

    private void Start()
    {
        levelGrid = GameManager.Grid;
        CenterCamera();
    }
    private void CenterCamera()
    {
        Vector2[] gridCorners =
        {
            levelGrid[0, levelGrid.Height - 1].position, 
            levelGrid[levelGrid.Width - 1, levelGrid.Height - 1].position,
            levelGrid[0, 0].position, levelGrid[levelGrid.Width - 1, 0].position
        };
        transform.position = (Vector3)GetCenterPoint(gridCorners) + new Vector3(0, 0, distanceToLevel);
    }
    private Vector2 GetCenterPoint(params Vector2[] points)
    {
        float totalX = 0;
        float totalY = 0;
        int length = points.Length;
        for (int i = 0; i < length; i++)
        {
            totalX += points[i].x;
            totalY += points[i].y;
        }
        return new Vector2(totalX / length, totalY / length);
    }
}
