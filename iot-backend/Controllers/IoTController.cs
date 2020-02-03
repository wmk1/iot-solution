using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iot_backend.Repository;
using iot_backend.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using iot_backend.Repository.IoTRepository;

namespace iot_backend.Controllers
{
    [ApiController]
    public class IoTController : ControllerBase
    {
        private IRepository _repository;    

        private readonly ILogger<LoggerDataModel> _logger;

        public IoTController() { this._repository = new IoTRepository(); }

        [HttpGet]
        [Route("Home/getdevice")]
        public IoTDataModel GetDevice()
        {

            return this._repository.Execute(new IoTDataModel(), RepositoryAction.Get).Entity;
        }  

        [HttpPost]
        [Route("Home/pushdata")]
        public void PushData(IoTDataModel dataModel)    
        {

            this._repository.Execute(dataModel, RepositoryAction.Put);
        }

        [HttpDelete]
        [Route("Home/deletedata")]
        public void DeleteData(IoTDataModel dataModel)
        {

            this._repository.Execute(dataModel, RepositoryAction.Remove);
        }
    }
}