using System;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Slot : SerializedMonoBehaviour
{
    public enum ConnectionTypeEnum
    {
        Up = 1,
        Down = -1,

        UpLeft = 2,
        DownRight = -2,

        UpRight = 3,
        DownLeft = -3,
    }

    public bool IsEmpty { get => block == null; }
    public Dictionary<ConnectionTypeEnum, Slot> Connections { get => connections; }

    public IBlock block;
    public int id;

    private RectTransform rectTransform;

    [SerializeField] private Dictionary<ConnectionTypeEnum, Slot> connections = new Dictionary<ConnectionTypeEnum, Slot>();

    void Reset()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Connect(Slot another, ConnectionTypeEnum connectionType)
    {
        if (connections.TryGetValue(connectionType, out var connecttedNode))
        {
            if (connecttedNode == another)
            {
                return;
            }
            else
            {
                throw new Exception();
            }
        }
        else
        {
            connections.Add(connectionType, another); // this -> another 참조 시작

            var inverseConnectionType = (ConnectionTypeEnum)((int)connectionType * -1); // 상대 객체 관점의 커넥션 타입
            another.Connect(this, inverseConnectionType); // another -> this 침조 시작
        }
    }

    public void Disconnect(ConnectionTypeEnum connectionType)
    {
        if (connections.ContainsKey(connectionType))
        {
            var temp = connections[connectionType];
            connections.Remove(connectionType); // this -> another 참조 제거

            var inverseConnectionType = (ConnectionTypeEnum)((int)connectionType * -1); // 상대 객체 관점의 커넥션 타입
            temp.Disconnect(inverseConnectionType); // another -> this 참조 제거
        }
    }

    [ContextMenu("Connect with around")]
    // 주변 슬롯들과 이 객체를 연결
    private void ConnectWithAround()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
        EventSystem eventSystem = FindFirstObjectByType<EventSystem>();
        Vector2 anchoredPos;

        connections.Clear();

        PointerEventData raycastEventData;
        List<RaycastResult> raycastResults = new List<RaycastResult>();

        // Up
        raycastEventData = new PointerEventData(EventSystem.current);
        anchoredPos = rectTransform.anchoredPosition + Vector2.up * 94f;
        raycastEventData.position = RectTransformUtility.WorldToScreenPoint(null, canvasRectTransform.TransformPoint(anchoredPos)); // 레이 캐스팅 위치 설정
        raycastResults.Clear();

        eventSystem.RaycastAll(raycastEventData, raycastResults);

        if (raycastResults.Count == 0)
        {
            Debug.LogWarning($"{this.gameObject.name} could'nt find any slots connectable with {ConnectionTypeEnum.Up.ToString()} side.");
        }

        foreach (var raycastResult in raycastResults)
        {
            var slot = raycastResult.gameObject.GetComponentInParent<Slot>();

            if (slot != null)
            {
                Connect(slot, ConnectionTypeEnum.Up);
                break;
            }
        }

        // Down
        raycastEventData = new PointerEventData(EventSystem.current);
        anchoredPos = rectTransform.anchoredPosition + Vector2.up * -94f;
        raycastEventData.position = RectTransformUtility.WorldToScreenPoint(null, canvasRectTransform.TransformPoint(anchoredPos)); // 레이 캐스팅 위치 설정
        raycastResults.Clear();

        eventSystem.RaycastAll(raycastEventData, raycastResults);

        if (raycastResults.Count == 0)
        {
            Debug.LogWarning($"{this.gameObject.name} could'nt find any slots connectable with {ConnectionTypeEnum.Down.ToString()} side.");
        }

        foreach (var raycastResult in raycastResults)
        {
            var slot = raycastResult.gameObject.GetComponentInParent<Slot>();

            if (slot != null)
            {
                Connect(slot, ConnectionTypeEnum.Down);
                break;
            }
        }

        // Up Left
        raycastEventData = new PointerEventData(EventSystem.current);
        anchoredPos = rectTransform.anchoredPosition + Vector2.right * -77.25f + Vector2.up * 47f;
        raycastEventData.position = RectTransformUtility.WorldToScreenPoint(null, canvasRectTransform.TransformPoint(anchoredPos)); // 레이 캐스팅 위치 설정
        raycastResults.Clear();

        eventSystem.RaycastAll(raycastEventData, raycastResults);

        if (raycastResults.Count == 0)
        {
            Debug.LogWarning($"{this.gameObject.name} could'nt find any slots connectable with {ConnectionTypeEnum.UpLeft.ToString()} side.");
        }

        foreach (var raycastResult in raycastResults)
        {
            var slot = raycastResult.gameObject.GetComponentInParent<Slot>();

            if (slot != null)
            {
                Connect(slot, ConnectionTypeEnum.UpLeft);
                break;
            }
        }

        // Up Right
        raycastEventData = new PointerEventData(EventSystem.current);
        anchoredPos = rectTransform.anchoredPosition + Vector2.right * 77.25f + Vector2.up * 47f;
        raycastEventData.position = RectTransformUtility.WorldToScreenPoint(null, canvasRectTransform.TransformPoint(anchoredPos)); // 레이 캐스팅 위치 설정
        raycastResults.Clear();

        eventSystem.RaycastAll(raycastEventData, raycastResults);

        if (raycastResults.Count == 0)
        {
            Debug.LogWarning($"{this.gameObject.name} could'nt find any slots connectable with {ConnectionTypeEnum.UpRight.ToString()} side.");
        }

        foreach (var raycastResult in raycastResults)
        {
            var slot = raycastResult.gameObject.GetComponentInParent<Slot>();

            if (slot != null)
            {
                Connect(slot, ConnectionTypeEnum.UpRight);
                break;
            }
        }

        // Down Left
        raycastEventData = new PointerEventData(EventSystem.current);
        anchoredPos = rectTransform.anchoredPosition + Vector2.right * -77.25f + Vector2.up * -47f;
        raycastEventData.position = RectTransformUtility.WorldToScreenPoint(null, canvasRectTransform.TransformPoint(anchoredPos)); // 레이 캐스팅 위치 설정
        raycastResults.Clear();

        eventSystem.RaycastAll(raycastEventData, raycastResults);

        if (raycastResults.Count == 0)
        {
            Debug.LogWarning($"{this.gameObject.name} could'nt find any slots connectable with {ConnectionTypeEnum.DownLeft.ToString()} side.");
        }

        foreach (var raycastResult in raycastResults)
        {
            var slot = raycastResult.gameObject.GetComponentInParent<Slot>();

            if (slot != null)
            {
                Connect(slot, ConnectionTypeEnum.DownLeft);
                break;
            }
        }

        // Down Right
        raycastEventData = new PointerEventData(EventSystem.current);
        anchoredPos = rectTransform.anchoredPosition + Vector2.right * 77.25f + Vector2.up * -47f;
        raycastEventData.position = RectTransformUtility.WorldToScreenPoint(null, canvasRectTransform.TransformPoint(anchoredPos)); // 레이 캐스팅 위치 설정
        raycastResults.Clear();

        eventSystem.RaycastAll(raycastEventData, raycastResults);

        if (raycastResults.Count == 0)
        {
            Debug.LogWarning($"{this.gameObject.name} could'nt find any slots connectable with {ConnectionTypeEnum.DownRight.ToString()} side.");
        }

        foreach (var raycastResult in raycastResults)
        {
            var slot = raycastResult.gameObject.GetComponentInParent<Slot>();

            if (slot != null)
            {
                Connect(slot, ConnectionTypeEnum.DownRight);
                break;
            }
        }
    }
}
