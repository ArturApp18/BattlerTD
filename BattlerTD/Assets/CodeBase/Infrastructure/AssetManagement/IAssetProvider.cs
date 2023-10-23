using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.AssetManagement
{
  public interface IAssetProvider:IService
  {
    GameObject Instantiate(string path, Vector3 at);
    GameObject Instantiate(string path);
  }
}