using UnityEngine;
using DG.Tweening;
public class ObstacleAnimationsController : MonoBehaviour
{
    public void CardMover()
    {
        transform.DOMoveX(transform.position.x - 11.7f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);  
    }
    public void GuillotineMover()
    {
       transform.DORotate(new Vector3(0, 0, -90), 2, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
    public void HandMover()
    {
        transform.DOMoveX(transform.position.x - 5.4f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);  
    }
    public void WallMover()
    {
        transform.DORotate(new Vector3(0, 3600, 0), 20, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo);
    }
}
