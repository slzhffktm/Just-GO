using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;
using TMPro;

namespace TMPro {

    public class ButtonController : MonoBehaviour
    {
        private string dbPath;
        private TextMeshProUGUI buttonText;

    // Start is called before the first frame update
    void Start()
        {
            dbPath = "URI=file:" + Application.persistentDataPath + "/playerDatabase.db";
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = GetProfile(int.Parse(buttonText.text));
        }

        public void OnClick()
        {
            if (buttonText.text == "Empty")
            {
                InsertNewProfile();
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
                    cmd.CommandText = "INSERT INTO profiles (level) " +
                                      "VALUES (@Level);";

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

        string GetProfile(int id)
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
                        Value = id
                    });

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var level = reader.GetInt32(0);
                        text = string.Format("{0}: Level {1}", id, level);
                        Debug.Log(text);
                    }
                }
            }

            return text;
        }
    }

}