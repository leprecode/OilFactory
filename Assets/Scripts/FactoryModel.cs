public class FactoryModel
{
    private int producedOil;

    public void AddOil(int value)
    {
        if (value > 0)
        {
            producedOil += value;
        }
    }

    public int TakeAllOil()
    {
        var allOil = producedOil;
        producedOil = 0;

        return allOil;
    }

    public int GetOil()
    {
        return producedOil;
    }
}
