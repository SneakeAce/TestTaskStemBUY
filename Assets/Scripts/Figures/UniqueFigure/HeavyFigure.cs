public class HeavyFigure : Figure
{
    public override void SetComponents(FigureConfig config)
    {
        base.SetComponents(config);

        UnityEngine.Debug.Log("HeavyFigure SetComponents");

        //if (config is SpecialFigureConfig heavyFigureConfig)
        //{
        //    _rigidbody.mass = heavyFigureConfig.Mass;
        //    _rigidbody.gravityScale = heavyFigureConfig.GravityScale;
        //}
    }
}
