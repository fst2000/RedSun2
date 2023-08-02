using UnityEngine;
public class PlayerStatus : IHumanStatus
{
    public BoolFunc IsAiming()
    {
        return bAction => bAction(Input.GetMouseButton(1));
    }

    public BoolFunc IsArmed()
    {
        return bAction => bAction(Input.GetMouseButton(0));
    }

    public BoolFunc IsCrouching()
    {
        return bAction => bAction(Input.GetKey(KeyCode.LeftShift));
    }
}