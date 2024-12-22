using BackendApiTest.Contracts.Payments;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        public CoutryhouseeContext Context { get; }

        public PaymentsController(CoutryhouseeContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получить список всех платежей.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var payments = Context.Payments.ToList();
            var response = payments.Adapt<List<GetPaymentsResponse>>();
            return Ok(response);
        }

        /// <summary>
        /// Получить платеж по идентификаторам.
        /// </summary>
        [HttpGet("{paymentsId}/{userId}")]
        public IActionResult GetById(int paymentsId, int userId)
        {
            var payment = Context.Payments
                .FirstOrDefault(x => x.PaymentsId == paymentsId && x.UserId == userId);

            if (payment == null) return BadRequest("Not Found");

            return Ok(payment.Adapt<GetPaymentsResponse>());
        }

        /// <summary>
        /// Добавить новый платеж.
        /// </summary>
        [HttpPost]
        public IActionResult Add(CreatePaymentsRequest request)
        {
            var payment = request.Adapt<Payment>();
            Context.Payments.Add(payment);
            Context.SaveChanges();
            return Ok(payment.Adapt<GetPaymentsResponse>());
        }

        /// <summary>
        /// Обновить платеж.
        /// </summary>
        [HttpPut("{paymentsId}/{userId}")]
        public IActionResult Update(CreatePaymentsRequest request, int paymentsId, int userId)
        {
            var existingPayment = Context.Payments
                .FirstOrDefault(x => x.PaymentsId == paymentsId && x.UserId == userId);

            if (existingPayment == null) return BadRequest("Not Found");

            request.Adapt(existingPayment);
            Context.SaveChanges();
            return Ok(existingPayment.Adapt<GetPaymentsResponse>());
        }

        /// <summary>
        /// Удалить платеж.
        /// </summary>
        [HttpDelete("{paymentsId}/{userId}")]
        public IActionResult Delete(int paymentsId, int userId)
        {
            var payment = Context.Payments
                .FirstOrDefault(x => x.PaymentsId == paymentsId && x.UserId == userId);

            if (payment == null) return BadRequest("Not Found");

            Context.Payments.Remove(payment);
            Context.SaveChanges();
            return Ok();
        }
    }
}
