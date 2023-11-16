using _Game.ScriptsTableObj;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Game.Scripts
{
    public class SelectPlayer : MonoBehaviour
    {
        [SerializeField] private LevelData levelData;
        [SerializeField] private Button[] buttonPlayer;
        [SerializeField] private Button[] buttonBot;
        [SerializeField] private Button[] buttonMap;
        [SerializeField] private Sprite imageBtnOn, imageBtnOff;
        [SerializeField] private TextMeshProUGUI textSelect;
        [SerializeField] private bool isSelectPlayer, isSelectBot, isSelectMap;
        [SerializeField] private Button buttonStart;

        private void Start()
        {
            foreach (Button button in buttonPlayer)
            {
                button.onClick.AddListener(() => OnButtonClick(button, buttonPlayer));
                button.image.sprite = imageBtnOff;
            }

            foreach (Button button in buttonBot)
            {
                button.onClick.AddListener(() => OnButtonClick(button, buttonBot));
                button.image.sprite = imageBtnOff;
            }

            foreach (Button button in buttonMap)
            {
                button.onClick.AddListener(() => OnButtonClick(button, buttonMap));
                button.image.sprite = imageBtnOff;
            }

            buttonPlayer[0].onClick.AddListener(() => SetCountPlayer(1));
            buttonPlayer[1].onClick.AddListener(() => SetCountPlayer(2));
            buttonBot[0].onClick.AddListener(() => SetCountBot(1));
            buttonBot[1].onClick.AddListener(() => SetCountBot(2));
            buttonBot[2].onClick.AddListener(() => SetCountBot(3));
            buttonBot[3].onClick.AddListener(() => SetCountBot(0));
            buttonMap[0].onClick.AddListener(() => SetCountMap(0));
            buttonMap[1].onClick.AddListener(() => SetCountMap(1));
            buttonMap[2].onClick.AddListener(() => SetCountMap(2));
            buttonMap[3].onClick.AddListener(() => SetCountMap(3));
            buttonStart.onClick.AddListener(StartGame);
        }

        private void Update()
        {
            if (levelData.playerCount == 1)
            {
                buttonBot[2].enabled = true;
                buttonBot[3].enabled = false;
                buttonBot[3].image.sprite = imageBtnOff;
                if (levelData.botCount == 0)
                {
                    isSelectBot = false;
                }
            }

            if (levelData.playerCount == 2)
            {
                buttonBot[3].enabled = true;
                buttonBot[2].enabled = false;
                buttonBot[2].image.sprite = imageBtnOff;
                if (levelData.botCount == 3)
                {
                    isSelectBot = false;
                }
            }
        }
        private void OnButtonClick(Button clickedButton, Button[] arrBtn)
        {
            foreach (Button button in arrBtn)
            {
                if (button == clickedButton)
                {
                    button.image.sprite = imageBtnOn;
                }
                else
                {
                    button.image.sprite = imageBtnOff;
                }

                textSelect.gameObject.SetActive(false);
            }
        }

        private void StartGame()
        {
            if (!isSelectPlayer || !isSelectBot || !isSelectMap)
            {
                textSelect.gameObject.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene("LevelScene");
            }
        }

        private void SetCountPlayer(int index)
        {
            levelData.playerCount = index;
            isSelectPlayer = true;
        }

        private void SetCountBot(int index)
        {
            levelData.botCount = index;
            isSelectBot = true;
        }

        private void SetCountMap(int index)
        {
            levelData.mapCount = index;
            levelData.SetLevel();
            isSelectMap = true;
        }
    }
}