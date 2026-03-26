# Wasla — All DTOs

---

## 📁 Folder Structure

```
Wasla.Application/
└── DTOs/
    ├── Auth/
    │   ├── RegisterRequest.cs
    │   ├── LoginRequest.cs
    │   ├── AuthResponse.cs
    │   └── UserProfileResponse.cs
    ├── Projects/
    │   ├── CreateProjectRequest.cs
    │   ├── UpdateProjectRequest.cs
    │   ├── ProjectFilterRequest.cs
    │   ├── ProjectSummaryResponse.cs
    │   └── ProjectDetailsResponse.cs
    ├── Proposals/
    │   ├── SubmitProposalRequest.cs
    │   └── ProposalResponse.cs
    ├── Contracts/
    │   ├── DeliverWorkRequest.cs
    │   ├── DisputeRequest.cs
    │   └── ContractResponse.cs
    ├── Reviews/
    │   ├── SubmitReviewRequest.cs
    │   └── ReviewResponse.cs
    ├── Wallet/
    │   ├── TopUpRequest.cs
    │   ├── WalletResponse.cs
    │   └── TransactionResponse.cs
    ├── AI/
    │   └── AIPriceSuggestionResponse.cs
    ├── Admin/
    │   ├── ResolveDisputeRequest.cs
    │   └── AdminUserResponse.cs
    └── Common/
        ├── ApiResponse.cs
        └── PaginatedResult.cs
```

---

## 🔐 Auth DTOs

### `RegisterRequest.cs`
```csharp
namespace Wasla.Application.DTOs.Auth;

public class RegisterRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    /// "Client" or "Freelancer"
    public string Role { get; set; } = string.Empty;

    // Required only when Role == "Freelancer"
    public List<string>? Skills { get; set; }
}
```

### `LoginRequest.cs`
```csharp
namespace Wasla.Application.DTOs.Auth;

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
```

### `AuthResponse.cs`
```csharp
namespace Wasla.Application.DTOs.Auth;

public class AuthResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime TokenExpiry { get; set; }
}
```

### `UserProfileResponse.cs`
```csharp
namespace Wasla.Application.DTOs.Auth;

public class UserProfileResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public List<string> Skills { get; set; } = new();
    public double Rating { get; set; }
    public int ReviewCount { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

---

## 📋 Projects DTOs

### `CreateProjectRequest.cs`
```csharp
namespace Wasla.Application.DTOs.Projects;

public class CreateProjectRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public DateTime Deadline { get; set; }
    public string Category { get; set; } = string.Empty;
    public List<string> Skills { get; set; } = new();
}
```

### `UpdateProjectRequest.cs`
```csharp
namespace Wasla.Application.DTOs.Projects;

// All fields nullable — only send what changed
public class UpdateProjectRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? Budget { get; set; }
    public DateTime? Deadline { get; set; }
    public string? Category { get; set; }
    public List<string>? Skills { get; set; }
}
```

### `ProjectFilterRequest.cs`
```csharp
namespace Wasla.Application.DTOs.Projects;

// Query params: GET /api/projects?category=Backend&minBudget=500&page=1
public class ProjectFilterRequest
{
    public string? Category { get; set; }
    public decimal? MinBudget { get; set; }
    public decimal? MaxBudget { get; set; }
    public List<string>? Skills { get; set; }

    // "Open" | "InProgress" | "Completed" | null = all
    public string? Status { get; set; }

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
```

### `ProjectSummaryResponse.cs`
```csharp
namespace Wasla.Application.DTOs.Projects;

// Lightweight — used in paginated list GET /api/projects
public class ProjectSummaryResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public DateTime Deadline { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<string> Skills { get; set; } = new();
    public int ProposalCount { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
```

### `ProjectDetailsResponse.cs`
```csharp
namespace Wasla.Application.DTOs.Projects;

// Full details — used in GET /api/projects/{id}
public class ProjectDetailsResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public DateTime Deadline { get; set; }
    public string Category { get; set; } = string.Empty;
    public List<string> Skills { get; set; } = new();
    public string Status { get; set; } = string.Empty;
    public int ProposalCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Nested client info
    public Guid ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public double ClientRating { get; set; }
}
```

---

## 📩 Proposals DTOs

### `SubmitProposalRequest.cs`
```csharp
namespace Wasla.Application.DTOs.Proposals;

public class SubmitProposalRequest
{
    public string CoverLetter { get; set; } = string.Empty;
    public decimal ProposedBudget { get; set; }
    public int EstimatedDays { get; set; }
}
```

### `ProposalResponse.cs`
```csharp
namespace Wasla.Application.DTOs.Proposals;

public class ProposalResponse
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string ProjectTitle { get; set; } = string.Empty;
    public string CoverLetter { get; set; } = string.Empty;
    public decimal ProposedBudget { get; set; }
    public int EstimatedDays { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    // Freelancer info (shown to client when reviewing proposals)
    public Guid FreelancerId { get; set; }
    public string FreelancerName { get; set; } = string.Empty;
    public double FreelancerRating { get; set; }
    public List<string> FreelancerSkills { get; set; } = new();
}
```

---

## 📜 Contracts DTOs

### `DeliverWorkRequest.cs`
```csharp
namespace Wasla.Application.DTOs.Contracts;

public class DeliverWorkRequest
{
    public string DeliveryNote { get; set; } = string.Empty;
}
```

### `DisputeRequest.cs`
```csharp
namespace Wasla.Application.DTOs.Contracts;

public class DisputeRequest
{
    public string Reason { get; set; } = string.Empty;
}
```

### `ContractResponse.cs`
```csharp
namespace Wasla.Application.DTOs.Contracts;

public class ContractResponse
{
    public Guid Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal AgreedBudget { get; set; }
    public string? DeliveryNote { get; set; }
    public string? DisputeReason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    // Project info
    public Guid ProjectId { get; set; }
    public string ProjectTitle { get; set; } = string.Empty;

    // Parties
    public Guid ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public Guid FreelancerId { get; set; }
    public string FreelancerName { get; set; } = string.Empty;
}
```

---

## ⭐ Reviews DTOs

### `SubmitReviewRequest.cs`
```csharp
namespace Wasla.Application.DTOs.Reviews;

public class SubmitReviewRequest
{
    /// Must be between 1 and 5
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}
```

### `ReviewResponse.cs`
```csharp
namespace Wasla.Application.DTOs.Reviews;

public class ReviewResponse
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    // Who wrote it
    public Guid ReviewerId { get; set; }
    public string ReviewerName { get; set; } = string.Empty;

    // Who received it
    public Guid RevieweeId { get; set; }
    public string RevieweeName { get; set; } = string.Empty;
}
```

---

## 💰 Wallet DTOs

### `TopUpRequest.cs`
```csharp
namespace Wasla.Application.DTOs.Wallet;

public class TopUpRequest
{
    public decimal Amount { get; set; }
}
```

### `WalletResponse.cs`
```csharp
namespace Wasla.Application.DTOs.Wallet;

public class WalletResponse
{
    public Guid Id { get; set; }
    public decimal Balance { get; set; }
    public decimal EscrowBalance { get; set; }
    public decimal TotalBalance { get; set; }
}
```

### `TransactionResponse.cs`
```csharp
namespace Wasla.Application.DTOs.Wallet;

public class TransactionResponse
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }

    // "TopUp" | "Escrow" | "Release" | "Refund"
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // null for TopUp transactions
    public Guid? ContractId { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

---

## 🤖 AI DTOs

### `AIPriceSuggestionResponse.cs`
```csharp
namespace Wasla.Application.DTOs.AI;

public class AIPriceSuggestionResponse
{
    public decimal SuggestedMin { get; set; }
    public decimal SuggestedMax { get; set; }
    public string Reasoning { get; set; } = string.Empty;
}
```

---

## 🛡️ Admin DTOs

### `ResolveDisputeRequest.cs`
```csharp
namespace Wasla.Application.DTOs.Admin;

public class ResolveDisputeRequest
{
    // "Client" or "Freelancer" — who receives the escrowed funds
    public string FundsGoTo { get; set; } = string.Empty;
    public string AdminNote { get; set; } = string.Empty;
}
```

### `AdminUserResponse.cs`
```csharp
namespace Wasla.Application.DTOs.Admin;

public class AdminUserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool IsBanned { get; set; }
    public double Rating { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

---

## 🔧 Common DTOs

### `ApiResponse.cs`
```csharp
namespace Wasla.Application.DTOs.Common;

// Standard envelope — wrap ALL controller responses in this
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    public static ApiResponse<T> Ok(T data, string message = "Success")
        => new() { Success = true, Message = message, Data = data };

    public static ApiResponse<T> Fail(string message, List<string>? errors = null)
        => new() { Success = false, Message = message, Errors = errors };
}
```

### `PaginatedResult.cs`
```csharp
namespace Wasla.Application.DTOs.Common;

// Generic wrapper for all paginated endpoints
public class PaginatedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasNext => Page < TotalPages;
    public bool HasPrev => Page > 1;
}
```

---

## 💡 Usage Examples

### Controller returning `ApiResponse`
```csharp
// Success
return Ok(ApiResponse<AuthResponse>.Ok(response));

// Fail
return BadRequest(ApiResponse<AuthResponse>.Fail("Invalid credentials"));
```

### Controller returning paginated list
```csharp
var result = new PaginatedResult<ProjectSummaryResponse>
{
    Items = projects,
    TotalCount = totalCount,
    Page = request.Page,
    PageSize = request.PageSize
};

return Ok(ApiResponse<PaginatedResult<ProjectSummaryResponse>>.Ok(result));
```

### Mapping Entity → DTO (manual mapping example)
```csharp
var response = new ProposalResponse
{
    Id = proposal.Id,
    ProjectId = proposal.ProjectId,
    ProjectTitle = proposal.Project.Title,
    CoverLetter = proposal.CoverLetter,
    ProposedBudget = proposal.ProposedBudget,
    EstimatedDays = proposal.EstimatedDays,
    Status = proposal.Status.ToString(),
    CreatedAt = proposal.CreatedAt,
    FreelancerId = proposal.FreelancerId,
    FreelancerName = proposal.Freelancer.Name,
    FreelancerRating = proposal.Freelancer.Rating,
    FreelancerSkills = proposal.Freelancer.Skills
};
```
