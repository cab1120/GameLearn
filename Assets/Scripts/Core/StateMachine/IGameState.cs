public interface IGameState
{
    // 当我们进入这个状态时，调用此方法进行初始化
    void Enter();
    // 当这个状态正在进行时，每帧调用此方法（像Update）
    void Execute();
    // 当我们离开这个状态时，调用此方法进行清理
    void Exit();
}
