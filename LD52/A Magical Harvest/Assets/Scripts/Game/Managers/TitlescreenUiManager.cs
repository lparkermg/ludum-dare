using Game.Global;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Game.Managers
{
    public class TitlescreenUiManager : MonoBehaviour
    {
        [SerializeField]
        private UIDocument _ui;

        private Button _startButton;
        private Button _exitButton;

        void Awake()
        {
            _startButton = _ui.rootVisualElement.Q<Button>("StartButton");
            _exitButton = _ui.rootVisualElement.Q<Button>("ExitButton");
            GameSettings.SetComplete(false);
        }

        // Start is called before the first frame update
        void Start()
        {
            _startButton = _ui.rootVisualElement.Q<Button>("StartButton");
            _startButton.clicked += () => SceneManager.LoadScene(1);

            _exitButton = _ui.rootVisualElement.Q<Button>("ExitButton");
            _exitButton.clicked += () => Application.Quit();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
