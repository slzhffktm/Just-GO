﻿using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TMPro {

    public class ButtonController : MonoBehaviour
    {
        private string dbPath;

        private TextMeshProUGUI buttonText;
        private int buttonId;

    // Start is called before the first frame update
    void Start()
        {
            dbPath = "URI=file:" + Application.persistentDataPath + "/playerDatabase.db";
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
            buttonId = int.Parse(buttonText.text);
            buttonText.text = GetProfile();
        }

        public void OnClick()
        {
            if (buttonText.text == "Empty")
            {
                InsertNewProfile();
                LoadProfile();
            }
            else
            {
                LoadProfile();
            }
        }

        void InsertNewProfile()
        {
            using (var conn = new SqliteConnection(dbPath))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO profiles (id, level) " +
                                      "VALUES (@Id, @Level);";
                    
                    cmd.Parameters.Add(new SqliteParameter
                    {
                        ParameterName = "Id",
                        Value = buttonId
                    });
                    cmd.Parameters.Add(new SqliteParameter
                    {
                        ParameterName = "Level",
                        Value = 1
                    });

                    var result = cmd.ExecuteNonQuery();
                    Debug.Log("insert level: " + result);
                }
            }
        }

        void LoadProfile()
        {
            PlayerPrefs.SetInt("id", buttonId);
            PlayerPrefs.Save();

            SceneManager.LoadScene(1);
        }

        string GetProfile()
        {
            string text = "Empty";

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
                        Value = buttonId
                    });

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(0);
                        var level = reader.GetInt32(1);
                        text = string.Format("{0}: Level {1}", buttonId, level);
                        Debug.Log(text);
                    }
                }
            }

            return text;
        }
    }

}