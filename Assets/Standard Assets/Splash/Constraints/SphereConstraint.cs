using UnityEngine;
using System.Collections;

namespace QuickAndDirty.Splash
{
    public class SphereConstraint : ParticleAffector
    {
        public float radius = 1;
        public Vector3 center = new Vector3(0, 0, 0);
        public bool killVelocity = false;

        public override void AffectParticle(ref ParticleSystem.Particle p, float dt)
        {
            Vector3 position = p.position;
            if (particleController.IsWorldSpace())
            {
                position = transform.InverseTransformPoint(position);
            }
            Vector3 d = position - center;
            if (d.sqrMagnitude > (radius * radius))
            {
                position = center + d.normalized * radius;
                if (killVelocity)
                {
                    p.velocity = Vector3.zero;
                }
            }

            if (particleController.IsWorldSpace())
            {
                position = transform.TransformPoint(position);
            }
            p.position = position;
        }

        void OnDrawGizmosSelected()
        {
            if (enabled)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.TransformPoint(center), radius);
            }
        }
    }
}
