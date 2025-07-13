using UnityEngine;

namespace PunchFeature.Core.Modifiers
{
    public class ColorModifier
    {
        private static readonly int ColorId = Shader.PropertyToID("_Color");
    
        private bool _isAnimating;
        private float _currentTime;
        private float _animationSpeed;
        private AnimationCurve _colorCurve;
        private Color _originalColor;
        private Color _targetColor;
        private Renderer _renderer;

        public void Tick()
        {
            if (!_isAnimating) return;

            _currentTime += Time.deltaTime * _animationSpeed;
            float evaluatedColorValue = _colorCurve.Evaluate(_currentTime);
        
            ApplyColorEffect(evaluatedColorValue);
        
            if (_currentTime >= _colorCurve[_colorCurve.length - 1].time)
            {
                _renderer.material.color = _originalColor;
                _isAnimating = false;
            }
        }

        public void StartChangeColor(Renderer renderer, Color targetColor, float speed, AnimationCurve shapeCurve)
        {
            if (_isAnimating)
            {
                _renderer.material.color = _originalColor;
            }
        
            _renderer = renderer;
            _originalColor = renderer.material.color;
            _animationSpeed = speed;
            _colorCurve = shapeCurve;
            _targetColor = targetColor;
        
            _isAnimating = true;
            _currentTime = 0f;
        }
    
        private void ApplyColorEffect(float colorValue)
        {
            Color currentColor = Color.Lerp(_originalColor, _targetColor, colorValue);
            _renderer.material.SetColor(ColorId, currentColor);
        }
    }
}