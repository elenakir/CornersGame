using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _panelInfo;
    [SerializeField] private Image _blockImage;
    [SerializeField] private ToggleGroup _toggleGroup;
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonRestart;
    [SerializeField] private Button _buttonExit;
    [SerializeField] private Button _buttonExitWinPanel;
    [SerializeField] private Button _buttonRestartWinPanel;
    [SerializeField] private TextMeshProUGUI _currentPlayer;
    [SerializeField] private TextMeshProUGUI _turnCountPlayer1;
    [SerializeField] private TextMeshProUGUI _turnCountPlayer2;
    [SerializeField] private TextMeshProUGUI _winner;

    [Header("Rules")]
    [SerializeField] private Rule _jumpForward;
    [SerializeField] private Rule _jumpDiagonal;
    [SerializeField] private Rule _step;

    [Header("Animations")]
    [SerializeField] private Animator _settingsPanelAnimator;
    [SerializeField] private Animator _winPanelAnimator;

    [HideInInspector] public Rule CurrentRule => currentRule;

    private Rule currentRule;

    private void Awake()
    {
        currentRule = _step;
        _panelInfo.SetActive(false);
        _turnCountPlayer1.text = "0";
        _turnCountPlayer2.text = "0";

        _winPanelAnimator.speed = 0;

        _buttonStart.onClick.AddListener(() => 
        {
            switch (_toggleGroup.ActiveToggles().FirstOrDefault().name)
            {
                case "JumpForward":
                    {
                        currentRule = _jumpForward;
                        break;
                    }
                case "JumpDiagonal":
                    {
                        currentRule = _jumpDiagonal;
                        break;
                    }
                case "Step":
                    {
                        currentRule = _step;
                        break;
                    }
            }
            currentRule.Init();
            _blockImage.enabled = false;
            _panelInfo.SetActive(true);
            _settingsPanelAnimator.SetBool("btnClicked", true);
        });

        _buttonRestart.onClick.AddListener(() => { SceneManager.LoadScene(0); });
        _buttonRestartWinPanel.onClick.AddListener(() => { SceneManager.LoadScene(0); });
        _buttonExit.onClick.AddListener(() => { Application.Quit(); });
        _buttonExitWinPanel.onClick.AddListener(() => { Application.Quit(); });
    }

    private void Update()
    {
        if (currentRule != null) currentRule.Run();

        if (Player.IsWhite)
        {
            _currentPlayer.text = "Игрок №1";
            _currentPlayer.color = Color.white;
        }
        else
        {
            _currentPlayer.text = "Игрок №2";
            _currentPlayer.color = Color.black;
        }

        _turnCountPlayer1.text = Player.TurnCount1.ToString();
        _turnCountPlayer2.text = Player.TurnCount2.ToString();

        if (Player.IsWin)
        {
            if (!Player.IsWhite) _winner.text = "Игрок №1";
            else _winner.text = "Игрок №2";

            _winPanelAnimator.speed = 1;
            _blockImage.enabled = true;
        }
    }
}
