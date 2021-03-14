using UnityEngine;

[CreateAssetMenu(fileName = "Step", menuName = "Rules/Step")]
public class Step : Rule
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
            //Движение по диагонали
            if (Cells[Mathf.Clamp(Index + 7, 0, 63)].IsFree && !Cells[Index].IsFirstRow && !Cells[Index].IsLastColumn) AllowableCells.Add(Cells[Index + 7]);
            if (Cells[Mathf.Clamp(Index - 7, 0, 63)].IsFree && !Cells[Index].IsLastRow && !Cells[Index].IsFirstColumn) AllowableCells.Add(Cells[Index - 7]);
            if (Cells[Mathf.Clamp(Index + 9, 0, 63)].IsFree && !Cells[Index].IsLastRow && !Cells[Index].IsLastColumn) AllowableCells.Add(Cells[Index + 9]);
            if (Cells[Mathf.Clamp(Index - 9, 0, 63)].IsFree && !Cells[Index].IsFirstRow && !Cells[Index].IsFirstColumn) AllowableCells.Add(Cells[Index - 9]);
        }

        OnFinalize();
    }

    public override void OnFinalize()
    {
        base.OnFinalize();
    }
}
