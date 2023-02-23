namespace Assets.Scripts.Bank
{
    public class BankModel
    {
        public int money { get; private set; }

        public BankModel(int money)
        {
            this.money = money;
        }

        public void AddMoney(int value)
        {
            if (value > 0)
                money += value;
        }
        public void SpendMoney(int value)
        {
            if (value > 0)
                money -= value;
        }
    }
}
