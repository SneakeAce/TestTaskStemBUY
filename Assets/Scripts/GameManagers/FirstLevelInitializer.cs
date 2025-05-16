public class FirstLevelInitializer
{
    private FigureSpawner _figureSpawner;

    public FirstLevelInitializer(FigureSpawner figureSpawner)
    {
        _figureSpawner = figureSpawner;

        Start();
    }

    private void Start()
    {
        _figureSpawner.Start();
    }
}
