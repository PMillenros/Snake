using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedList<T>
{
    private Node<T> first;
    private Node<T> last;
    public Node<T> First => first;
    public Node<T> Last => last;
    private int count = 0;
    public int Count => count;

    public void AddLast(T value)
    {
        Node<T> newNode = new Node<T>(value);
        if (Count == 0)
        {
            NodeIntoEmptyList(newNode);
            return;
        }
        last.next = newNode;
        last = newNode;
        count++;
    }
    private void NodeIntoEmptyList(Node<T> newNode)
    {
        first = newNode;
        last = newNode;
        count++;
    }

    public void MoveLastNodeToFront()
    {
        if (first == null || first.next == null)
            return;
        
        Node<T> secLastNode = null;
        Node<T> lastNode = first;
        while (lastNode.next != null)
        {
            secLastNode = lastNode;
            lastNode = lastNode.next;
        }
        secLastNode.next = null; 
        lastNode.next = first;
        first = lastNode;
        last = secLastNode;
    }
}
