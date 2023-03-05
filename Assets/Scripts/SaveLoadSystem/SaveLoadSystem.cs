using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadSystem : MonoBehaviour
{
    // Public
    public static SaveLoadSystem Instance;

    // Private
    private string _savePath;

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
            return;
        }

        _savePath = Application.persistentDataPath + "/save.dat";
    }

    public void SaveLanguage(Languages language)
    {
        FileStream stream = File.Create(_savePath);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, language);
        stream.Close();
    }

    public Languages LoadLanguage()
    {
        if (File.Exists(_savePath))
        {
            FileStream stream = File.Open(_savePath, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            Languages language = (Languages)formatter.Deserialize(stream);
            stream.Close();
            return language;
        }
        else
        {
            return Languages.EN;
        }
    }
}