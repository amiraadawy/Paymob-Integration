⭐ Paymob Card Payment Integration (ASP.NET Core)

This project demonstrates a clean and structured implementation of the Paymob online card payment workflow in ASP.NET Core using a service-based design.

It includes:

Authentication

Order creation

Payment key generation

Iframe redirection

Callback handling

Integration with courses, students, and enrollments

🚀 Features

Fully implemented Paymob payment flow

Clean code following SOLID & separation of concerns

Uses Repository Pattern + Service Layer

Secure callback handling

Stores full payment records (amount, enrollment, transaction ID, status, method)

Redirects users to Paymob’s hosted card-payment iframe

Works with your own DB entities (Student, Course, Enrollment, Payment)

🛠 Technologies

ASP.NET Core MVC 8

C#

Repository Pattern

Dependency Injection

HttpClient

SQL Database

Paymob REST API

⚙️ Payment Workflow
1️⃣ Get Authentication Token

Your server sends the Paymob API key → receives a valid authentication token.

2️⃣ Create Order

A Paymob order is created with:

Amount in cents

Unique merchant order ID

Delivery settings

Currency

3️⃣ Generate Payment Key

A secure payment key is generated using:

Order ID

Billing data (student)

Integration ID

Payment amount

4️⃣ Redirect User to Iframe Checkout

Final payment page:

https://accept.paymob.com/api/acceptance/iframes/{IframeId}?payment_token={token}

5️⃣ Handle Callback

Paymob sends a JSON callback after payment.

Your backend processes:

Payment success or failure

Transaction ID

Merchant order ID

Updates database payment record

📦 Paymob Service (Core Logic)

Your Paymob integration is handled in a clean, isolated service:

✔ GetAuthTokenAsync()

Authenticates with Paymob.

✔ CreateOrderAsync()

Creates a new Paymob order.

✔ GeneratePaymentKeyAsync()

Generates a payment key for the iframe.

📁 Project Structure (recommended)
/PayMopIntegration
│── Controllers/
│── Entities/
│── Interfaces/
│── Repository/
│── Services/
│── Views/
└── README.md

🔧 Configuration (appsettings.json)
"Paymob": {
  "ApiKey": "YOUR_API_KEY",
  "IntegrationId": "YOUR_INTEGRATION_ID",
  "IframeId": "YOUR_IFRAME_ID",
  "BaseUrl": "https://accept.paymob.com/api"
}

📡 Callback Endpoint

Your backend receives Paymob’s callback and updates:

Status: Paid / Failed

Transaction ID

Enrollment ID
