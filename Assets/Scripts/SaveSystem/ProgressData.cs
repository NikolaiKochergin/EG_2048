[System.Serializable]
public class ProgressData
{
    public int Level;
    public int Coins;
    public float[] BackgroundColor;
    public bool IsMusicOn;

    public ProgressData(Progress progress)
    {
        Level = progress.Level;
        Coins = progress.Coins;
        BackgroundColor = new float[3];
        BackgroundColor[0] = progress.BackgroundColor.r;
        BackgroundColor[1] = progress.BackgroundColor.g;
        BackgroundColor[2] = progress.BackgroundColor.b;
        IsMusicOn = progress.IsMusicOn;
    }
}