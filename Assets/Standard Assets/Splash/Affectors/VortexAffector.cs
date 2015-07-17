using UnityEngine;
using System.Collections.Generic;
using QuickAndDirty;

/*
 * Uniform linear acceleration around an axis (up)
 */

public class VortexAffector : QuickAndDirty.Splash.ParticleAffector {

    public Vector3 up = Vector3.forward;

    public bool acceleration = true;

    public float linearTangentalAcceleration = 0.5f;
    public float centrifugalCoef = 1.0f;

    public Vector3 centerOffset = Vector3.zero;

    public override void AffectParticle(ref ParticleSystem.Particle p, float dt)
    {
        // This math is much easier in local space
        if (particleController.IsWorldSpace()) {
            particleController.InverseTransformParticle(ref p);
        }

        Vector3 r = p.position - centerOffset;
        float radius = r.magnitude;
        Vector3 ac = Vector3.Cross(up, r.normalized);
        ac.Normalize();
        

        if (acceleration)
        {
            ac *= linearTangentalAcceleration * dt;
            Vector3 cf = ((r * ac.sqrMagnitude) / radius) * centrifugalCoef;
            p.velocity += ac + cf;
        }
        else
        {
            ac *= linearTangentalAcceleration;
            p.velocity = ac;
        }

        if (particleController.IsWorldSpace()) {
            particleController.TransformParticle(ref p);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (enabled)
        {
            Vector3 up = new Vector3(0, 0.25f, 0);
            Vector3 right = new Vector3(0.25f, 0, 0);
            Vector3 forward = new Vector3(0, 0, 0.25f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.TransformPoint(centerOffset - up), transform.TransformPoint(centerOffset + up));
            Gizmos.DrawLine(transform.TransformPoint(centerOffset - right), transform.TransformPoint(centerOffset + right));
            Gizmos.DrawLine(transform.TransformPoint(centerOffset - forward), transform.TransformPoint(centerOffset + forward));
        }
    }
}
