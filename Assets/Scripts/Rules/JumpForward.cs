using UnityEngine;

[CreateAssetMenu(fileName = "JumpForward", menuName = "Rules/JumpForward")]
public class JumpForward : Rule
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
            //Могут перепрыгивать фигуру по прямой
            if (Cells[Mathf.Clamp(Index + 2, 0, 63)].IsFree
                && !Cells[Index].IsLastRow
                && !Cells[Index + 1].IsFree
                && !Cells[Index + 1].IsLastRow)
                AllowableCells.Add(Cells[Index + 2]);

            if (Cells[Mathf.Clamp(Index - 2, 0, 63)].IsFree 
                && !Cells[Index].IsFirstRow
                && !Cells[Index - 1].IsFree
                && !Cells[Index - 1].IsFirstRow)
                AllowableCells.Add(Cells[Index - 2]);

            if (Cells[Mathf.Clamp(Index + 16, 0, 63)].IsFree 
                && !Cells[Index].IsLastColumn
                && !Cells[Index + 8].IsFree
                && !Cells[Index + 8].IsLastColumn)
                AllowableCells.Add(Cells[Index + 16]);

            if (Cells[Mathf.Clamp(Index - 16, 0, 63)].IsFree 
                && !Cells[Index].IsFirstColumn
                && !Cells[Index - 8].IsFree
                && !Cells[Index - 8].IsFirstColumn)
                AllowableCells.Add(Cells[Index - 16]);
        }

        OnFinalize();
    }

    public override void OnFinalize()
    {
        base.OnFinalize();
    }
}
