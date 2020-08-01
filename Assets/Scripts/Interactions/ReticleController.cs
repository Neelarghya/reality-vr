using UnityEngine;

namespace Interactions
{
    [RequireComponent(typeof(Animator))]
    public class ReticleController : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int IsOpen = Animator.StringToHash("isOpen");
        private static readonly int Click = Animator.StringToHash("click");

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void ActivateReticle()
        {
            _animator.SetBool(IsOpen, true);
        }

        public void DeactivateReticle()
        {
            _animator.SetBool(IsOpen, false);
        }

        public void ClickReticle()
        {
            _animator.SetTrigger(Click);
        }
    }
}
