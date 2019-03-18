using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

public class LevelsRenderer : MonoBehaviour
{
    private Transform levels;

    private string dbPath;
    private int playerId;

    private void Start()
    {
        dbPath = "URI=file:" + Application.persistentDataPath + "/playerDatabase.db";
        playerId = PlayerPrefs.GetInt("id");
        levels = this.gameObject.transform;

        RenderLevelButtons();
    }
    
    private void RenderLevelButtons()
    {
        int currentLevel = GetCurrentLevel();
        for (int i = 0; i < currentLevel; i++)
        {
            levels.GetChild(i).gameObject.SetActive(true);
        }
    }

    int GetCurrentLevel()
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
