using ECS;

namespace CompositeRoot
{
    public class GameState : IGameState
    {
        private readonly Loader _loader;

        public GameState(Loader loader)
        {
            _loader = loader;
        }

        public void StopGame()
        {
            _loader.Stop();
        }

        public void RestartGame()
        {
            _loader.Restart();
        }
    }
}

