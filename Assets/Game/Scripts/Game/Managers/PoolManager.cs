#region Creator

//Created by Recep Yazıcı 
//gamedeveloper.recep@gmail.com

#endregion

using Game.Interface;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Manager
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance;

        [Header("Pool List")] 
        [SerializeField] private List<CreatablePoolItemProperty> creatablePoolItems = new();

        private void Awake()
        {
            #region Singleton

            if (Instance == null)
            {
                DontDestroyOnLoad(this.gameObject);
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }

            #endregion

            Spawn();
        }

        #region private

        private Dictionary<string, List<IPoolItem>> poolItemList = new();

        #endregion

        private void Spawn()
        {
            for (int i = 0; i < creatablePoolItems.Count; i++)
            {
                FirstCreatePoolItem(creatablePoolItems[i]);
            }
        }

        public T GetPoolItem<T>(string script)
        {
            IPoolItem iPoolItemResult = null;

            if (poolItemList.ContainsKey(script))
            {
                foreach (IPoolItem iPoolItem in poolItemList[script])
                {
                    if (iPoolItem.IsAvailableForSpawn)
                    {
                        iPoolItemResult = iPoolItem;
                        break;
                    }
                }
            }

            if (iPoolItemResult == null)
            {
                iPoolItemResult = AfterCreatePoolItem(script);

                Add(script, iPoolItemResult);
            }

            return (T)iPoolItemResult;
        }

        private void Add<T>(string poolItemName, T poolItem)
        {
            List<IPoolItem> newPoolItems;
            if (!poolItemList.ContainsKey(poolItemName))
            {
                newPoolItems = new List<IPoolItem>();
                newPoolItems.Add((IPoolItem)poolItem);

                poolItemList.Add(poolItemName, newPoolItems);
            }
            else
            {
                newPoolItems = poolItemList[poolItemName];
                newPoolItems.Add((IPoolItem)poolItem);
            }
        }

        private IPoolItem AfterCreatePoolItem(string poolItemName)
        {
            CreatablePoolItemProperty creatablePoolItemPropertyTemp =
                creatablePoolItems.FirstOrDefault(x => x.GetPoolItemName() == poolItemName);

            GameObject prefab = creatablePoolItemPropertyTemp.Prefab;
            Transform parent = creatablePoolItemPropertyTemp.Parent;

            return Instantiate(prefab, parent).GetComponent<IPoolItem>();
        }

        private void FirstCreatePoolItem(CreatablePoolItemProperty creatablePoolItemProperty)
        {
            GameObject prefab = creatablePoolItemProperty.Prefab;
            Transform parent = creatablePoolItemProperty.Parent;
            int spawnCount = creatablePoolItemProperty.SpawnCount;
            string poolItemName = creatablePoolItemProperty.GetPoolItemName();

            for (int s = 0; s < spawnCount; s++)
            {
                IPoolItem iPoolItemTemp = Instantiate(prefab, parent).GetComponent<IPoolItem>();
                Add(poolItemName, iPoolItemTemp);
            }
        }
    }
}