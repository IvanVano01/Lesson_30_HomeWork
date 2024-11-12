using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Image _imageWin;
    [SerializeField] private Image _imageLost;
    private GameController _gamecontroller;

    public void Initialize(GameController gamecontroller)
    {
        _gamecontroller = gamecontroller;
        _gamecontroller.GameFinished += OnGameFinished;
    }

    private void OnDestroy()
    {
        _gamecontroller.GameFinished -= OnGameFinished;
    }

    private void OnGameFinished(bool win, bool lost)
    {
        Show(win, lost);
    }

    public void Hide()
    {
        _imageWin.gameObject.SetActive(false);
        _imageLost.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }

    public void Show(bool win, bool lost)
    {
        gameObject.SetActive(true);

        _imageWin.gameObject.SetActive(win);
        _imageLost.gameObject.SetActive(lost);
    }
}
