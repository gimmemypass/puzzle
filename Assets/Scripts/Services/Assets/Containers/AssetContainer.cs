using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using Object = UnityEngine.Object;

namespace Services.Assets.Containers
{
    public class AssetContainer<TRef, TObject>
        where TRef : AssetReference
        where TObject : Object 
    {
        private readonly DiContainer _diContainer;
        private readonly TObject _asset;
        private readonly TRef _reference;
        
        private readonly List<GameObject> _instances = new List<GameObject>();

        public TObject Asset => _asset;
        public TRef Reference => _reference;
        public int RefsCount => _instances.Count;

        public AssetContainer(DiContainer diContainer, TRef loadedReference)
        {
            _diContainer = diContainer;
            _asset = loadedReference.Asset as TObject;
            _reference = loadedReference;
        }
        
        public async UniTask<GameObject> CreateInstance(Transform parent = null, DiContainer withContainer = null)
        {
            GameObject instance = await Addressables.InstantiateAsync(_reference, parent);

            withContainer ??= _diContainer;
            withContainer.InjectGameObject(instance);
            _instances.Add(instance);
            
            return instance;
        }

        public async UniTask<TComponent> CreateInstanceForComponent<TComponent>(Transform parent = null, DiContainer withContainer = null)
            where TComponent : Component
        {
            
            GameObject instance = await CreateInstance(parent, withContainer);
            return instance != null 
                ? instance.GetComponent<TComponent>()
                :null;
        }

        public void ReleaseInstance(GameObject instance)
        {
            if (_instances.Remove(instance))
            {
                Addressables.ReleaseInstance(instance);
            }
        }

        public void ReleaseInstance(Component instance)
        {
            ReleaseInstance(instance.gameObject);
        }
    }
}