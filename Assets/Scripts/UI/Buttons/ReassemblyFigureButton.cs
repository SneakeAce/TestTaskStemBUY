using UnityEngine;
using Zenject;

public class ReassemblyFigureButton : MonoBehaviour, IButton
{
    private FigureFieldController _figureFieldController;

    [Inject]
    private void Consturct(FigureFieldController figureFieldController)
    {
        _figureFieldController = figureFieldController;
    }

    public void Click()
    {
        _figureFieldController.ReassembleFigure();
    }
}
