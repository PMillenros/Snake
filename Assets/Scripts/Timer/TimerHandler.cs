using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Timers;
using UnityEngine;

public class TimerHandler : MonoBehaviour
{
    private void Start()
    {
        MoveSnakes();
    }
    private void MoveSnakes()
    {
        List<Controller> snakeControllers = GameManager.SnakeControllers;
        for (int i = 0; i < snakeControllers.Count; i++) 
        {
            Controller snakeController = snakeControllers[i];
            float delay = snakeController.snake.moveDelay;
            
            CallbackTimer.SetTimer(()=> LoopSnakeMovement(snakeController), delay);
        }
    }
    private void LoopSnakeMovement(Controller snakeController)
    {
        if (!GameManager.SnakeControllers.Contains(snakeController))
            return;
        
        Vector2Int input = snakeController.GetInput();
        
        float delay = snakeController.snake.moveDelay;

        snakeController.snake.SetDirection(input);
        snakeController.snake.MoveSnake();
        CallbackTimer.SetTimer(()=> LoopSnakeMovement(snakeController), delay);
    }
}
