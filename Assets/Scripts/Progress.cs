using UnityEngine;

public class Progress : MonoBehaviour
{
    public int Level;
    public int Coins;
    public Color BackgroundColor;
    public bool IsMusicOn;

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
        Load();
    }

    public void SetLevel(int level)
    {
        Level = level;
        Save();
    }

    public void AddCoins(int value)
    {
        Coins += value;
        Save();
    }

    [ContextMenu("DeleteFile")]
    public void DeleteFile()
    {
        SaveSystem.DeleteFile();
    }

    [ContextMenu("Save")]
    public void Save()
    {
        SaveSystem.Save(this);
    }

    [ContextMenu("Load")]
    public void Load()
    {
        ProgressData progressData = SaveSystem.Load();
        if (progressData != null)
        {
            Level = progressData.Level;
            Coins = progressData.Coins;
            Color color = new Color();
            color.r = progressData.BackgroundColor[0];
            color.g = progressData.BackgroundColor[1];
            color.b = progressData.BackgroundColor[2];
            BackgroundColor = color;
            IsMusicOn = progressData.IsMusicOn;
        }
        else
        {
            Level = 1;
            Coins = 0;
            BackgroundColor = Color.cyan * 0.6f;
            IsMusicOn = true;
        }
    }
}