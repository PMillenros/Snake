using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<T>
{
    private T value;
    public T Value => value;
    public Node<T> next;
    public Node<T> Next => next;
    public Node(T value)
    {
        this.value = value;
    }
}
