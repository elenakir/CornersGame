using UnityEngine;

[CreateAssetMenu(fileName = "JumpDiagonal", menuName = "Rules/JumpDiagonal")]
public class JumpDiagonal : Rule
{
    public override void Init()
    {
        base.Init();
    }
    public override void Run()
    {
        base.Run();

        //Формируем список ячеек, в которые можем переместиться
        if (Player.SelectedPawn != null)
        {
            //Могут перепрыгивать фигуру по диагонали
            if (Cells[Mathf.Clamp(Index + 14, 0, 63)].IsFree
                && !Cells[Index].IsLastColumn
                && !Cells[Index].IsFirstRow
                && !Cells[Index + 7].IsFree
                && !Cells[Index + 7].IsLastColumn
                && !Cells[Index + 7].IsFirstRow)
                AllowableCells.Add(Cells[Index + 14]);

            if (Cells[Mathf.Clamp(Index - 14, 0, 63)].IsFree 
                && !Cells[Index].IsLastRow
                && !Cells[Index].IsFirstColumn
                && !Cells[Index - 7].IsFree
                && !Cells[Index - 7].IsLastRow
                && !Cells[Index - 7].IsFirstColumn)
                AllowableCells.Add(Cells[Index - 14]);

            if (Cells[Mathf.Clamp(Index + 18, 0, 63)].IsFree 
                && !Cells[Index].IsLastColumn
                && !Cells[Index].IsLastRow
                && !Cells[Index + 9].IsFree
                && !Cells[Index + 9].IsLastColumn
                && !Cells[Index + 9].IsLastRow)
                AllowableCells.Add(Cells[Index + 18]);

            if (Cells[Mathf.Clamp(Index - 18, 0, 63)].IsFree 
                && !Cells[Index].IsFirstColumn
                && !Cells[Index].IsFirstRow
                && !Cells[Index - 9].IsFree
                && !Cells[Index - 9].IsFirstColumn
                && !Cells[Index - 9].IsFirstRow)
                AllowableCells.Add(Cells[Index - 18]);
        }

        OnFinalize();
    }

    public override void OnFinalize()
    {
        base.OnFinalize();
    }
}
