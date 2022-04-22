using DG.Tweening;
using UnityEngine;

namespace CyberBuggy.TweenControllers
{
    public class Rotate : TweenController
    {
        [SerializeField] private Transform _tweeningTransform;
        [SerializeField] private Vector3 _rotatingTarget;
        public Vector3 RotatingTarget { get => _rotatingTarget; set => _rotatingTarget = value; }
        private Vector3 _originalRotation;

        protected override Tween TriggerTween()
        {
            _mainTween.Kill(true);

            _originalRotation = _tweeningTransform.localEulerAngles;

            Tween tween = _tweeningTransform.DOLocalRotate(_rotatingTarget, _tweenSettings.duration, RotateMode.FastBeyond360);

            if(_tweenSettings.tweenOrientation == TweenSettings.TweenOrientation.From)
                tween = _tweeningTransform.DOLocalRotate(_rotatingTarget, _tweenSettings.duration, RotateMode.FastBeyond360).From();

            return tween;
        }

        protected override Tween RevertTween()
        {
            _mainTween.Kill(true);

            Tween tween = _tweeningTransform.DOLocalRotate(_originalRotation, _tweenSettings.duration, RotateMode.FastBeyond360);

            if(_tweenSettings.tweenOrientation == TweenSettings.TweenOrientation.From)
                tween = _tweeningTransform.DOLocalRotate(_originalRotation, _tweenSettings.duration, RotateMode.FastBeyond360).From();

            return tween;
        }

    }
}