using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ObstacleAnimationsController : MonoBehaviour
{
    private void CardMover()
    {
        transform.DOMoveX(transform.position.x - 10.8f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);  // CardMover
    }
    private void GuillotineMover()
    {
        transform.DORotate(new Vector3(0, 0, -90), 2, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
    private void HandMover()
    {
        transform.DOMoveX(transform.position.x - 5.4f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);  // HandMover
    }
    private void WallMover()
    {
        transform.DORotate(new Vector3(0, 3600, 0), 20, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo);
    }
}
