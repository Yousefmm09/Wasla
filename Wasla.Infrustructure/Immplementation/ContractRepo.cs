using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasla.Data.Entite;
using Wasla.Infrustructure.Context;
using Wasla.Service.Abstract.Repositories;
using Wasla.Service.DTOs.Contracts;

namespace Wasla.Service.Immplementation
{
    public class ContractRepo : IContractRepo
    {
        private readonly AppDb _appDb;
        public ContractRepo(AppDb app)
        {
            _appDb = app; 
        }
        public async Task<ContractResponse> CreatContract(int id)
        {
            var contract = await _appDb.Contracts.Where(x => x.Id == id).Select(x => new ContractResponse
            {
                ClientName = x.Client.UserName,
                FreelancerName = x.Freelancer.UserName,
                Status = x.Status,
                CompletedAt = x.CompletedAt,
                CreatedAt = x.CreatedAt,
                AgreedBudget = x.AgreedBudget,
                DeliveredAt = x.DeliveredAt,
                ProjectTitle = x.Project.Title
            }).FirstOrDefaultAsync();
            return contract;
        }
        public async Task<string> UploadProject(int id)
        {
            var project = await _appDb.Projects.Include(x=>x.Contract).Where(x => x.Id == id).FirstOrDefaultAsync();
            if (project == null)
                return "not found project";
            if (project.Contract.Status == 0)
            {
                project.Status = ProjectStatus.Completed;
                await _appDb.SaveChangesAsync();
            }
            return "the project has been upload and delivered to client";
        }
        public async Task<string> ApproveContract(int id)
        {
            var contract = await _appDb.Contracts.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (contract == null)
                return "not found project";
            contract.Status=ContractStatus.Approved;
            return "the Contract has been Approved , you need to send a money to freelancer";
        }

    }
}
