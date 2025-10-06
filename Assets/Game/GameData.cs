public class GameData
{
    public Observer<int> score = new Observer<int>();
    public Observer<float> endTime = new Observer<float>();
    public Observer<int> moveCount = new Observer<int>();
    public Observer<GameStateEnum> gameState = new Observer<GameStateEnum>();

    public void Reset()
    {
        score.Value = 0;
        endTime.Value = 0f;
        moveCount.Value = 0;
        gameState.Value = GameStateEnum.None;
    }
}