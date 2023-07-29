public class HumanAimState : IHumanState
{
    HumanState walkAimState;
    HumanState crouchAimState;
    IHumanState currentState;

    public HumanAimState(HumanState walkAimState, HumanState crouchAimState)
    {
        this.walkAimState = walkAimState;
        this.crouchAimState = crouchAimState;

        currentState = walkAimState();
    }

    public void Start()
    {
        throw new System.NotImplementedException();
    }
    public void Update()
    {
        throw new System.NotImplementedException();
    }
    public void Exit()
    {
        throw new System.NotImplementedException();
    }
    public IHumanState NextState()
    {
        throw new System.NotImplementedException();
    }
}