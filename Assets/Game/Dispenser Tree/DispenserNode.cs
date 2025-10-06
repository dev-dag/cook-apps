using UnityEngine;

/// <summary>
/// 1진 트리 그래프의 노드 클래스.
/// 레벨이 높은 노드에서 낮은 노드로 데이터를 공급하기 위한 구조.
/// </summary>
public class DispenserNode<T> where T : class
{
    public bool IsRoot { get => parent == null; }
    public DispenserNode<T> Parent { get => parent; }
    public DispenserNode<T> Child { get => child; }
    public T Value { get => value; }

    [SerializeField] private DispenserNode<T> parent;
    [SerializeField] private T value;
    [SerializeField] private DispenserNode<T> child;

    public DispenserNode(T newValue)
    {
        value = newValue;
        parent = null;
        child = null;
    }

    public bool SetChild(DispenserNode<T> newChild)
    {
        if (child != null)
        {
            return false;
        }
        else
        {
            child = newChild;
            newChild.parent = this;
            return true;
        }
    }

    public bool Dispense(T newValue)
    {
        bool result = child != null && child.Dispense(value);

        if (result == true)
        {
            value = newValue;
            return true;
        }
        else if (value == null)
        {
            value = newValue;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanDispense()
    {
        bool canDispense = value == null;

        if (canDispense == false && child != null)
        {
            canDispense = child.CanDispense();
        }

        return canDispense;
    }
}