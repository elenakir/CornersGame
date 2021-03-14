using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [SerializeField] private Transform _parent;

    private void Awake()
    {
        CreateBoard();
    }

    /// <summary>
    /// Формирование игровой доски
    /// </summary>
    private void CreateBoard()
    {
        int count = 0;
        List<Cell> cells = new List<Cell>();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject cell = Instantiate(Resources.Load("EmptyCell"), _parent) as GameObject;
                cell.name = $"Cell_{count++}";
                cell.transform.position = new Vector2(cell.transform.position.x + (100 * i), cell.transform.position.y + (100 * j));
                cell.transform.SetAsFirstSibling();

                Cell cellComp = cell.GetComponent<Cell>();
                cellComp.IsFree = true;
                cellComp.IsFirstColumn = i == 0;
                cellComp.IsLastColumn = i == 7;
                cellComp.IsFirstRow = j == 0;
                cellComp.IsLastRow = j == 7;

                Button btn = cell.GetComponent<Button>();
                btn.onClick.AddListener(() =>
                {
                    if (Player.SelectedPawn != null && !Player.SelectedPawn.IsMoving)
                    {
                        Player.SelectedCell = cellComp;
                    }
                });

                cells.Add(cellComp);
            }
        }
        Player.Cells = cells;
    }
}
