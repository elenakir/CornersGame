using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Все ячейки на доске
    /// </summary>
    public static List<Cell> Cells 
    {
        get { return cells; }
        set { cells = value; }
    }

    /// <summary>
    /// Выбранная фигура
    /// </summary>
    public static Pawn SelectedPawn;
    /// <summary>
    /// Выбранная ячейка
    /// </summary>
    public static Cell SelectedCell;
    /// <summary>
    /// Проверка, чей сейчас ход
    /// </summary>
    public static bool IsWhite;
    /// <summary>
    /// Проверка, выиграл ли один из игроков
    /// </summary>
    public static bool IsWin;
    /// <summary>
    /// Счетчик ходов игрока 1
    /// </summary>
    public static int TurnCount1;
    /// <summary>
    /// Счетчик ходов игрока 2
    /// </summary>
    public static int TurnCount2;

    [SerializeField] private Transform _pawnsParent;
    [SerializeField, Range(1,9)] private int _cellsForWin; //Количество занятых ячеек противника, нужных для победы

    private static Pawn previousPawn;
    private static List<Pawn> pawns = new List<Pawn>();
    private static List<Cell> cells = new List<Cell>();

    private void Awake()
    {
        SelectedPawn = null;
        SelectedCell = null;
        previousPawn = null;
        IsWhite = true;
        IsWin = false;
        TurnCount1 = TurnCount2 = 0;
        pawns.Clear();

        //Размещаем белые фигуры на их стартовых позициях
        int count = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject field = Instantiate(Resources.Load("PawnWhite"), _pawnsParent) as GameObject;
                field.name = $"PawnWhite_{count++}";
                field.transform.position = new Vector2(field.transform.position.x + (100 * i), field.transform.position.y + (100 * j));
                Pawn pawn = field.GetComponent<Pawn>();
                pawn.pawnColor = Pawn.PawnColor.white;
                pawns.Add(pawn);
            }
        }

        //Размещаем черные фигуры на их стартовых позициях
        count = 0;
        for (int i = 5; i < 8; i++)
        {
            for (int j = 5; j < 8; j++)
            {
                GameObject field = Instantiate(Resources.Load("PawnBlack"), _pawnsParent) as GameObject;
                field.name = $"PawnBlack_{count++}";
                field.transform.position = new Vector2(field.transform.position.x + (100 * i), field.transform.position.y + (100 * j));
                Pawn pawn = field.GetComponent<Pawn>();
                pawn.pawnColor = Pawn.PawnColor.black;
                pawns.Add(pawn);
            }
        }

        SetBaseCells();
    }

    private void Update()
    {
        if (SelectedCell != null)
        {
            if (SelectedCell.IsFree && SelectedCell.CanMove)
            {
                MoveTo();
            }
        }
    }

    /// <summary>
    /// Движение к выбранной ячейке
    /// </summary>
    private void MoveTo()
    {
        SelectedPawn.IsMoving = true;
        SelectedPawn.transform.position = Vector3.MoveTowards(SelectedPawn.transform.position, SelectedCell.Transform.position, 10f);
        SelectedPawn.transform.SetAsLastSibling();
        if (SelectedPawn.transform.position == SelectedCell.Transform.position)
        {
            if (IsWhite) TurnCount1++;
            else TurnCount2++;
            IsWhite = !IsWhite;
            UpdateBoard();
            SelectionsOff();
            WinChecking();
        }
    }

    /// <summary>
    /// Выбор фигуры
    /// </summary>
    /// <param name="pawn"></param>
    public static void SelectPawn(Pawn pawn)
    {
        SelectedCell = null;
        foreach (var cell in Cells)
        {
            cell.CanMove = false;
            Image image = cell.GetComponentInChildren<Image>();
            var tempColor = image.color;
            tempColor.a = 0f;
            image.color = tempColor;
        }

        if (IsWhite)
        {
            if (pawn.pawnColor == Pawn.PawnColor.white)
            {
                if (previousPawn != null)
                {
                    previousPawn.Animator.Rebind();
                    previousPawn.Animator.Update(0f);
                    previousPawn.Animator.enabled = false;
                };
                pawn.Animator.enabled = !pawn.Animator.enabled;

                SelectedPawn = pawn;
                previousPawn = pawn;
            }
        }
        else
        {
            if (pawn.pawnColor == Pawn.PawnColor.black)
            {
                if (previousPawn != null)
                {
                    previousPawn.Animator.Rebind();
                    previousPawn.Animator.Update(0f);
                    previousPawn.Animator.enabled = false;
                };
                pawn.Animator.enabled = !pawn.Animator.enabled;

                SelectedPawn = pawn;
                previousPawn = pawn;
            }
        }
    }

    /// <summary>
    /// Завершение мигания фигуры
    /// </summary>
    private void SelectionsOff()
    {
        SelectedCell = null;

        SelectedPawn.IsMoving = false;
        SelectedPawn.Animator.Rebind();
        SelectedPawn.Animator.Update(0f);
        SelectedPawn.Animator.enabled = false;
        SelectedPawn = null;

        foreach (var cell in cells)
        {
            cell.CanMove = false;
            Image image = cell.GetComponentInChildren<Image>();
            var tempColor = image.color;
            tempColor.a = 0f;
            image.color = tempColor;
        }
    }

    /// <summary>
    /// Формирование списка ячеек, который является "базой" игрока и которые должен занять противник для победы
    /// </summary>
    private void SetBaseCells()
    {
        foreach (var pawn in pawns)
        {
            foreach (var cell in cells)
            {
                if (cell.Transform.position == pawn.Transform.position)
                {
                    if (pawn.pawnColor == Pawn.PawnColor.black)
                    {
                        cell.IsPawnBaseBlack = true;
                    }
                    else
                    {
                        cell.IsPawnBaseWhite = true;
                    }
                    cell.IsFree = false;
                }
            }
        }
    }

    /// <summary>
    /// Обновление доски, пересчет состояния ячейки (занята или свободна)
    /// </summary>
    private void UpdateBoard()
    {
        foreach (var cell in cells)
        {
            cell.IsFree = true;
            foreach (var pawn in pawns)
            {
                if (cell.Transform.position == pawn.Transform.position)
                {
                    cell.IsFree = false;
                }
            }
        }
    }

    /// <summary>
    /// Проверка, выиграл ли какой-нибудь игрок
    /// </summary>
    /// <returns></returns>
    private bool WinChecking()
    {
        int countPlayer1 = 0;
        int countPlayer2 = 0;
        foreach (var cell in cells)
        {
            foreach (var pawn in pawns)
            {
                if (cell.Transform.position == pawn.Transform.position)
                {
                    if (pawn.pawnColor == Pawn.PawnColor.black && cell.IsPawnBaseWhite)
                    {
                        countPlayer1++;
                    }
                    else if (pawn.pawnColor == Pawn.PawnColor.white && cell.IsPawnBaseBlack)
                    {
                        countPlayer2++;
                    }
                }
            }
        }
        IsWin = countPlayer1 == _cellsForWin || countPlayer2 == _cellsForWin;

        return IsWin;
    }
}
