using UnityEngine;
using System.Collections;

namespace QuickAndDirty.Splash
{
    public class KillParticlesSphere : ParticleAffector
    {
        public float radius = 1.0f;
        public Vector3 center;

        public float lifeDrain = 2.0f;

        public override void AffectParticle(ref ParticleSystem.Particle p, float dt)
        {
            Vector3 pos = p.position;
            if (particleController.IsWorldSpace())
            {
                pos = transform.InverseTransformPoint(pos);
            }

            if ((pos - center).sqrMagnitude < radius * radius)
            {
                p.lifetime -= lifeDrain * dt;
            }
        }

        void OnDrawGizmosSelected()
        {
            if (enabled)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.TransformPoint(center), radius);
            }
        }
    }
}
