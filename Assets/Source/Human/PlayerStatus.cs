using UnityEngine;
public class PlayerStatus : IHumanStatus
{
    IBool isArmed = new SwitchBool(new FuncBool(bAction => bAction(Input.GetKeyDown(KeyCode.Space))));
    public BoolFunc IsAiming()
    {
        return bAction => bAction(Input.GetMouseButton(1));
    }

    public BoolFunc IsArmed()
    {
        return  bAction => bAction(true);
    }

    public BoolFunc IsCrouching()
    {
        return bAction => bAction(Input.GetKey(KeyCode.LeftShift));
    }
}