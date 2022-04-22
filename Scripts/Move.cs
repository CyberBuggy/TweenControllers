using DG.Tweening;
using UnityEngine;

namespace CyberBuggy.TweenControllers
{
    public class Move : TweenController
    {
        [SerializeField] private Transform _tweeningTransform;
        [SerializeField] private Vector3 _targetDestination;
        public Vector3 TargetDestination { get => _targetDestination; set => _targetDestination = value; }
        private Vector3 _originalPosition;

        protected override Tween TriggerTween()
        {
            _mainTween.Kill(true);

            _originalPosition = _tweeningTransform.localPosition;

            Tween tween = _tweeningTransform.DOLocalMove(_targetDestination, _tweenSettings.duration);    

            if(_tweenSettings.tweenOrientation == TweenSettings.TweenOrientation.From)
                tween = _tweeningTransform.DOLocalMove(_targetDestination, _tweenSettings.duration).From();

            return tween;
        }

        protected override Tween RevertTween()
        {
            _mainTween.Kill(true);

            Tween tween = _tweeningTransform.DOLocalMove(_originalPosition, _tweenSettings.duration);

            if(_tweenSettings.tweenOrientation == TweenSettings.TweenOrientation.From)
                tween = _tweeningTransform.DOLocalMove(_originalPosition, _tweenSettings.duration).From();

            return tween;
        }

    }
}