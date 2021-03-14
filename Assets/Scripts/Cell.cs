using UnityEngine;

public class Cell : MonoBehaviour
{
    /// <summary>
    /// Позиция ячейки
    /// </summary>
    public Transform Transform => transform;
    /// <summary>
    /// Ячейка свободна
    /// </summary>
    public bool IsFree { get; set; }
    /// <summary>
    /// Ячейка является "территорией" белых фигур
    /// </summary>
    public bool IsPawnBaseWhite { get; set; }
    /// <summary>
    /// Ячейка является "территорией" черных фигур
    /// </summary>
    public bool IsPawnBaseBlack { get; set; }
    /// <summary>
    /// Ячейка находится в первой строке
    /// </summary>
    public bool IsFirstRow { get; set; }
    /// <summary>
    /// Ячейка находится в последней строке
    /// </summary>
    public bool IsLastRow { get; set; }
    /// <summary>
    /// Ячейка находится в первом столбце
    /// </summary>
    public bool IsFirstColumn { get; set; }
    /// <summary>
    /// Ячейка находится в последнем столбце
    /// </summary>
    public bool IsLastColumn { get; set; }
    /// <summary>
    /// Если True, может двигаться к этой ячейке
    /// </summary>
    public bool CanMove { get; set; }
}
