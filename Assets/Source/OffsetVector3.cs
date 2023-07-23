using UnityEngine;
public class OffsetVector3 : IVector3
{
    IVector3 position;
    IVector3 offset;
    public OffsetVector3(IVector3 position, IVector3 offset)
    {
        this.position = position;
        this.offset = offset;
    }
    public void GiveVector3(IVector3Consumer consumer)
    {
        offset.GiveVector3(new DelegateVector3Consumer(offseetV =>
        {
            position.GiveVector3(new DelegateVector3Consumer(positionV =>
            {
                consumer.Consume(positionV + offseetV);
            }));
        }));
    }
}