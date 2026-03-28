using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasla.Service.DTOs.Common;
using Wasla.Service.DTOs.Projects;
using Wasla.Service.DTOs.Proposals;

namespace Wasla.Service.Abstract.Services
{
    public interface IProjectSerivce
    {
        Task<ApiResponse<List<ProjectDetailsResponse>>> GetAllProjects();
        Task<ApiResponse<ProjectDetailsResponse>> GetProjectDetails(int projectId);
        Task<ApiResponse<CreateProjectRequest>> CreateProject(CreateProjectRequest request);
        Task<ProjectDetailsResponse> UpdateProject(int projectId, UpdateProjectRequest request);
        Task<ApiResponse<string>> DeleteProject(int projectId);
        Task<ApiResponse<CreateProposal>> CreatProposal(CreateProposal request);
        Task<ApiResponse<List<ProposalResponse>>> GetAllProposals();
        Task<ApiResponse<string>> SubmitProposal(int projectId, SubmitProposalRequest request);
    }
}
