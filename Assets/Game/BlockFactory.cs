using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// 블럭 인스턴스를 생성해줄 팩토리 게임 오브젝트
/// </summary>
public class BlockFactory : MonoBehaviour
{
    public ObjectPool<ColorBlock> ColorBlockPool { get => colorBlockPool; }

    [SerializeField] private GameObject colorBlockPrefab;
    private ObjectPool<ColorBlock> colorBlockPool;

    public void Initialize()
    {
        MakeColorBlockPool();
    }

    private void MakeColorBlockPool()
    {
        colorBlockPool = new ObjectPool<ColorBlock>(createFunc: OnCreate, actionOnGet: OnGet, actionOnRelease: OnRelease);

        ColorBlock OnCreate()
        {
            var newObject = GameObject.Instantiate(colorBlockPrefab, this.transform);
            newObject.name = $"{colorBlockPrefab.name}_{colorBlockPool.CountAll + 1}";

            return newObject.GetComponent<ColorBlock>();
        }

        void OnGet(ColorBlock block)
        {
            block.gameObject.SetActive(true);
        }

        void OnRelease(ColorBlock block)
        {
            block.gameObject.SetActive(false);
        }
    }
}
