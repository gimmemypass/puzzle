using Cysharp.Threading.Tasks;
using Services.Assets.Containers;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Services.Assets
{
    public interface IAssetsService
    {
        UniTask<AssetContainer<TRef, TObject>> GetContainer<TRef, TObject>(TRef reference)
            where TRef : AssetReference
            where TObject : Object;

        void ReleaseContainer<TRef, TObject>(AssetContainer<TRef, TObject> container)
            where TRef : AssetReference
            where TObject : Object;
    }
}