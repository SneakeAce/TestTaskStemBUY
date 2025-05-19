using UnityEngine;

public class StickyFigure : MonoBehaviour
{
    private StickyFigureConfig _config;
    private SpringJoint2D _springJoint;
    private Figure _stuckFigure;

    private int _currentStuckedFigureCount;
    private bool _isStuck = false;
    public SpringJoint2D SpringJoint => _springJoint;

    public void SetComponents(StickyFigureConfig config)
    {
        _config = config;

        var trigger = gameObject.AddComponent<CircleCollider2D>();
        trigger.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isStuck)
            return;

        if (collision.TryGetComponent<Figure>(out Figure otherFigure))
        {
            if (otherFigure.IsInSlot == false && _currentStuckedFigureCount < _config.StuckFigureCount && 
                otherFigure.IsStuck == false)
            {
                StickAndDragFigure(otherFigure);
            }
        }
    }

    private void StickAndDragFigure(Figure otherFigure)
    {
        Rigidbody2D otherRb = otherFigure.GetComponent<Rigidbody2D>();

        if (otherRb == null)
            return;

        _isStuck = true;
        _currentStuckedFigureCount++;
        _stuckFigure = otherFigure;
        _stuckFigure.IsStuck = true;

        _stuckFigure.DetachFigure += OnDetachOtherFigure;

        _springJoint = gameObject.AddComponent<SpringJoint2D>();
        _springJoint.connectedBody = otherRb;
        _springJoint.enableCollision = true;
        _springJoint.autoConfigureDistance = false;
        _springJoint.distance = _config.DistanceToStuckFigure; // настраивай под размер фигур
        _springJoint.dampingRatio = _config.SmoothingRatio; // сглаживание
        _springJoint.frequency = _config.Frequency; // сила пружины

        

        Debug.Log($"{gameObject.name} прилип к {otherFigure.gameObject.name} и теперь тянет его");
    }

    private void OnDetachOtherFigure()
    {
        if (_springJoint != null)
        {
            _springJoint.enabled = false;
        }

        if (_stuckFigure != null)
        {
            _stuckFigure.DetachFigure -= OnDetachOtherFigure;
            _stuckFigure.IsStuck = false;
            _stuckFigure = null;
        }

        _isStuck = false;
    }
}
