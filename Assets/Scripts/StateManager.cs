using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{

    public static StateManager Instance;
    public string Name;
    public int Highscore = 0;
    public string HighscoreName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        LoadData();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }


    public void ChangeName(string newName)
    {
        Name = newName;
    }

    [System.Serializable]
    public class SavedData
    {
        public string Name;
        public int Highscore;
    }


    public void SaveData()
    {
        SavedData data = new SavedData();
        data.Name = Name;
        data.Highscore = Highscore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SavedData data = JsonUtility.FromJson<SavedData>(json);

            HighscoreName = data.Name;
            Highscore = data.Highscore;
        }
    }

}
