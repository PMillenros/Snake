using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private SnakeController controls;
    private SnakeController.MovementActions movement;
    private Vector2 direction;
    private bool switchingDirection = false;
    public readonly Queue<Vector2Int> Inputs = new Queue<Vector2Int>();
    public Snake snake;
    [SerializeField, Range(1, 50)] private int maxQueuedInputs;
    
    private void Awake()
    {
        controls = new SnakeController();
        movement = controls.Movement;
        movement.Vertical.performed += context => QueueInput(ToVector2Int( context.ReadValue<Vector2>()));
        movement.Horizontal.performed += context => QueueInput(ToVector2Int(context.ReadValue<Vector2>()));
    }
    private Vector2Int ToVector2Int(Vector2 v)
    {
        return new Vector2Int((int)v.x,(int)v.y);
    }
    private void QueueInput(Vector2Int direction)
    {
        if (direction == Vector2.zero || Inputs.Count > maxQueuedInputs - 1)
            return;
        if(Inputs.Count == 0 || direction != Inputs.Last())
            Inputs.Enqueue(direction);
    }
    public Vector2Int GetInput()
    {
        if (Inputs.Count > 0)
            return Inputs.Dequeue();
        return snake.direction;
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDestroy()
    {
        controls.Disable();
    }
}
