using DG.Tweening;
using UnityEngine;

namespace CyberBuggy.TweenControllers
{
    public class SpriteFade : TweenController
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] [Range(0,1)] private float _fadeEndValue;
        public float FadeEndValue { get => _fadeEndValue; set => _fadeEndValue = value; }
        private float _originalFadeValue;

        protected override Tween TriggerTween()
        {
            _mainTween.Kill(true);
            
            _originalFadeValue = _spriteRenderer.color.a;

            Tween tween = _spriteRenderer.DOFade(_fadeEndValue, _tweenSettings.duration);

            if(_tweenSettings.tweenOrientation == TweenSettings.TweenOrientation.From)
                tween = _spriteRenderer.DOFade(_fadeEndValue, _tweenSettings.duration).From();

            return tween;
        }

        protected override Tween RevertTween()
        {
            _mainTween.Kill(true);

            Tween tween = _spriteRenderer.DOFade(_originalFadeValue, _tweenSettings.duration);

            if(_tweenSettings.tweenOrientation == TweenSettings.TweenOrientation.From)
                tween = _spriteRenderer.DOFade(_originalFadeValue, _tweenSettings.duration).From();

            return tween;
        }
    }
}