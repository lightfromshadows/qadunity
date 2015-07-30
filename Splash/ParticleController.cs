using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace QuickAndDirty.Splash
{
    enum K : int { MAX_PARTICLES = 65536 }

    /*
     * A centralized "controller" for specialized particle initializers, affectors, and constraints 
     * that guarantees correct execution order between components.
     * 
     * It is assumed that this component will be on the same node as the ParticleSystem affected.
     */

    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleController : MonoBehaviour
    {

        public ParticleSystem particles { get; private set; }
        public bool useFixedTime = false;

        public List<ParticleSystem.Particle> tempParticles = new List<ParticleSystem.Particle>();

        public List<ParticleAffector> affectors { get; private set; }

        private bool _isWorldSpace = false;

        void Awake()
        {
            particles = gameObject.GetComponent<ParticleSystem>();
            affectors = new List<ParticleAffector>();
        }

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (!useFixedTime && Time.timeScale > 0)
            {
                DoUpdate(Time.deltaTime);
            }
        }

        void LateUpdate()
        {
            if (useFixedTime && Time.timeScale > 0)
            {
                DoUpdate(Time.fixedDeltaTime);
            }
        }

        void DoUpdate(float deltaTime)
        {
            _isWorldSpace = particles.simulationSpace == ParticleSystemSimulationSpace.World;

            //InitializePendingParticles();
            AcquireParticleArray();

            UpdateAffectors(deltaTime);

            ReleaseParticleArray();
        }

        void AcquireParticleArray()
        {
            ParticleSystem.Particle[] systemParticles = new ParticleSystem.Particle[particles.particleCount];
            particles.GetParticles(systemParticles);
            //Debug.Log("Acquire " + systemParticles.Length);
            tempParticles.AddRange(systemParticles);
        }

        void ReleaseParticleArray()
        {
            particles.SetParticles(tempParticles.ToArray(), tempParticles.Count);
            //Debug.Log("Release " + tempParticles.Count);
            tempParticles.Clear();
        }

        void InitializePendingParticles()
        {
            throw new System.NotImplementedException();
        }

        void UpdateAffectors(float dt)
        {
            foreach (ParticleAffector affector in affectors)
            {
                if (affector.enabled)
                    affector.AffectParticles(tempParticles, dt);
            }
        }

        // You have to use these functions instead of calling it directly if you want to use QuickAndDirty ParticleInitializers
        // Affectors and Constraints should work just fine without this special wankery so I'm not putting that much work into the area atm.
        public void Emit(int count)
        {
            throw new System.NotImplementedException();
        }
        public void Emit(Vector3 position, Vector3 velocity, float size, float lifetime, Color32 color)
        {
            throw new System.NotImplementedException();
        }
        public void Emit(ParticleSystem.Particle p)
        {
            throw new System.NotImplementedException();
        }


        public void Add(ParticleAffector affector) {
            if (!affectors.Contains(affector)) {
                affectors.Add(affector);
                affector.SetParticleController(this);
                affectors.Sort();
            }
        }
        public void Remove(ParticleAffector affector) {
            affectors.Remove(affector);
            affectors.Sort();
        }


        public void TransformParticle(ref ParticleSystem.Particle p)
        {
            p.position = transform.TransformPoint(p.position);
            p.velocity = transform.TransformVector(p.velocity);
            p.axisOfRotation = transform.TransformDirection(p.axisOfRotation);
        }

        public void InverseTransformParticle(ref ParticleSystem.Particle p)
        {
            p.position = transform.InverseTransformPoint(p.position);
            p.velocity = transform.InverseTransformVector(p.velocity);
            p.axisOfRotation = transform.InverseTransformDirection(p.axisOfRotation);
        }

        public bool IsWorldSpace()
        {
            return _isWorldSpace;
        }
    }
}