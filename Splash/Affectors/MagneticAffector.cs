using UnityEngine;
using System.Collections;

namespace QuickAndDirty.Splash
{
    public class MagneticAffector : ParticleAffector
    {
        public float sizeMassCoef = 2.0f;
        public Vector3 center;

        public FloatRange accelLimits = new FloatRange(-200, 200);

        public override void AffectParticle(ref ParticleSystem.Particle p, float dt)
        {
            Vector3 pole = particleController.IsWorldSpace() ? transform.TransformPoint(center) : center;
            Vector3 d = pole - p.position;
            p.velocity += d * accelLimits.Clamp(((p.size * sizeMassCoef) / d.sqrMagnitude)) * 96.04f * dt;
        }

        public void OnDrawGizmosSelected()
        {
            if (enabled)
            {
                Vector3 up = new Vector3(0, 0.25f, 0);
                Vector3 right = new Vector3(0.25f, 0, 0);
                Vector3 forward = new Vector3(0, 0, 0.25f);
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(transform.TransformPoint(center - up), transform.TransformPoint(center + up));
                Gizmos.DrawLine(transform.TransformPoint(center - right), transform.TransformPoint(center + right));
                Gizmos.DrawLine(transform.TransformPoint(center - forward), transform.TransformPoint(center + forward));
            }
        }
    }
}
