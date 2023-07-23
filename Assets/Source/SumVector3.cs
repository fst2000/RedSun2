using UnityEngine;
public class SumVector3 : IVector3
{
    IVector3 vector1;
    IVector3 vector2;

    public SumVector3(IVector3 vector1, IVector3 vector2)
    {
        this.vector1 = vector1;
        this.vector2 = vector2;
    }
    public void GiveVector3(IVector3Consumer consumer)
    {
        vector1.GiveVector3(new DelegateVector3Consumer(v1 =>
        {
            vector2.GiveVector3(new DelegateVector3Consumer(v2 =>
            {
                consumer.Consume(v1 + v2);
            }));
        }));
    }
}