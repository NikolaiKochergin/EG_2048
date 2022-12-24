using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Task
{
    public ItemType ItemType;
    public int Number;
    public int Level;
}

public class Level : MonoBehaviour
{
    [SerializeField] private int _numberOfBalls = 50;
    [SerializeField] private int _maxCreatedBallLevel = 1;
    [SerializeField] private Task[] _tasks;

    public IReadOnlyList<Task> Tasks => _tasks;
}
