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
    [SerializeField] private Dictionary<int, List<Slot>> dispenserTree;
    private Dictionary<int, DispenserNode<Slot>> dispenserRoots = new Dictionary<int, DispenserNode<Slot>>();

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
        MakeDispenserTree();
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

    // 분배 트리 생성
    private void MakeDispenserTree()
    {
        dispenserRoots.Clear();

        foreach (int lineID in dispenserTree.Keys)
        {
            DispenserNode<Slot> root = new DispenserNode<Slot>(dispenserTree[lineID][0]);
            dispenserRoots.Add(lineID, root);

            DispenserNode<Slot> prevNode = root;

            for (int index = 1; index < dispenserTree[lineID].Count; index++)
            {
                var node = new DispenserNode<Slot>(dispenserTree[lineID][index]);
                prevNode.SetChild(node);

                prevNode = node;
            }
        }
    }
}