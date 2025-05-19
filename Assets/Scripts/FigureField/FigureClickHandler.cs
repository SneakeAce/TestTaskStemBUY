using UnityEngine;
using UnityEngine.InputSystem;

public class FigureClickHandler
{
    private PlayerInput _playerInput;
    private ActionBar _actionBar;
    private Camera _mainCamera;

    private LayerMask _figureLayer = 1 << 6;

    public FigureClickHandler(PlayerInput playerInput, Camera mainCamera, ActionBar actionBar)
    {
        _playerInput = playerInput;
        _playerInput.Enable();

        _mainCamera = mainCamera;

        _actionBar = actionBar;

        _playerInput.Touch.TouchPress.performed += ClickOnFigure;
    }

    private void ClickOnFigure(InputAction.CallbackContext context)
    {
        Vector2 screenPosition = _playerInput.Touch.TouchPosition.ReadValue<Vector2>();
        Vector2 worldPosition = _mainCamera.ScreenToWorldPoint(screenPosition);

        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, _figureLayer);

        if (hit.collider != null)
        {
            Figure figure = hit.collider.GetComponent<Figure>();

            if (figure.IsStuck)
                figure.IsStuck = false;

            if (figure.FigureType == FigureType.StickyFigure)
            {
                StickyFigure stickyFigure = hit.collider.GetComponent<StickyFigure>();

                stickyFigure.SpringJoint.enabled = false;
                stickyFigure.enabled = false;
            }

            if (figure == null)
                return;

            _actionBar.SetFigureToSlot(figure, _mainCamera);
        }

    }
}
