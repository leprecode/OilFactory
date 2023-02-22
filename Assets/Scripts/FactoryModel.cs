public class FactoryModel
{
    private int producedOil;

    public FactoryModel(int producedOil)
    {
        this.producedOil = producedOil;
    }

    public void AddOil(int value)
    {
        if (value > 0)
        {
            producedOil += value;
        }
    }

    public int GetOil()
    {
        return producedOil;
    }
}
