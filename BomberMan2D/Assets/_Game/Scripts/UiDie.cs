using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Game.Scripts
{
    public class UiDie : MonoBehaviour
    {
        [SerializeField] private Button btnHome, btnLoad;
        [SerializeField] private TextMeshProUGUI textWin;
        private void Start()
        {
            btnHome.onClick.AddListener(LoadHome);
            btnLoad.onClick.AddListener(Reset);
        }

        private void Reset()
        {
            SceneManager.LoadScene("LevelScene");
        }

        private void LoadHome()
        {
            SceneManager.LoadScene("MenuScene");
        }

        public void SetText(string text)
        {
            textWin.SetText(text);
        }
    }
}
