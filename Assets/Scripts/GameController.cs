using System;

public class GameController 
{
    public event Action<bool> GameFinished;
   
    private IConditions _currentConditionVictory;
    private IConditions _currentConditionDefeat;

    public GameController()   
    { 
        StartGame();
    }

    public bool IsRunningGame { get; private set; }   

    public void Update()
    {
        if (IsRunningGame == false)
            return;
        
    }

    public void StartGame()
    { 
        IsRunningGame = true;
    }   

    public void SetConditionVictory(IConditions conditionVictory)
    {
        if (conditionVictory == null)
            throw new ArgumentNullException(nameof(conditionVictory));

        if (_currentConditionVictory != null)
        {
            _currentConditionVictory.—onditionfulfilled -= On—onditionfulfilledVictory;
            _currentConditionVictory.OnDisable();
        }

        _currentConditionVictory = conditionVictory;
        _currentConditionVictory.OnEnable();
        _currentConditionVictory.—onditionfulfilled += On—onditionfulfilledVictory;
    }

    public void SetConditionDefaet(IConditions conditionDefeat)
    {
        if (conditionDefeat == null)
            throw new ArgumentNullException(nameof(conditionDefeat));

        if (_currentConditionDefeat != null)
        {
            _currentConditionDefeat.—onditionfulfilled -= On—onditionfulfilledDefeat;
            _currentConditionDefeat.OnDisable();
        }

        _currentConditionDefeat = conditionDefeat;
        _currentConditionDefeat.OnEnable();
        _currentConditionDefeat.—onditionfulfilled += On—onditionfulfilledDefeat;
    }

    private void WinGame()
    {
        GameOver(true);
    }

    private void DefeatGame()
    {
        GameOver(false);
    }

    private void On—onditionfulfilledVictory()
    {
        WinGame();
    }

    private void On—onditionfulfilledDefeat()
    {
        DefeatGame();
    }    

    private void GameOver(bool isWin)
    {
        IsRunningGame = false;
        GameFinished?.Invoke(isWin);        
    }    
}
