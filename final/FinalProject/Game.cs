public class Game
{
    private Colony _colony;
    private int _targetDays;
    private bool _gameOver;

    public Game()
    {
        _colony = new Colony();
        _targetDays = 30;
    }

    public void Run()
    {
        while (_colony.GetDaysSurvived() < _targetDays)
        {
            // Advance the game state by one day
            _colony.AdvanceDay();
            // Display the current state of the colony
            _colony.logColonyStatus();
            // Check for game over conditions
            if (_gameOver)
            {
                break;
            }
        }
    }
}
