public class ConditionSwitcher 
{
    private GameDefeat _gameDefeat;
    private GameVictory _gameVictory;

    private GameController _gameController;
    private ConditionFactory _conditionFactory;

    public ConditionSwitcher(GameDefeat gameDefeat, GameVictory gameVictory, GameController gameController, ConditionFactory conditionFactory)
    {
        _gameDefeat = gameDefeat;
        _gameVictory = gameVictory;
        _gameController = gameController;
        _conditionFactory = conditionFactory;

        SwitchConditions();
    }

    public void SwitchConditions()
    {
        _gameController. SetConditionVictory(_conditionFactory.GetConditionsVictory(_gameVictory));
        _gameController. SetConditionDefaet(_conditionFactory.GetConditionsDefeat(_gameDefeat));
    }
}
