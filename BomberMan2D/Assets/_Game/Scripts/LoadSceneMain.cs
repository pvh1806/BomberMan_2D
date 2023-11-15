using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Scripts
{
    public class LoadSceneMain : MonoBehaviour
    {
        void Start()
        {
            Invoke(nameof(LoadScene), 5f);
        }

        private void LoadScene()
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}