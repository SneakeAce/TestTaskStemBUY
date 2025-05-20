using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ActionBar : MonoBehaviour
{
    private const int StartIndex = 0;
    private const int MatchingFigureToDestroy = 3;  

    private List<ActionSlot> _emptyActionSlots;
    private List<ActionSlot> _filledSlots;
    private FigureFieldController _figureFieldController;

    [Inject]
    private void Construct(FigureFieldController figureFieldController)
    {
        _figureFieldController = figureFieldController;
    }

    private void Start()
    {
        _emptyActionSlots = new List<ActionSlot>(GetComponentsInChildren<ActionSlot>());
        Debug.Log($"Start / _emptyActionSlots = {_emptyActionSlots.Count}");

        _filledSlots = new List<ActionSlot>();
    }

    public void SetFigureToSlot(Figure figure, Camera camera)
    {
        ActionSlot emptySlot = GetEmptySlot();

        if (emptySlot == null)
            return;

        emptySlot.SetFigure(figure, camera);

        figure.MoveToSlot(emptySlot.CurrentPositionSlot);

        _filledSlots.Add(emptySlot);

        CheckMatchThreeFigure(emptySlot);
    }

    public Dictionary<FigureConfig, int> GetCurrentFigureCounts()
    {
        Dictionary<FigureConfig, int> result = new();

        foreach (ActionSlot slot in _filledSlots)
        {
            if (slot.IsEmptySlot || slot.CurrentFigure == null) continue;

            FigureConfig config = slot.CurrentFigure.FigureConfig; 

            if (result.ContainsKey(config) == false)
                result[config] = 0;

            result[config]++;
        }

        return result;
    }

    private ActionSlot GetEmptySlot()
    {
        foreach (ActionSlot emptySlot in _emptyActionSlots)
        {
            if (emptySlot.IsEmptySlot)
            {
                _emptyActionSlots.Remove(emptySlot);

                return emptySlot;
            }
        }

        return null;
    }

    private void CheckMatchThreeFigure(ActionSlot slot)
    {
        List<ActionSlot> matchThreeSlots = new List<ActionSlot>();

        for (int i = 0; i < _filledSlots.Count; i++)
        {
            if (_filledSlots[i].IsEmptySlot)
                continue;

            if (_filledSlots[i].CurrentFigureAnimalType == slot.CurrentFigureAnimalType &&
                _filledSlots[i].CurrentFigureShapeType == slot.CurrentFigureShapeType &&
                _filledSlots[i].CurrentFigureColor == slot.CurrentFigureColor)
            {
                matchThreeSlots.Add(_filledSlots[i]);
            }
        }

        if (matchThreeSlots.Count >= MatchingFigureToDestroy)
        {
            _figureFieldController.RemoveSameTypeFigureFromField(matchThreeSlots[StartIndex].CurrentFigure.FigureConfig);

            for (int i = 0; i < matchThreeSlots.Count; i++)
            {
                matchThreeSlots[i].ClearSlot();

                _filledSlots.Remove(matchThreeSlots[i]);

                _emptyActionSlots.Add(matchThreeSlots[i]);
            }

        }

        if (_emptyActionSlots.Count == 0)
        {
            _figureFieldController.AllActionBarSlotsFilled();

            return;
        }
    }
}

