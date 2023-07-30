using UnityEngine;
public class PlayerStatus : IHumanStatus
{
    public BoolFunc isAiming()
    {
        return bAction => bAction(Input.GetMouseButton(1));
    }

    public BoolFunc isArmed()
    {
        return bAction => bAction(Input.GetMouseButtonDown(0));
    }

    public BoolFunc isCrouching()
    {
        return bAction => bAction(Input.GetKey(KeyCode.LeftControl));
    }
}