using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Rule : ScriptableObject
{
    /// <summary>
    /// Список ячеек, в которые можем переместить фигуру
    /// </summary>
    public List<Cell> AllowableCells { get; protected set; }
    public List<Cell> Cells { get; protected set; }
    public int Index { get; protected set; }

    public virtual void Init() 
    {
        AllowableCells = new List<Cell>();
        Cells = new List<Cell>(Player.Cells);
    }

    public virtual void Run()
    {
        if (Player.SelectedPawn != null && !Player.SelectedPawn.IsMoving)
        {
            for (int i = 0; i < Cells.Count; i++)
            {
                if (Player.SelectedPawn.Transform.position == Cells[i].Transform.position)
                {
                    Index = i;
                }
            }
            //Прямое направление (общее для всех 3-х правил)
            if (Cells[Mathf.Clamp(Index + 1, 0, 63)].IsFree && !Cells[Index].IsLastRow) AllowableCells.Add(Cells[Index + 1]);
            if (Cells[Mathf.Clamp(Index - 1, 0, 63)].IsFree && !Cells[Index].IsFirstRow) AllowableCells.Add(Cells[Index - 1]);
            if (Cells[Mathf.Clamp(Index + 8, 0, 63)].IsFree && !Cells[Index].IsLastColumn) AllowableCells.Add(Cells[Index + 8]);
            if (Cells[Mathf.Clamp(Index - 8, 0, 63)].IsFree && !Cells[Index].IsFirstColumn) AllowableCells.Add(Cells[Index - 8]);
        }
    }

    public virtual void OnFinalize()
    {
        foreach (var item in AllowableCells)
        {
            item.CanMove = true;

            Image image = item.GetComponentInChildren<Image>();
            var tempColor = image.color;
            tempColor.a = 0.7f;
            image.color = tempColor;
        }
        AllowableCells.Clear();
    }
}
