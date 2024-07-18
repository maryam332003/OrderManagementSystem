namespace OrdersManagement.Service.Product.Contract
{
    public interface IValidateTheOrderServices
    {
        decimal Discount(decimal TotalBeforeDiscount);
        bool Validate(int Cont, int ProductId);
    }
}