using UnityEngine;
using System.Collections;

namespace QuickAndDirty.Splash
{
    public class BoxConstraint : ParticleAffector
    {
        public Bounds bounds = new Bounds(new Vector3(0,0,0), new Vector3(1,1,1));

        public override void AffectParticle(ref ParticleSystem.Particle p, float dt)
        {
            Vector3 position = p.position;
            if (particleController.IsWorldSpace())
            {
                position = transform.InverseTransformPoint(position);
            }

            position.x = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
            position.y = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);
            position.z = Mathf.Clamp(position.z, bounds.min.z, bounds.max.z);

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

                Vector3[] top = { bounds.min,
                                   bounds.min + new Vector3(bounds.size.x, 0, 0),
                                   bounds.min + new Vector3(bounds.size.x, 0, bounds.size.z),
                                   bounds.min + new Vector3(0, 0, bounds.size.z) };
                Vector3[] bottom = { 
                                   bounds.max - new Vector3(bounds.size.x, 0, bounds.size.z),
                                   bounds.max - new Vector3(0, 0, bounds.size.z),
                                   bounds.max,
                                   bounds.max - new Vector3(bounds.size.x, 0, 0) };
                Gizmos.DrawLine(transform.TransformPoint(top[0]), transform.TransformPoint(bottom[0]));
                Gizmos.DrawLine(transform.TransformPoint(top[3]), transform.TransformPoint(top[0]));
                Gizmos.DrawLine(transform.TransformPoint(bottom[3]), transform.TransformPoint(bottom[0]));
                for (int i = 1; i < 4; ++i)
                {
                    Gizmos.DrawLine(transform.TransformPoint(top[i - 1]), transform.TransformPoint(top[i]));
                    Gizmos.DrawLine(transform.TransformPoint(bottom[i - 1]), transform.TransformPoint(bottom[i]));
                    Gizmos.DrawLine(transform.TransformPoint(top[i]), transform.TransformPoint(bottom[i]));
                }
            }
        }

    }
}
