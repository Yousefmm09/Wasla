using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasla.Service.Abstract.Repositories;
using Wasla.Service.Abstract.Services;
using Wasla.Service.DTOs.Common;
using Wasla.Service.DTOs.Projects;
using Wasla.Service.DTOs.Proposals;

namespace Wasla.Service.Immplementation
{
    public class ProjectSerivce : IProjectSerivce
    {
        private readonly IProjectRepo _projectRepo;
        public ProjectSerivce(IProjectRepo projectRepo)
        {
            _projectRepo = projectRepo;
        }
        public async Task<ApiResponse<CreateProjectRequest>> CreateProject(CreateProjectRequest request)
        {
           var res=await _projectRepo.CreateProject(request);
            return res;
        }

        public Task<ApiResponse<CreateProposal>> CreatProposal(CreateProposal request)
        {
            return _projectRepo.CreatProposal(request);
        }

        public Task<ApiResponse<string>> DeleteProject(int projectId)
        {
            return _projectRepo.DeleteProject(projectId);
        }

        public Task<ApiResponse<List<ProjectDetailsResponse>>> GetAllProjects()
        {
            return _projectRepo.GetAllProjects();
        }

        public Task<ApiResponse<List<ProposalResponse>>> GetAllProposals()
        {
            return _projectRepo.GetAllProposals();
        }

        public Task<ApiResponse<ProjectDetailsResponse>> GetProjectDetails(int projectId)
        {
            return _projectRepo.GetProjectDetails(projectId);
        }

        public Task<ApiResponse<string>> SubmitProposal(int projectId, SubmitProposalRequest request)
        {
            var res = _projectRepo.SubmitProposal(projectId, request);
            return res;
        }

        public Task<ProjectDetailsResponse> UpdateProject(int projectId, UpdateProjectRequest request)
        {
            return _projectRepo.UpdateProject(projectId, request);
        }
    }
}
