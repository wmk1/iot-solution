using iot_backend.Repository.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace iot_backend.Repository
{
    public interface IRepository
    {

        EntityEntry<IoTDataModel> Execute(IoTDataModel tempDataModel, RepositoryAction action);

    }
}