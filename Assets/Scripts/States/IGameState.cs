public interface IGameState
{
    void EnterState();       // Called when this state becomes active
    void ExitState();        // Called when leaving this state
    void UpdateState();      // Optional, for per-frame logic
}