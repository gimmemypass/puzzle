using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace GameScripts.GameCore
{
    [RequireComponent(typeof(MeshRenderer))]
    public class TileArrowView : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public async UniTask Disable()
        {
            var sequence = DOTween.Sequence();
            sequence
                .Append(_meshRenderer.material.DOColor(Color.white, 0.5f))
                .Append(_meshRenderer.material.DOFade(0, 0.25f));
            await sequence;
        }
    }
}