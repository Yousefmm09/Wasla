# 🔗 Wasla — Project Documentation

> **ASP.NET Core Web API** | Clean Architecture + CQRS + MediatR | PostgreSQL + EF Core

---

## 📁 Folder Structure

```
Wasla/
├── Wasla.API/                          # Presentation Layer
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── ProjectsController.cs
│   │   ├── ProposalsController.cs
│   │   ├── ContractsController.cs
│   │   ├── ReviewsController.cs
│   │   ├── WalletController.cs
│   │   └── AdminController.cs
│   ├── Middleware/
│   │   ├── ExceptionHandlingMiddleware.cs
│   │   └── RoleAuthorizationMiddleware.cs
│   ├── Extensions/
│   │   └── ServiceExtensions.cs
│   ├── appsettings.json
│   └── Program.cs
│
├── Wasla.Application/                  # Application Layer (CQRS)
│   ├── Common/
│   │   ├── BaseResponse.cs
│   │   └── PaginatedResult.cs
│   ├── Features/
│   │   ├── Auth/
│   │   │   ├── Commands/
│   │   │   │   ├── RegisterCommand.cs
│   │   │   │   └── LoginCommand.cs
│   │   │   └── Handlers/
│   │   │       ├── RegisterHandler.cs
│   │   │       └── LoginHandler.cs
│   │   ├── Projects/
│   │   │   ├── Commands/
│   │   │   │   ├── CreateProjectCommand.cs
│   │   │   │   ├── UpdateProjectCommand.cs
│   │   │   │   └── DeleteProjectCommand.cs
│   │   │   ├── Queries/
│   │   │   │   ├── GetProjectsQuery.cs
│   │   │   │   └── GetProjectByIdQuery.cs
│   │   │   └── Handlers/
│   │   ├── Proposals/
│   │   │   ├── Commands/
│   │   │   │   ├── SubmitProposalCommand.cs
│   │   │   │   ├── AcceptProposalCommand.cs
│   │   │   │   └── RejectProposalCommand.cs
│   │   │   └── Handlers/
│   │   ├── Contracts/
│   │   │   ├── Commands/
│   │   │   │   ├── DeliverWorkCommand.cs
│   │   │   │   └── ApproveDeliveryCommand.cs
│   │   │   └── Handlers/
│   │   ├── Reviews/
│   │   │   ├── Commands/
│   │   │   │   └── SubmitReviewCommand.cs
│   │   │   └── Handlers/
│   │   ├── Wallet/
│   │   │   ├── Commands/
│   │   │   │   └── TopUpWalletCommand.cs
│   │   │   └── Queries/
│   │   │       └── GetWalletBalanceQuery.cs
│   │   └── AI/
│   │       ├── Queries/
│   │       │   └── GetPriceSuggestionQuery.cs
│   │       └── Handlers/
│   └── Interfaces/
│       ├── IAuthService.cs
│       ├── IProjectRepository.cs
│       ├── IProposalRepository.cs
│       ├── IContractRepository.cs
│       ├── IWalletRepository.cs
│       └── IAIService.cs
│
├── Wasla.Domain/                       # Domain Layer
│   ├── Entities/
│   │   ├── User.cs
│   │   ├── Project.cs
│   │   ├── Proposal.cs
│   │   ├── Contract.cs
│   │   ├── Review.cs
│   │   ├── Wallet.cs
│   │   └── WalletTransaction.cs
│   └── Enums/
│       ├── UserRole.cs          (Client, Freelancer, Admin)
│       ├── ProjectStatus.cs     (Open, InProgress, Completed, Cancelled)
│       ├── ProposalStatus.cs    (Pending, Accepted, Rejected)
│       ├── ContractStatus.cs    (Active, Delivered, Approved, Disputed)
│       └── TransactionType.cs   (TopUp, Escrow, Release, Refund)
│
└── Wasla.Infrastructure/               # Infrastructure Layer
    ├── Data/
    │   ├── AppDbContext.cs
    │   └── Migrations/
    ├── Repositories/
    │   ├── ProjectRepository.cs
    │   ├── ProposalRepository.cs
    │   ├── ContractRepository.cs
    │   └── WalletRepository.cs
    ├── Services/
    │   ├── AuthService.cs
    │   ├── JwtService.cs
    │   ├── AIService.cs           (Claude API integration)
    │   └── NotificationService.cs (Hangfire)
    └── Jobs/
        └── NotificationJob.cs
```

---

## 🔌 API Endpoints

### 🔐 Auth
| Method | Endpoint | Role | Description |
|--------|----------|------|-------------|
| POST | `/api/auth/register` | Public | تسجيل يوزر جديد (Client / Freelancer) |
| POST | `/api/auth/login` | Public | تسجيل الدخول وإرجاع JWT |
| GET | `/api/auth/me` | Authenticated | بيانات اليوزر الحالي |

**Register Body:**
```json
{
  "name": "Yousef Ahmed",
  "email": "yousef@example.com",
  "password": "StrongPass123!",
  "role": "Freelancer",
  "skills": ["ASP.NET Core", "PostgreSQL", "React"]
}
```

---

### 📋 Projects
| Method | Endpoint | Role | Description |
|--------|----------|------|-------------|
| GET | `/api/projects` | Public | جلب كل البروجيكتات مع فلتر وباجينيشن |
| GET | `/api/projects/{id}` | Public | تفاصيل بروجيكت معين |
| POST | `/api/projects` | Client | نشر بروجيكت جديد |
| PUT | `/api/projects/{id}` | Client (Owner) | تعديل البروجيكت |
| DELETE | `/api/projects/{id}` | Client (Owner) | حذف البروجيكت |

**Query Params للـ GET /projects:**
```
?category=Backend
&minBudget=500
&maxBudget=5000
&skills=ASP.NET Core,PostgreSQL
&status=Open
&page=1
&pageSize=10
```

**Create Project Body:**
```json
{
  "title": "بناء API لتطبيق حجز مواعيد",
  "description": "محتاج ASP.NET Core Web API...",
  "budget": 3000,
  "deadline": "2024-03-01",
  "category": "Backend",
  "skills": ["ASP.NET Core", "PostgreSQL"]
}
```

---

### 📩 Proposals
| Method | Endpoint | Role | Description |
|--------|----------|------|-------------|
| POST | `/api/projects/{id}/proposals` | Freelancer | تقديم proposal على بروجيكت |
| GET | `/api/projects/{id}/proposals` | Client (Owner) | جلب كل proposals البروجيكت |
| GET | `/api/proposals/mine` | Freelancer | proposals بتاعتي |
| PUT | `/api/proposals/{id}/accept` | Client | قبول proposal |
| PUT | `/api/proposals/{id}/reject` | Client | رفض proposal |

**Submit Proposal Body:**
```json
{
  "coverLetter": "أنا مهتم بالبروجيكت ده وعندي خبرة...",
  "proposedBudget": 2500,
  "estimatedDays": 14
}
```

---

### 📜 Contracts
| Method | Endpoint | Role | Description |
|--------|----------|------|-------------|
| GET | `/api/contracts/{id}` | Client / Freelancer | تفاصيل الكونتراكت |
| GET | `/api/contracts/mine` | Authenticated | كونتراكتات اليوزر |
| PUT | `/api/contracts/{id}/deliver` | Freelancer | رفع تسليم الشغل |
| PUT | `/api/contracts/{id}/approve` | Client | قبول التسليم وتحرير المبلغ |
| PUT | `/api/contracts/{id}/dispute` | Client | فتح dispute |

> **ملاحظة:** الكونتراكت بيتعمل تلقائياً لما الـ Client يقبل الـ Proposal

---

### ⭐ Reviews
| Method | Endpoint | Role | Description |
|--------|----------|------|-------------|
| POST | `/api/contracts/{id}/review` | Client / Freelancer | تقييم بعد اتمام الشغل |
| GET | `/api/users/{id}/reviews` | Public | تقييمات يوزر معين |

**Submit Review Body:**
```json
{
  "rating": 5,
  "comment": "شغل ممتاز وفي الوقت المحدد"
}
```

---

### 💰 Wallet
| Method | Endpoint | Role | Description |
|--------|----------|------|-------------|
| GET | `/api/wallet` | Authenticated | رصيد المحفظة |
| POST | `/api/wallet/topup` | Client | شحن المحفظة |
| GET | `/api/wallet/transactions` | Authenticated | سجل المعاملات |

---

### 🤖 AI
| Method | Endpoint | Role | Description |
|--------|----------|------|-------------|
| GET | `/api/ai/price-suggestion/{projectId}` | Freelancer | اقتراح سعر مناسب للـ Proposal |

**Response:**
```json
{
  "suggestedMin": 2000,
  "suggestedMax": 3500,
  "reasoning": "بناءً على الـ skills المطلوبة والـ deadline وحجم الشغل..."
}
```

---

### 🛡️ Admin
| Method | Endpoint | Role | Description |
|--------|----------|------|-------------|
| GET | `/api/admin/users` | Admin | كل اليوزرز |
| PUT | `/api/admin/users/{id}/ban` | Admin | حظر يوزر |
| GET | `/api/admin/disputes` | Admin | عرض الـ disputes |
| PUT | `/api/admin/disputes/{id}/resolve` | Admin | حل الـ dispute وتحديد مين ياخد الفلوس |

---

## 🔄 Full Workflow

```
┌─────────────────────────────────────────────────────────────┐
│                        WASLA WORKFLOW                        │
└─────────────────────────────────────────────────────────────┘

1️⃣  REGISTRATION & AUTH
    ┌─────────┐     Register (Client/Freelancer)     ┌────────┐
    │  User   │ ──────────────────────────────────▶  │  JWT   │
    └─────────┘                                       └────────┘

2️⃣  PROJECT POSTING
    ┌────────┐    POST /api/projects    ┌──────────────────────┐
    │ Client │ ───────────────────────▶ │ Project (Status=Open) │
    └────────┘                          └──────────────────────┘

3️⃣  PROPOSAL SUBMISSION
    ┌────────────┐   POST /api/projects/{id}/proposals   ┌──────────────────────────┐
    │ Freelancer │ ────────────────────────────────────▶ │ Proposal (Status=Pending) │
    └────────────┘                                        └──────────────────────────┘
          │
          │ (Optional) GET /api/ai/price-suggestion/{projectId}
          ▼
    ┌─────────────────────┐
    │ AI Price Suggestion │
    └─────────────────────┘

4️⃣  PROPOSAL ACCEPTANCE + ESCROW
    ┌────────┐   PUT /api/proposals/{id}/accept   ┌──────────────────────────────────┐
    │ Client │ ─────────────────────────────────▶ │ System:                          │
    └────────┘                                     │  - Proposal → Accepted           │
                                                   │  - Project  → InProgress         │
                                                   │  - Contract → Created (Active)   │
                                                   │  - Wallet   → Escrow (lock fund) │
                                                   └──────────────────────────────────┘

5️⃣  WORK DELIVERY
    ┌────────────┐   PUT /api/contracts/{id}/deliver   ┌────────────────────────────────┐
    │ Freelancer │ ──────────────────────────────────▶ │ Contract (Status=Delivered)    │
    └────────────┘                                      │ + Hangfire: notify Client      │
                                                        └────────────────────────────────┘

6️⃣  DELIVERY APPROVAL + FUND RELEASE
    ┌────────┐   PUT /api/contracts/{id}/approve   ┌────────────────────────────────────┐
    │ Client │ ────────────────────────────────── ▶│ System:                            │
    └────────┘                                      │  - Contract  → Approved            │
                                                    │  - Project   → Completed           │
                                                    │  - Wallet    → Release to          │
                                                    │               Freelancer           │
                                                    └────────────────────────────────────┘

7️⃣  REVIEW (MUTUAL)
    ┌────────┐                          ┌────────────┐
    │ Client │  POST /contracts/{id}/   │ Freelancer │
    │        │  review  ◀────────────▶  │            │
    └────────┘    (Both can review)     └────────────┘

8️⃣  DISPUTE FLOW (if Client disagrees)
    ┌────────┐   PUT /contracts/{id}/dispute   ┌───────────────────┐
    │ Client │ ──────────────────────────────▶ │ Contract=Disputed  │
    └────────┘                                  └───────────────────┘
                                                         │
                                          PUT /admin/disputes/{id}/resolve
                                                         │
                                                         ▼
                                               ┌─────────────────┐
                                               │ Admin Decision:  │
                                               │ Fund → Client   │
                                               │  OR             │
                                               │ Fund → Freelan. │
                                               └─────────────────┘
```

---

## 💾 Database Schema (Key Tables)

```sql
-- Users
Users (Id, Name, Email, PasswordHash, Role, Skills[], Rating, CreatedAt)

-- Projects
Projects (Id, ClientId, Title, Description, Budget, Deadline, 
          Category, Skills[], Status, CreatedAt)

-- Proposals
Proposals (Id, ProjectId, FreelancerId, CoverLetter, 
           ProposedBudget, EstimatedDays, Status, CreatedAt)

-- Contracts
Contracts (Id, ProjectId, ProposalId, ClientId, FreelancerId,
           AgreedBudget, Status, DeliveryNote, CreatedAt, CompletedAt)

-- Reviews
Reviews (Id, ContractId, ReviewerId, RevieweeId, Rating, Comment, CreatedAt)

-- Wallets
Wallets (Id, UserId, Balance, EscrowBalance)

-- WalletTransactions
WalletTransactions (Id, WalletId, Amount, Type, ContractId, Description, CreatedAt)
```

---

## 🛠️ Tech Stack Summary

| Layer | Technology |
|-------|------------|
| Framework | ASP.NET Core 8 Web API |
| Architecture | Clean Architecture + CQRS + MediatR |
| ORM | Entity Framework Core |
| Database | PostgreSQL |
| Auth | JWT Bearer Tokens |
| Background Jobs | Hangfire |
| AI Integration | Claude API (Anthropic) |
| Validation | FluentValidation |
| Error Handling | Global Exception Middleware |

---

## 📦 NuGet Packages

```xml
<!-- Application -->
<PackageReference Include="MediatR" />
<PackageReference Include="FluentValidation.AspNetCore" />
<PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" />

<!-- Infrastructure -->
<PackageReference Include="Microsoft.EntityFrameworkCore" />
<PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" />
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" />
<PackageReference Include="BCrypt.Net-Next" />
<PackageReference Include="Hangfire.AspNetCore" />
<PackageReference Include="Hangfire.PostgreSql" />
```

---

## ✅ 2-Week Development Plan

| Day | Task |
|-----|------|
| 1 | Setup solution, Clean Architecture folders, DI registration |
| 2 | Auth — Register/Login/JWT, Role-based Authorization |
| 3 | Projects — Create, Update, Delete |
| 4 | Projects — Search, Filter, Pagination |
| 5 | Proposals — Submit, Accept, Reject |
| 6 | Contract auto-creation + Escrow wallet logic |
| 7 | Wallet — TopUp, Transactions history |
| 8 | Contract Delivery + Approval + Fund Release |
| 9 | Reviews & Ratings system |
| 10 | Hangfire — Notification background jobs |
| 11 | AI Integration — Price suggestion via Claude API |
| 12 | Admin endpoints — Users, Disputes |
| 13 | Global Exception Middleware, FluentValidation, cleanup |
| 14 | README, Swagger docs, push to GitHub |
```
