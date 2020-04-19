using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LPSoft.LD46.Management {
    public class EndScreen : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _message;

        [SerializeField]
        private TextMeshProUGUI _subMessage;
        // Start is called before the first frame update
        void Start()
        {
            _message.text = GameManager.EndMessage;
            _subMessage.text = GameManager.SubMessage;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
