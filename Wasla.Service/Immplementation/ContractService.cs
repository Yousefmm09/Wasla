using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasla.Service.Abstract.Repositories;
using Wasla.Service.Abstract.Services;
using Wasla.Service.DTOs.Contracts;

namespace Wasla.Service.Immplementation
{
    public class ContractService : IContractService
    {
        private readonly IContractRepo _contract;
        public ContractService(IContractRepo contract)
        {
            _contract = contract;
        }
        public async Task<ContractResponse> CreatContract(int id)
        {
            return  await _contract.CreatContract(id);
        }

        public async Task<string> UploadProject(int id)
        {
            return await _contract.UploadProject(id);
        }
        public async Task<string> ApproveContract(int id)
        {
            return await _contract.ApproveContract(id);
        }
    }
}
