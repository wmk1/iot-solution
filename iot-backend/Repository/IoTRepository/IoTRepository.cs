using System;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using iot_backend.Repository.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace iot_backend.Repository.IoTRepository
{
    public class IoTRepository : IRepository
    {
        
        private Repository _repository;

        public IoTRepository()
        {
            this._repository = new Repository();
        }
        /// <summary>
        /// Main repository command execution
        /// </summary>
        /// <param name="tempDataModel">Date model of the repository</param>
        /// <param name="action">repository action to preform</param>
        /// <returns>Updated date model of the result of the repository action</returns>
        public EntityEntry<IoTDataModel> Execute(IoTDataModel ioTDataModel, RepositoryAction action)
        {

            if (action != null || ioTDataModel != null)
            {
                return this.DelegateCommandHandler(ioTDataModel, action);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Delegation handler 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="repositoryAction"></param>
        /// <returns></returns>
        private EntityEntry<IoTDataModel> DelegateCommandHandler(IoTDataModel model, RepositoryAction repositoryAction)
        {

            IRepository repository = null;
            switch (repositoryAction)
            {
                case RepositoryAction.Get:
                    return this.GetEntry(model);
                    break;
                
                case RepositoryAction.Put:
                    return this.AddEntry(model);
                    break;
                
                case RepositoryAction.Update:
                    return this.UpdateEntry(model);
                    break;
                
                case RepositoryAction.Remove:
                    return this.RemoveEntry(model);
                    break;
                
                default:

                    break;
            }


            return null;
        }

        private EntityEntry<IoTDataModel> RemoveEntry(IoTDataModel model)
        {
            return this._repository.Remove(model);
        }

        private EntityEntry<IoTDataModel> UpdateEntry(IoTDataModel model)
        {
            return this._repository.Update(model);
        }

        private EntityEntry<IoTDataModel> AddEntry(IoTDataModel model)
        {
            return this._repository.Add(model);
        }

        private EntityEntry<IoTDataModel> GetEntry(IoTDataModel model)
        {
            return null;
            //return this._repository.TempDataModels.Find(model.DeviceId);
        }
    }
}