using PunchFeature.Core.Modifiers;
using PunchFeature.Infrastructure.ObjectPools;
using UnityEngine;

namespace PunchFeature.Core
{
    [RequireComponent(typeof(Rigidbody))]
    public class PunchReceiver : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
    
        [SerializeField] private bool _isChangesShape;
        [SerializeField] private AnimationCurve _shapeCurve;
        [SerializeField] private float _shapeAnimationSpeed;
    
        [SerializeField] private bool _isChangeColor;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private AnimationCurve _colorCurve;
        [SerializeField] private Color _onHeatColor;
        [SerializeField] private float _colorAnimationSpeed;
    
        [SerializeField] private bool _isShowParticles;
        [SerializeField] private ParticleSystem _particleSystem;
    
        private ShapeModifier _shapeModifier;
        private ColorModifier _colorModifier;
        private IObjectPool<ParticleSystem> _particlesPool;

        public void GetPunch(Vector3 position, Vector3 direction)
        {
            _rigidbody.AddForceAtPosition(direction, position, ForceMode.Impulse);

            if (_isChangesShape)
            {
                _shapeModifier.StartChangeShape(transform, direction, _shapeAnimationSpeed, _shapeCurve);
            }

            if (_isChangeColor)
            {
                _colorModifier.StartChangeColor(_renderer, _onHeatColor, _colorAnimationSpeed, _colorCurve);
            }

            if (_isShowParticles)
            {
                ShowParticles(position);
            }
        }
    
        private void Awake()
        {
            _rigidbody ??= GetComponent<Rigidbody>();
            _shapeModifier = new();
            _colorModifier = new();
            _particlesPool = new ObjectPool<ParticleSystem>(_particleSystem, 1);
        }

        private void Update()
        {
            if (_isChangesShape)
            {
                _shapeModifier.Tick();
            }

            if (_isChangeColor)
            {
                _colorModifier.Tick();
            }
        }

        private void ShowParticles(Vector3 atposition)
        {
            ParticleSystem particle = _particlesPool.Get();
            particle.transform.position = atposition;
            particle.Play();
        }
    }
}