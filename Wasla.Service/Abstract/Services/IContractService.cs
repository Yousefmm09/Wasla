using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasla.Service.DTOs.Contracts;

namespace Wasla.Service.Abstract.Services
{
    public interface IContractService
    {
        Task<ContractResponse> CreatContract(int id);
        Task<string> UploadProject(int id);
         Task<string> ApproveContract(int id);

    }
}
