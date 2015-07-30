using UnityEngine;
using System.Collections.Generic;

namespace QuickAndDirty.Splash
{
    [RequireComponent(typeof(ParticleController))]
    public class ParticleAffector : MonoBehaviour, System.IComparable<ParticleAffector>
    {
        public int order = 0;

        protected virtual void Start()
        {
            
            GetComponent<ParticleController>().Add(this);
        }

        public virtual void Remove()
        {
            particleController.Remove(this);
            Destroy(this);
        }
        public virtual void AffectParticles(List<ParticleSystem.Particle> particles, float dt)
        {
            int len = particles.Count;
            for (int i = 0; i < len; ++i)
            {
                ParticleSystem.Particle p = particles[i];
                AffectParticle(ref p, dt);
                particles[i] = p;
            }
        }

        public virtual void AffectParticle(ref ParticleSystem.Particle p, float dt)
        {
            Debug.Log("Don't stick the base class on an object!");
        }

        protected ParticleController particleController { get; private set; }

        public void SetParticleController(ParticleController controller)
        {
            particleController = controller;
        }

        public int CompareTo(ParticleAffector other)
        {
            return order - other.order;
        }
    }
}