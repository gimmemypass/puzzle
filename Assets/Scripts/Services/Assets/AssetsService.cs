using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Services.Assets.Containers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using Object = UnityEngine.Object;

namespace Services.Assets
{
    public class AssetsService : IAssetsService
    {
        private const int MaxRetryDelay = 60;

        private readonly DiContainer _diContainer;

        private readonly Dictionary<AssetReference, UniTask> _assetsLoadsMap =
            new Dictionary<AssetReference, UniTask>();
        private readonly Dictionary<AssetReference, object> _assetsContainersCache = 
            new Dictionary<AssetReference, object>();
        private readonly Dictionary<AssetReference, int> _containersRefsCount = 
            new Dictionary<AssetReference, int>();

        private int _exceptionsCount = 0;

        [Inject]
        public AssetsService(DiContainer diContainer)
        {
            _diContainer = diContainer;
            Application.backgroundLoadingPriority = ThreadPriority.Low;
        }

        public async UniTask<AssetContainer<TRef, TObject>> GetContainer<TRef, TObject>(TRef reference)
            where TRef : AssetReference
            where TObject : Object
        {
            if (_assetsLoadsMap.ContainsKey(reference))
            {
                await _assetsLoadsMap[reference];
            }
            else if (!_assetsContainersCache.ContainsKey(reference))
            {
                await PreloadContainer<TRef, TObject>(reference);
            }
            
            _containersRefsCount[reference]++;
            return _assetsContainersCache[reference] as AssetContainer<TRef, TObject>;
        }

        public void ReleaseContainer<TRef, TObject>(AssetContainer<TRef, TObject> container)
            where TRef : AssetReference
            where TObject : Object 
        {
            TRef reference = container.Reference;
            if (!_assetsContainersCache.ContainsKey(reference))
            {
                Debug.LogError($"Cannot find container with provided ref {reference}");
                return;
            }

            _containersRefsCount[reference]--;
            
            int assetsInstancesRefsCount = container.RefsCount;
            int assetContainerRefsCount = _containersRefsCount[reference];
            
            if (assetsInstancesRefsCount == 0 && assetContainerRefsCount == 0)
            {
                _containersRefsCount.Remove(reference);
                _assetsContainersCache.Remove(reference);
                reference.ReleaseAsset();
            }
        }

        private async UniTask PreloadContainer<TRef, TObject>(
            TRef reference,
            UniTaskCompletionSource loadingTCS = null
        )
            where TRef : AssetReference
            where TObject : Object
        {
            if (loadingTCS == null)
            {
                loadingTCS = new UniTaskCompletionSource();
                _assetsLoadsMap[reference] = loadingTCS.Task.Preserve();
            }
            
            try
            {
                await reference.LoadAssetAsync<TObject>().ToUniTask();
                    
                _exceptionsCount = 0;
                AssetContainer<TRef, TObject> container = new AssetContainer<TRef, TObject>(_diContainer, reference);
                _assetsContainersCache[reference] = container;
                _containersRefsCount[reference] = 0;
                
                _assetsLoadsMap.Remove(reference);
                loadingTCS?.TrySetResult();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                _exceptionsCount++;
                int delayTime = Mathf.Clamp((int) Mathf.Pow(2, _exceptionsCount), 0, MaxRetryDelay) * 1000;
                await UniTask.Delay(delayTime);
                await PreloadContainer<TRef, TObject>(reference, loadingTCS);
            }
        }
    }
}