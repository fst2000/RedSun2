public interface IHumanState
{
    void Start();
    void Update();
    IHumanState NextState();
}