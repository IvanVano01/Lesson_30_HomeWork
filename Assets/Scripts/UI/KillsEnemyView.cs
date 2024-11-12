using TMPro;
using UnityEngine;

public class KillsEnemyView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberOfKillText;
    private SpawnerEnemy _spawnerEnemy;
    private int _numberOfKilledEnemy;

    public void Initialize(SpawnerEnemy spawnerEnemy)
    {
        _spawnerEnemy = spawnerEnemy;
        _spawnerEnemy.KilledEnemy += OnKilledEnemy;

        _numberOfKilledEnemy = 0;
    }

    private void OnDestroy()
    {
        _spawnerEnemy.KilledEnemy -= OnKilledEnemy;
    }

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);

    private void OnKilledEnemy()
    {
        _numberOfKilledEnemy++;
        _numberOfKillText.text = _numberOfKilledEnemy.ToString();
    }
}
