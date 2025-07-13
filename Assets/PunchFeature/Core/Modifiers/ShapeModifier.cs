using PunchFeature.Infrastructure.ObjectPools;
using UnityEngine;

namespace PunchFeature.Core.Modifiers
{
    public class ShapeModifier
    {
        private bool _isAnimating;
        private float _currentTime;
        private float _animationSpeed;
        private Transform _targetTransform;
        private AnimationCurve _stretchCurve;
        private Vector3 _originalScale;
        private Vector3 _punchDirection;

        public void Tick()
        {
            if (!_isAnimating) return;

            _currentTime += Time.deltaTime * _animationSpeed;
            float evaluatedStretchValue = _stretchCurve.Evaluate(_currentTime);
        
            ApplyStretchEffect(evaluatedStretchValue);
        
            if (_currentTime >= _stretchCurve[_stretchCurve.length - 1].time)
            {
                _targetTransform.localScale = _originalScale;
                _isAnimating = false;
            }
        }

        public void StartChangeShape(Transform transform, Vector3 direction, float speed, AnimationCurve shapeCurve)
        {
            if (_isAnimating)
            {
                _targetTransform.localScale = _originalScale;
            }
        
            _targetTransform = transform;
            _originalScale = _targetTransform.localScale;
            _punchDirection = direction;
            _animationSpeed = speed;
            _stretchCurve = shapeCurve;
        
            _isAnimating = true;
            _currentTime = 0f;
        }
    
        private void ApplyStretchEffect(float curveValue)
        {
            Vector3 punchAxis = GetDominantAxis(_punchDirection);
        
            Vector3 newScale = _originalScale;
            newScale -= punchAxis * curveValue;
        
            Vector3 perpendicularAxes = Vector3.one - punchAxis;
            newScale += perpendicularAxes * (curveValue * 5f);

            _targetTransform.localScale = newScale;
        }
    
        private Vector3 GetDominantAxis(Vector3 direction)
        {
            float absX = Mathf.Abs(direction.x);
            float absY = Mathf.Abs(direction.y);
            float absZ = Mathf.Abs(direction.z);

            if (absX > absY && absX > absZ)
                return new Vector3(1, 0, 0);
        
            if (absY > absZ)
                return new Vector3(0, 1, 0);
            else
                return new Vector3(0, 0, 1);
        }
    }
}