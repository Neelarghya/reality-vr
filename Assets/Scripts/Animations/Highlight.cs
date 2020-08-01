using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animations
{
    public class Highlight : MonoBehaviour
    {
        [SerializeField] private float transitionTime = 1f;
        private Vector3 _originalScale;
        private Coroutine _transition;

        private void Awake()
        {
            _originalScale = transform.localScale;
        }

        public void Scale(float scale)
        {
            StartTransition(scale);
        }

        public void ScaleAndReset(float scale)
        {
            float currentScale = transform.localScale.x / _originalScale.x;

            StartTransition(new List<float> {scale, currentScale});
        }

        public void ResetScale()
        {
            StartTransition(1);
        }

        private void StartTransition(float scale)
        {
            StartTransition(new List<float> {scale});
        }

        private void StartTransition(List<float> scales)
        {
            if (_transition != null) StopCoroutine(_transition);

            _transition = StartCoroutine(Transition(scales, transitionTime));
        }

        private IEnumerator Transition(List<float> scales, float delay)
        {
            float fractionalDelay = delay / scales.Count;

            foreach (float scale in scales)
            {
                float time = 0;
                Vector3 vectorScale = _originalScale * scale;

                while (time <= fractionalDelay)
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, vectorScale, time / fractionalDelay);
                    time += Time.deltaTime;
                    yield return null;
                }
            }
        }
    }
}