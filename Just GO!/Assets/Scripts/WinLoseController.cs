﻿using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinLoseController : MonoBehaviour
{
    private string dbPath;
    private int playerId;

    private float startTime;
    public float surviveTime;
    private float elapsedTime;
    public PlayerController player;

    public Text timePassed;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        dbPath = "URI=file:" + Application.persistentDataPath + "/playerDatabase.db";
        playerId = PlayerPrefs.GetInt("id");
    }

    private void Update()
    {
        elapsedTime = Time.time - startTime;
        timePassed.text = (elapsedTime).ToString();
        if (player.alive == false)
        {
            IsLose();
        }
        if (elapsedTime >= surviveTime)
        {
            IsWin();
            player.movementSpeed = 0;
            player.isWin = true;
        }
    }

    void IsWin()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int currentLevel = int.Parse(currentScene.name[currentScene.name.Length - 1].ToString());
        int unlockedLevel = GetUnlockedLevel();

        print(currentLevel);

        if (currentLevel == unlockedLevel)
        {
            UpdateLevelUnlocked(currentLevel + 1);
        }

        SceneManager.LoadScene(1);
    }

    void IsLose()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateLevelUnlocked(int newUnlockedLevel)
    {
        using (var conn = new SqliteConnection(dbPath))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE profiles SET level = @Level WHERE id = @Id;";

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "Level",
                    Value = newUnlockedLevel
                });
                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "Id",
                    Value = playerId
                });

                var result = cmd.ExecuteNonQuery();
                Debug.Log("create schema: " + result);
            }
        }
    }

    int GetUnlockedLevel()
    {
        int level = 1;

        using (var conn = new SqliteConnection(dbPath))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM profiles WHERE id = @Id;";

                cmd.Parameters.Add(new SqliteParameter
                {
                    ParameterName = "Id",
                    Value = playerId
                });

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    level = reader.GetInt32(0);
                }
            }
        }

        return level;
    }
}
