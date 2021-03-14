using UnityEngine;
using UnityEngine.UI;

public class Pawn : MonoBehaviour
{
    /// <summary>
    /// Позиция фигуры
    /// </summary>
    public Transform Transform => transform;
    /// <summary>
    /// Анимация фигуры при выборе
    /// </summary>
    public Animator Animator => animator;
    /// <summary>
    /// Проверка, передвигается ли фигура в данный момент
    /// </summary>
    public bool IsMoving { get; set; }
    /// <summary>
    /// Цвет фигуры
    /// </summary>
    public PawnColor pawnColor { get; set; }
    public enum PawnColor
    {
        white,
        black
    }
    private Button select;
    private Animator animator;

    private void Awake()
    {
        select = GetComponent<Button>();
        select.onClick.AddListener(() => 
        {
            Player.SelectPawn(this);
        });

        animator = GetComponent<Animator>();
        animator.enabled = false;
    }
}