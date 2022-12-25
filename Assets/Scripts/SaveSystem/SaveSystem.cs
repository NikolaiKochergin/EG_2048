using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Application = UnityEngine.Device.Application;

public static class SaveSystem
{
    private static string DataPath = Application.persistentDataPath + "/progress.yo";
    
    public static void Save(Progress progress)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(DataPath, FileMode.Create);
        ProgressData progressData = new ProgressData(progress);
        binaryFormatter.Serialize(fileStream, progressData);
        fileStream.Close();
    }

    public static ProgressData Load()
    {
        if (File.Exists(DataPath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(DataPath, FileMode.Open);
            ProgressData progressData = binaryFormatter.Deserialize(fileStream) as ProgressData;
            fileStream.Close();
            return progressData;
        }
        else
        {
            Debug.Log("No file");
            return null;
        }
    }

    public static void DeleteFile()
    {
        File.Delete(DataPath);
    }
}