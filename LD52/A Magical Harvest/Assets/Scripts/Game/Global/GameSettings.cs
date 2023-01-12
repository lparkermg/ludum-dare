namespace Game.Global
{
    public static class GameSettings
    {
        public static bool IsPaused { get; private set; }

        public static bool IsComplete { get; private set; }

        public delegate void OnPauseHandler(bool paused);
        public static event OnPauseHandler OnPause;

        public delegate void OnCompleteHandler(bool complete);
        public static event OnCompleteHandler OnComplete;

        public static void SetPause(bool isPaused)
        {
            IsPaused = isPaused;
            OnPause?.Invoke(isPaused);
        }

        public static void SetComplete(bool isComplete)
        {
            IsComplete = isComplete;
            OnComplete?.Invoke(isComplete);
        }
    }
}
