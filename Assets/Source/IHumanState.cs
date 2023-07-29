public interface IHumanState
{
    void Start();
    void Update();
    void Exit();
    IHumanState NextState();
}