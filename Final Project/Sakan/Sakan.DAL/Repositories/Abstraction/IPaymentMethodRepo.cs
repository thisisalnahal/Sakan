using Sakan.DAL.Entities;

namespace Sakan.DAL.Repositories.Abstraction
{
    internal interface IPaymentMethodRepo
    {
        (bool Success, string Message) Add(PaymentMethod method);

        (bool Success, string Message, PaymentMethod? Data) GetById(int id);

        (bool Success, string Message, List<PaymentMethod> Data) GetByUser(string userId, bool activeOnly);

        (bool Success, string Message, PaymentMethod? Data) GetDefault(string userId);

        (bool Success, string Message) SetDefault(string userId, int methodId);

        (bool Success, string Message) Update(PaymentMethod method);
    }
}
