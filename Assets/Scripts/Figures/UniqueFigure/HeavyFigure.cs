using System.Diagnostics;

public class HeavyFigure : Figure
{
    public override void SetComponents(FigureConfig config)
    {
        base.SetComponents(config);

        UnityEngine.Debug.Log("HeavyFigure SetComponents");

        if (config is HeavyFigureConfig heavyFigureConfig)
            _rigidbody.mass = heavyFigureConfig.Mass;
    }
}
