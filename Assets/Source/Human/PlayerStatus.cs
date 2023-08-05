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
        BoolFunc isArmedFunc = null;
        isArmed.Accept(b => isArmedFunc = bAction => bAction(b));
        return isArmedFunc;
    }

    public BoolFunc IsCrouching()
    {
        return bAction => bAction(Input.GetKey(KeyCode.LeftShift));
    }
}