using UnityEngine;
using UnityEngine.SceneManagement;

namespace TMPro
{
    public class LevelLoader : MonoBehaviour
    {
        private TextMeshProUGUI buttonText;
        private int levelId;

        // Start is called before the first frame update
        void Start()
        {
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
            levelId = int.Parse(buttonText.text);
        }

        public void LoadLevel()
        {
            SceneManager.LoadScene(levelId + 1);
        }
    }
}