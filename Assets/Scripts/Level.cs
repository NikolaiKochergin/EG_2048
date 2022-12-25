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
    [SerializeField][Min(0)] private int _numberOfBalls = 50;
    [SerializeField][Min(0)] private int _maxCreatedBallLevel = 1;
    [SerializeField] private Task[] _tasks;

    public int NumberOfBalls => _numberOfBalls;
    public int MaxCreatedBallLevel => _maxCreatedBallLevel;
    public IReadOnlyList<Task> Tasks => _tasks;
    public static Level Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
