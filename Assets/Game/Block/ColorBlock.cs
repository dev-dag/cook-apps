using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ColorBlock : SerializedMonoBehaviour, IBlock
{
    public enum BlockStateEnum
    {
        None,
        Idle,
        Solved,
        Vibrate,
        Hint,
    }

    public enum ColorEnum
    {
        None,
        Red,
        Orange,
        Green,
        Pink,
        DeepPink,
        Yellow
    }

    public BlockStateEnum State { get => state; }
    public ColorEnum Color { get => color; }
    public Image Render { get => render; }

    [SerializeField, Required] private Animator animator;
    [SerializeField] private BlockStateEnum state;
    [SerializeField] private ColorEnum color;
    [SerializeField, Required] private Image render;
    [SerializeField] private Dictionary<ColorEnum, Sprite> sprites;

    private Coroutine hintCoroutine;
    private void Reset()
    {
        animator = GetComponent<Animator>();
    }

    public void Initialize(ColorEnum newColor)
    {
        render.sprite = sprites[newColor];
        DoIdle();
    }

    public void DoIdle()
    {
        StopAllCoroutines();
        render.transform.localPosition = Vector3.zero;

        animator.Play("ACP_Idle");
    }

    public void DoSolve()
    {
        StopAllCoroutines();
        render.transform.localPosition = Vector3.zero;

        animator.Play("ACP_Solve");
    }

    public void DoVibrate()
    {
        StopAllCoroutines();
        render.transform.localPosition = Vector3.zero;

        animator.Play("ACP_Vibrate");
    }

    public void DoHint(Slot.ConnectionTypeEnum direction)
    {
        StopAllCoroutines();
        render.transform.localPosition = Vector3.zero;

        animator.Play("ACP_Hint");

        switch (direction)
        {
            case Slot.ConnectionTypeEnum.Up:
                StartCoroutine(HintMoveCoroutine(Vector2.up));
                break;
            case Slot.ConnectionTypeEnum.Down:
                StartCoroutine(HintMoveCoroutine(Vector2.down));
                break;
            case Slot.ConnectionTypeEnum.UpLeft:
                StartCoroutine(HintMoveCoroutine(Vector2.left + Vector2.up));
                break;
            case Slot.ConnectionTypeEnum.DownRight:
                StartCoroutine(HintMoveCoroutine(Vector2.right + Vector2.down));
                break;
            case Slot.ConnectionTypeEnum.UpRight:
                StartCoroutine(HintMoveCoroutine(Vector2.right + Vector2.up));
                break;
            case Slot.ConnectionTypeEnum.DownLeft:
                StartCoroutine(HintMoveCoroutine(Vector2.left + Vector2.down));
                break;
        }
    }

    private IEnumerator HintMoveCoroutine(Vector2 direction)
    {
        Vector2 originPos = render.transform.position;
        Vector2 targetPos = originPos + direction * 10f;
        float speed = 1f;

        while (true)
        {
            float delta = 0f;

            while ((Vector2)render.transform.position != targetPos)
            {
                Vector2 newPos = Vector2.Lerp(originPos, targetPos, delta);
                render.transform.position = newPos;
                delta += Time.deltaTime * speed;

                yield return null;
            }

            delta = 0f;

            while ((Vector2)render.transform.position != originPos)
            {
                Vector2 newPos = Vector2.Lerp(targetPos, originPos, delta);
                render.transform.position = newPos;
                delta += Time.deltaTime * speed;

                yield return null;
            }
        }
    }
}