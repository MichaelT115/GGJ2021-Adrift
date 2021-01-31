using UnityEngine;
using UnityEngine.UI;

public sealed class HandleCard : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private Animator animator;

	public void DisplayInstructions(Instructions instructions)
    {
        image.sprite = instructions.Card;
        PlayAnimation();
    }

    [ContextMenu("Play Card Animation")]
    private void PlayAnimation() => animator.Play("Card Animation", -1, 0);
}
