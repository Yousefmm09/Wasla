using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Wasla.Data.Entite;
using Wasla.Infrustructure.Context;
using Wasla.Service.Abstract.Repositories;
using Wasla.Service.DTOs.Common;
using Wasla.Service.DTOs.Projects;
using Wasla.Service.DTOs.Proposals;

namespace Wasla.Infrustructure.Immplementation
{
    public class ProjectRepo :IProjectRepo
    {
        private readonly AppDb _appDb;
        private readonly IHttpContextAccessor _httpContext;
        public ProjectRepo(AppDb appDb , IHttpContextAccessor httpContext)
        {
            _appDb = appDb;
            _httpContext = httpContext;
        }
        public async Task<ApiResponse<CreateProjectRequest>> CreateProject(CreateProjectRequest request)
        {
            
            var CProject = new CreateProjectRequest
            {
                Title = request.Title,
                Description = request.Description,
                Deadline = request.Deadline,
                Category = request.Category,
                Skills = request.Skills,
                Budget = request.Budget,
            };
            var project = new Project
            {
                Title = CProject.Title,
                Description = CProject.Description,
                Deadline = CProject.Deadline,
                Category = CProject.Category,
                Skills = CProject.Skills,
                Budget = CProject.Budget,
            };
           await _appDb.Projects.AddAsync(project);
            await _appDb.SaveChangesAsync();
            return new ApiResponse<CreateProjectRequest>
                (
                Success: true,
                Message: "Project created successfully",
                Data: CProject,
                Errors: null

                );
            
        }

        public async Task<ApiResponse<CreateProposal>> CreatProposal(CreateProposal request)
        {
            var Porject = await _appDb.Projects.Where(x => x.Id == request.ProjectId).FirstOrDefaultAsync();
            if (Porject == null)
                return new ApiResponse<CreateProposal>
                (
                Success: false,
                Message: "not found Project",
                Data: null,
                Errors: null
                );
            var proposal = new Proposal
            {
                ProjectId = request.ProjectId,
                CoverLetter = request.CoverLetter,
                ProposedBudget = request.ProposedBudget,
                EstimatedDays = request.EstimatedDays,
                FreelancerId = request.FreelancerId,
                Status = ProposalStatus.Pending,
                CreatedAt = DateTimeOffset.UtcNow
            };
            await _appDb.Proposals.AddAsync(proposal);
            await _appDb.SaveChangesAsync();
            return new ApiResponse<CreateProposal>
                (
                Success: true,
                Message: "Proposal created successfully",
                Data: request,
                Errors: null
                );
        }

        public async Task<ApiResponse<string>> DeleteProject(int projectId)
        {
            var project = await _appDb.Projects.Where(x=>x.Id== projectId).FirstOrDefaultAsync();
            if(project==null)
                return new ApiResponse<string>
                (
                Success: false,
                Message: "not found Project",
                Data: null,
                Errors: null
                );
            _appDb.Projects.Remove(project);
            await _appDb.SaveChangesAsync();
            return new ApiResponse<string>
               (
               Success: true,
               Message: "Success to remove the project",
               Data: null,
               Errors: null
               );
        }

        public async Task<ApiResponse<List<ProjectDetailsResponse>>> GetAllProjects()
        {
            var Porject = await _appDb.Projects.Include(x=>x.Client).AsNoTracking().Select(x => new ProjectDetailsResponse
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Skills = x.Skills,
                Budget=x.Budget,
                Deadline=x.Deadline,
                Category=x.Category,
                CreatedAt=x.CreatedAt,
                ClientName=x.Client.UserName
            }).ToListAsync();
            if( Porject!=null )
                return new ApiResponse<List<ProjectDetailsResponse>>
                (
                    Success: true,
                    Message: "Projects retrieved successfully",
                    Data: Porject,
                    Errors: null
                );
            return new ApiResponse<List<ProjectDetailsResponse>>
            (
                Success: false,
                Message: "No projects found",
                Data: null,
                Errors: null
            );
        }

        public async Task<ApiResponse<List<ProposalResponse>>> GetAllProposals()
        {
            var proposals = await _appDb.Proposals.Include(x => x.Freelancer).Include(x => x.Project).AsNoTracking().Select(x => new ProposalResponse
            {
                Id = x.Id,
                ProjectId = x.ProjectId,
                ProjectTitle = x.Project.Title,
                CoverLetter = x.CoverLetter,
                ProposedBudget = x.ProposedBudget,
                EstimatedDays = x.EstimatedDays,
                Status = x.Status.ToString(),
                CreatedAt = x.CreatedAt,
                FreelancerId = x.FreelancerId,
                FreelancerName = x.Freelancer.UserName,
                FreelancerRating = x.Freelancer.Rating,
            }).ToListAsync();
            if (proposals != null)
                return new ApiResponse<List<ProposalResponse>>
                (
                    Success: true,
                    Message: "Proposals retrieved successfully",
                    Data: proposals,
                    Errors: null
                );
            return new ApiResponse<List<ProposalResponse>>
            (
                Success: false,
                Message: "No proposals found",
                Data: null,
                Errors: null
            );
        }

        public async Task<ApiResponse<ProjectDetailsResponse>> GetProjectDetails(int projectId)
        {
            var Porject = await _appDb.Projects.Include(x => x.Client).AsNoTracking().Select(x => new ProjectDetailsResponse
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Skills = x.Skills,
                Budget = x.Budget,
                Deadline = x.Deadline,
                Category = x.Category,
                CreatedAt = x.CreatedAt,
                ClientName = x.Client.UserName
            }).FirstOrDefaultAsync();
            if (Porject != null)
                return new ApiResponse<ProjectDetailsResponse>
                (
                    Success: true,
                    Message: "Projects retrieved successfully",
                    Data: Porject,
                    Errors: null
                );
            return new ApiResponse<ProjectDetailsResponse>
            (
                Success: false,
                Message: "No projects found",
                Data: null,
                Errors: null
            );
        }

        public async Task<ApiResponse<string>> SubmitProposal(int projectId,SubmitProposalRequest request)
        {
            var Proposal=await _appDb.Proposals.Where(x=>x.Id==projectId).FirstOrDefaultAsync();
            var user=_httpContext.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(Proposal==null)
                return new ApiResponse<string>
                (
                Success: false,
                Message: "not found Proposal",
                Data: null,
                Errors: null
                );
            Proposal.Status = ProposalStatus.Accepted;
            var contract = new Contract
            {
                ClientId = user,
                ProjectId = Proposal.ProjectId,
                ProposalId=Proposal.Id,
                FreelancerId = Proposal.FreelancerId,
                AgreedBudget = Proposal.ProposedBudget,
                DeliveredAt = DateTimeOffset.UtcNow.AddDays(Proposal.EstimatedDays),
            };
            await _appDb.Contracts.AddAsync(contract);
            await _appDb.SaveChangesAsync();
            return new ApiResponse<string>
            (
                Success: true,
                Message: "Proposal submitted successfully",
                Data: null,
                Errors: null
            );
        }

        public async Task<ProjectDetailsResponse> UpdateProject(int projectId, UpdateProjectRequest request)
        {
            var project =await _appDb.Projects.Where(x=>x.Id==projectId).FirstOrDefaultAsync();

            if(project != null)
            {

              project.Title = request.Title ?? project.Title;
                project.Description = request.Description ?? project.Description;
                project.Budget = request.Budget ?? project.Budget;
                project.Deadline = request.Deadline ?? project.Deadline;
                project.Category = request.Category ?? project.Category;
                project.Skills = request.Skills ?? project.Skills;
                project.Status  = project.Status;
                project.UpdatedAt   = DateTimeOffset.UtcNow;
                await _appDb.SaveChangesAsync();
                return new ProjectDetailsResponse
                {
                    Id = project.Id,
                    Title = project.Title,
                    Description = project.Description,
                    Skills = project.Skills,
                    Budget = project.Budget,
                    Deadline = project.Deadline,
                    Category = project.Category,
                    CreatedAt = project.CreatedAt,
                    UpdatedAt = project.UpdatedAt,
                };
            }
            return null;
        }
    }
}
