using Game.Entities;
using Game.Global;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Game.Managers
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField]
        private UIDocument _ui;

        [SerializeField]
        private VisualTreeAsset _inventoryIconTemplate;

        private VisualElement _hudDisplay;

        private VisualElement _inventoryHolder;
        private Label _timeLabel;

        private VisualElement _pauseMenu;
        private Button _resumeButton;
        private Button _exitButton;

        private VisualElement _completeDisplay;
        private Button _completeExitToTitle;
        private Label _completeTimeText;

        void Awake()
        {
            _hudDisplay = _ui.rootVisualElement.Q("Main");

            _inventoryHolder = _hudDisplay.Q("InventoryContainer");
            _timeLabel = _hudDisplay.Q<Label>("TimeLeftLabel");

            _pauseMenu = _ui.rootVisualElement.Q("Pause");

            _resumeButton = _pauseMenu.Q<Button>("ResumeButton");
            _exitButton = _pauseMenu.Q<Button>("ExitButton");

            _completeDisplay = _ui.rootVisualElement.Q("Complete");
            _completeExitToTitle = _completeDisplay.Q<Button>("ExitButton");
            _completeTimeText = _completeDisplay.Q<Label>("CompletedTimeText");
        }

        private void GamePauseChanged(bool paused)
        {
            if (paused)
            {
                // TODO: Show the pause menu.
                _hudDisplay.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
                _pauseMenu.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
            }
            else
            {
                // TODO: Hide the pause menu.
                _hudDisplay.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
                _pauseMenu.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            }
        }

        private void GameCompleteChanged(bool complete)
        {
            if (complete)
            {
                // TODO: Show the complete screen.
                _hudDisplay.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
                _completeDisplay.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
            }
            else
            {
                // TODO: Do nothing? We probably 
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            _hudDisplay = _ui.rootVisualElement.Q("Main");

            _inventoryHolder = _hudDisplay.Q("InventoryContainer");
            _timeLabel = _hudDisplay.Q<Label>("TimeLeftLabel");

            _pauseMenu = _ui.rootVisualElement.Q("Pause");

            _resumeButton = _pauseMenu.Q<Button>("ResumeButton");
            _resumeButton.clicked += () => GameSettings.SetPause(false);
            
            _exitButton = _pauseMenu.Q<Button>("ExitButton");
            _exitButton.clicked += () => SceneManager.LoadScene(0);

            _completeDisplay = _ui.rootVisualElement.Q("Complete");
            _completeExitToTitle = _completeDisplay.Q<Button>("ExitButton");
            _completeExitToTitle.clicked += () => SceneManager.LoadScene(0);

            _completeTimeText = _completeDisplay.Q<Label>("CompletedTimeText");

            GameSettings.OnPause += GamePauseChanged;
            GameSettings.OnComplete += GameCompleteChanged;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void RenderInventory(InventorySlotUi[] slots)
        {
            _inventoryHolder.Clear();

            foreach(var slot in slots)
            {
                var template = _inventoryIconTemplate.Instantiate();

                var icon = template.Q("DisplayIcon");
                var amountLabel = template.Q<Label>("AmountLabel");

                icon.style.backgroundImage = new StyleBackground(slot.Image);
                amountLabel.text = slot.Amount.ToString();

                _inventoryHolder.Add(template);
            }
        }

        public void SetCompletedTime(TimeSpan time)
        {
            _completeTimeText.text = $"{time:mm}:{time:ss}";
        }

        public void UpdateTimeDisplay(string time)
        {
            _timeLabel.text = time;
        }
    }
}
