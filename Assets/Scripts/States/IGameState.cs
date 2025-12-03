public interface IGameState
{
    void Enter();   // Called when entering this state
    void Exit();    // Called when leaving this state
    void Update();  // Optional: called every frame if needed
}