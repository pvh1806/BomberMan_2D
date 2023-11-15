using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Scripts.Canvas
{
    public class PlayGame : MonoBehaviour
    {

        public void LoadScene()
        {
            SceneManager.LoadScene("SelectPlayer");
        }
    }
}
