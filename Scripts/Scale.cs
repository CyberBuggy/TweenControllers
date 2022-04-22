using UnityEngine;
using DG.Tweening;

namespace CyberBuggy.TweenControllers
{
    public class Scale : TweenController
    {
        [SerializeField] private Transform _tweeningTransform;
        [SerializeField] private Vector3 _scaleTarget;
        public Vector3 ScaleTarget { get => _scaleTarget; set => _scaleTarget = value; }
        private Vector3 _originalScale;

        protected override Tween TriggerTween()
        {
            _mainTween.Kill(true);
            
            _originalScale = _tweeningTransform.localScale;

            Tween tween = _tweeningTransform.DOScale(_scaleTarget, _tweenSettings.duration);

            if(_tweenSettings.tweenOrientation == TweenSettings.TweenOrientation.From)
                tween = _tweeningTransform.DOScale(_scaleTarget, _tweenSettings.duration).From();

            return tween;
        }

        protected override Tween RevertTween()
        {
            _mainTween.Kill(true);

            Tween tween = _tweeningTransform.DOScale(_originalScale, _tweenSettings.duration);

            if(_tweenSettings.tweenOrientation == TweenSettings.TweenOrientation.From)
                tween = _tweeningTransform.DOScale(_originalScale, _tweenSettings.duration).From();

            return tween;
        }

    }
}