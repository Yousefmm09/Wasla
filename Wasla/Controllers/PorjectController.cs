using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wasla.Service.Abstract.Services;
using Wasla.Service.DTOs.Auth;
using Wasla.Service.DTOs.Projects;
using Wasla.Service.DTOs.Proposals;

namespace Wasla.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectSerivce _projectService;
        public ProjectController(IProjectSerivce projectService)
        {
            _projectService = projectService;
        }
        [HttpGet("GetAllProjects")]
        public async Task<IActionResult> GetAllProjects()
        {
            var res = await _projectService.GetAllProjects();
            return Ok(res);
        }
        [HttpGet("GetProjectDetails/{projectId}")]
        public async Task<IActionResult> GetProjectDetails(int projectId)
        {
            var res = await _projectService.GetProjectDetails(projectId);
            return Ok(res);
        }
        [HttpPost("CreateProject")]
        public async Task<IActionResult> CreateProject(CreateProjectRequest request)
        {
            var res = await _projectService.CreateProject(request);
            return Ok(res);
        }
        [HttpPatch("UpdateProject/{projectId}")]
        public async Task<IActionResult> UpdateProject([FromRoute] int projectId, [FromBody] UpdateProjectRequest request)
        {
            var res = await _projectService.UpdateProject(projectId, request);
            return Ok(res);
        }
        [HttpDelete("DeleteProject/{projectId}")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            var res = await _projectService.DeleteProject(projectId);
            return Ok(res);
        }
        [HttpPost("SubmitProposal/{projectId}")]
        public async Task<IActionResult> SubmitProposal(int projectId, SubmitProposalRequest request)
        {
            var res = await _projectService.SubmitProposal(projectId, request);
            return Ok(res);
        }
        [HttpPost("CreateProposal")]
        public async Task<IActionResult> CreateProposal(CreateProposal request)
        {
            var res = await _projectService.CreatProposal(request);
            return Ok(res);
        }
        [HttpGet("GetAllProposals")]
        public async Task<IActionResult> GetAllProposals()
        {
            var res = await _projectService.GetAllProposals();
            return Ok(res);
        }
    }
}
