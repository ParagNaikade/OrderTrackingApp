[![Parallel Docker Builds with Cache](https://github.com/ParagNaikade/OrderTrackingApp/actions/workflows/ci-pipeline.yml/badge.svg)](https://github.com/ParagNaikade/OrderTrackingApp/actions/workflows/ci-pipeline.yml)

# 🧾 Order Tracking System

A distributed order processing system built with **.NET Clean Architecture**, featuring **CQRS**, **RabbitMQ messaging**, and dual persistence in **SQL Server** and **MongoDB**.  

This project demonstrates a scalable microservice pattern using domain events and message queues to decouple the system.

---

## 📐 Architecture Overview

```
API ──> Application ──> Infra ──> RabbitMQ ──> Consumer
                           │                     │
                      SQL Server            MongoDB
```

### 🔄 Flow Description

1. **API**
   - Exposes endpoints to create and retrieve orders.
   - Triggers a `SaveOrderCommand` when a POST request is received.

2. **Application**
   - Implements **CQRS** to separate command and query logic.
   - Handles business rules (e.g., inventory validation).
   - Saves the order to **SQL Server**.
   - Raises an `OrderCreatedEvent` as a domain event.

3. **Infra**
   - Listens for domain events.
   - Publishes messages to **RabbitMQ** queue (`order.created`).

4. **Consumer**
   - Subscribes to the RabbitMQ queue.
   - Reads order details from SQL.
   - Syncs the order into **MongoDB** for read-side optimization or analytics.

---

## 🛠️ Tech Stack

| Layer        | Technology                     |
|--------------|--------------------------------|
| API          | ASP.NET Core Web API           |
| Application  | MediatR, CQRS, FluentValidation|
| Infra        | Entity Framework Core, RabbitMQ|
| Consumer     | Background Worker, MongoDB     |
| Messaging    | RabbitMQ                       |
| Databases    | SQL Server, MongoDB            |
| Architecture | Clean Architecture             |
| DevOps       | Docker, GitHub Actions (CI/CD) |

---

## 📂 Project Structure

```
OrderTrackingApp/
├── API                     # Entry point
├── Application             # CQRS, Commands/Queries, COre app logic
├── Application.Contracts   # Interfaces, DTOs, Validation
├── Domain                  # Entities, Value Objects, Events
├── Infrastructure          # Event Handlers, RabbitMQ
├── Consumer                # Background service that syncs to MongoDB
├── ReadPersistence         # MongoDB Read Models
├── Persistence             # EF Core
└── docker-compose.yml      # Services for RabbitMQ, SQL Server, MongoDB
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/)
- [MongoDB Compass](https://www.mongodb.com/products/compass) (optional for viewing MongoDB)
- [RabbitMQ Management UI](http://localhost:15672) (user: `guest`, password: `guest`)

### Run Locally

```bash
git clone https://github.com/ParagNaikade/order-tracking-app.git
cd order-tracking-app
docker-compose up --build
```

### Test API

- `POST /api/orders` → Creates an order
- `GET /api/orders/{id}` → Retrieves an order
- `GET /api/orders` → Retrieves paginated list

---

## 🧪 Features

- ✅ Clean Architecture (Separation of Concerns)
- ✅ CQRS with MediatR
- ✅ Domain Events and Messaging
- ✅ Async Communication via RabbitMQ
- ✅ SQL Write DB & MongoDB Read DB (eventual consistency)
- ✅ Dockerized Microservices

---

## 📬 Contact

Created with ❤️ by [Parag Naikade](https://paragnaikade.com/)
