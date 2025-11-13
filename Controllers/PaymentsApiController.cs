using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayMopIntegration.Interfaces;
using System.Text.Json;

namespace PayMopIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsApiController : ControllerBase
    {
        private readonly IPaymobService _paymobService;
        private readonly ICourses _coursesRepoistory;
        private readonly IEnrollment _enrollmentsRepoistory;
        private readonly IStudent _studentRepoistory;
        private readonly IPayment _paymentRepoistory;
        private readonly IConfiguration _config;
        public PaymentsApiController(IPaymobService paymobService,
            ICourses coursesRepoistory,
            IEnrollment enrollmentsRepoistory,
            IStudent studentRepoistory,
            IConfiguration config,
            IPayment paymentRepoistory
            )
        {
            _paymobService = paymobService;
            _coursesRepoistory = coursesRepoistory;
            _enrollmentsRepoistory = enrollmentsRepoistory;
            _studentRepoistory = studentRepoistory;
            _config = config;
            _paymentRepoistory = paymentRepoistory;


        }
        [HttpPost("create/{StudentId}/{CourseId}")]
        public async Task<IActionResult> CreatePayment(int StudentId, int CourseId)
        {
            var student = await _studentRepoistory.GetById(StudentId);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            var course = await _coursesRepoistory.GetCourseById(CourseId);
            if (course == null)
            {
                return NotFound("Course not found");
            }

            var enrollment = await _enrollmentsRepoistory.EnrollStudentInCourse(StudentId, CourseId);
            if (enrollment == null)
            {
                return NotFound("Enrollment not found for the given student and course");
            }

            var amount = course.Price;
            var amountCents = amount * 100;
            var token = await _paymobService.GetAuthTokenAsync();
            var orderId = await _paymobService.CreateOrderAsync(token, amountCents, $"ENR-{enrollment.Id}");
            var paymentKey = await _paymobService.GeneratePaymentKeyAsync(token, orderId, amountCents, student);
            var iframeId = _config["Paymob:IframeId"];
            var paymentUrl = $"https://accept.paymob.com/api/acceptance/iframes/{iframeId}?payment_token={paymentKey}";
            var PaymentRecord = new PayMopIntegration.Entities.Payment
            {
                EnrollmentId = enrollment.Id,
                Amount = amount,
                PaymentDate = DateTime.UtcNow,
                Status = "Pending"
            };
            await _paymentRepoistory.CreatePaymentAsync(PaymentRecord);

            return Ok(new { paymentUrl });

        }
        [HttpPost("callback")]
        public async Task<IActionResult> Callback([FromBody] JsonElement data)
        {
            var success = data.GetProperty("obj").GetProperty("success").GetBoolean();
            var merchantOrderId = data.GetProperty("obj").GetProperty("order").GetProperty("merchant_order_id").GetString();
            var enrollmentId = int.Parse(merchantOrderId.Replace("ENR-", ""));
            var payment = await _paymentRepoistory.GetPaymentByEnrollmentIdAsync(enrollmentId);

            if (payment != null)
            {
                payment.Status = success ? "Paid" : "Failed";
                payment.TransactionId = data.GetProperty("obj").GetProperty("id").GetInt32().ToString();
                await _paymentRepoistory.UpdatePaymentAsync(payment);
            }

            return Ok();
        }
    }
}