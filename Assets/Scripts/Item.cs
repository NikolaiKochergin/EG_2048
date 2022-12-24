using UnityEngine;

public enum ItemType
{
    Empty,
    Ball,
    Barrel,
    Stone,
    Box,
    Dynamit,
    Star,
    Question
}

public class Item : MonoBehaviour
{
    public ItemType ItemType;
}