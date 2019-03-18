using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

public class SQLiteHelper : MonoBehaviour
{
    private string dbPath;

    private void Start()
    {
        dbPath = "URI=file:" + Application.persistentDataPath + "/playerDatabase.db";
        CreateSchema();
    }

    public void CreateSchema()
    {
        using (var conn = new SqliteConnection(dbPath))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS 'profiles' ( " +
                                  "  'id' INTEGER PRIMARY KEY, " +
                                  "  'level' INTEGER NOT NULL" +
                                  ");";

                var result = cmd.ExecuteNonQuery();
                Debug.Log("create schema: " + result);
            }
        }
    }
}
