using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace CyberBuggy.TweenControllers
{
    public abstract class TweenController : MonoBehaviour
    {
        [SerializeField] protected TweenSettings _tweenSettings;
        public TweenSettings TweenSettings { get => _tweenSettings; set => _tweenSettings = value; }
        protected Tween _mainTween;
        protected Tween MainTween { get => _mainTween; set => _mainTween = value; }


        #region Initialize Methods
        private void Awake()
        {
            if(_tweenSettings.initializeMethod == TweenSettings.InitializeMethod.Awake)
                CallTween();
        }
        private void Start()
        {
            if(_tweenSettings.initializeMethod == TweenSettings.InitializeMethod.Start)
                CallTween();
        }
        private void OnEnable()
        {
            if(_tweenSettings.initializeMethod == TweenSettings.InitializeMethod.OnEnable)
                CallTween();
        }
        private void OnDisable()
        {
            if(_tweenSettings.initializeMethod == TweenSettings.InitializeMethod.OnDisable)
                CallTween();
        }
        #endregion

        public void CallTween(bool reverse = false)
        {
            if(!reverse)
                _mainTween = TriggerTween();
            else
                _mainTween = RevertTween();
            
            _tweenSettings.ApplyTweenSettings(ref _mainTween);
        }
        protected virtual Tween TriggerTween()
        {
            return null;
        }
        protected virtual Tween RevertTween()
        {
            return null;
        }

    }

    [Serializable]
    public class TweenSettings
    {
        public enum InitializeMethod
        {
            None,
            OnEnable,
            Awake,
            Start,
            OnDisable
        }

        public enum TweenOrientation

        {
            To,
            From
        }
      
        [Header("Settings")]
        public InitializeMethod initializeMethod;
        public TweenOrientation tweenOrientation;

        [Header("Update Types")]
        public UpdateType updateType;
        public bool ignoreTimeScale;

        [Header("Loops")]
        public int loopAmount;
        public LoopType loopType;

        [Header("Misc")]
        public bool isRelative;
        public bool isInverted;

        [Header("Values")]
        public float duration = 1;
        public float delay;

        [Header("Easing")]
        public Ease easeType = Ease.OutQuad;
        public float easeAmplitude = 1.70158f;

        public float easePeriod;


        [Header("Events")]
        public UnityEvent onTweenStarted;
        public UnityEvent onTweenFinished;

        public void ApplyTweenSettings(ref Tween tween)
        {
            tween.SetEase(easeType, easeAmplitude, easePeriod)
                 .SetLoops(loopAmount, loopType)
                 .SetDelay(delay)
                 .SetUpdate(updateType, ignoreTimeScale)
                 .SetRelative(isRelative)
                 .SetInverted(isInverted)
                 .OnPlay(() => onTweenStarted?.Invoke())
                 .OnComplete(() => onTweenFinished?.Invoke()); 
        }

    }
}