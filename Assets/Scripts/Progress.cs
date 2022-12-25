using UnityEngine;

public class Progress : MonoBehaviour
{
    public int Level;
    public int Coins;

    public static Progress Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLevel(int level)
    {
        Level = level;
    }

    public void AddCoins(int value)
    {
        Coins += value;
    }
}