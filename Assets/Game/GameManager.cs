using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class GameManager : SerializedMonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Dictionary<int, Slot> SlotCache { get; private set; } = new Dictionary<int, Slot>();

    public Observer<GameData> GameData = new Observer<GameData>();

    [SerializeField] private List<Slot> slots;

    private void Awake()
    {
        // 싱글톤 초기화
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        Initialize();
    }

    public void Initialize()
    {
        for (int index = 0; index < slots.Count; index++)
        {
            slots[index].id = index + 1;
        }

        Cache();
    }

    private void Cache()
    {
        SlotCache.Clear();

        for (int index = 0; index < slots.Count; index++)
        {
            var slot = slots[index];
            SlotCache.Add(slot.id, slot);
        }
    }
}