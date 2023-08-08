using UnityEngine;
public class HitParticle : IParticle
{
    IVector3 position;
    IVector3 direction;
    public HitParticle(IVector3 position, IVector3 direction)
    {
        this.position = position;
        this.direction = direction;
    }
    public void Show()
    {
        position.Accept(pos =>
        {
            GameObject.Instantiate(Resources.Load<ParticleSystem>("Particles/HitParticles"), pos, Quaternion.LookRotation(Vector3.up));
            direction.Accept(dir =>
            {
                //GameObject.Instantiate(Resources.Load<ParticleSystem>("Particles/HitParticles"), pos, Quaternion.LookRotation(dir));
            });
        });
    }
}