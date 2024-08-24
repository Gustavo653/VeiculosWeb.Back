using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;
using VeiculosWeb.Domain.Vehicles;
using VeiculosWeb.Domain.VehicleSpecification;
using VeiculosWeb.DTO;
using VeiculosWeb.DTO.Base;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Infrastructure.Service;
using VeiculosWeb.Utils;

namespace VeiculosWeb.Service
{
    public class CarService(
        ICarRepository carRepository,
        IFuelRepository fuelRepository,
        IBrandRepository brandRepository,
        IModelRepository modelRepository,
        IFeatureRepository featureRepository,
        IGearboxRepository gearboxRepository,
        IColorRepository colorRepository,
        IStateRepository stateRepository,
        ICityRepository cityRepository,
        IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor) : ICarService
    {
        private ISession Session => httpContextAccessor.HttpContext!.Session;

        public async Task<ResponseDTO> Create(BaseVehicleDTO carDTO)
        {
            ResponseDTO responseDTO = new();
            try
            {
                var state = await stateRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == carDTO.StateId);
                var city = await cityRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == carDTO.CityId);
                var color = await colorRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == carDTO.ColorId);
                var gearbox = await gearboxRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == carDTO.GearboxId);
                var fuel = await fuelRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == carDTO.FuelId);
                var brand = await brandRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == carDTO.BrandId);
                var model = await modelRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == carDTO.ModelId);
                var features = await featureRepository.GetTrackedEntities().Where(x => carDTO.Features.Contains(x.Id)).ToListAsync();
                var user = await userRepository.GetTrackedEntities().FirstOrDefaultAsync(x => x.Id == Session.GetString(Consts.ClaimUserId));

                if(user == null || brand == null || model == null)
                {
                    responseDTO.SetBadInput($"Verifique as informações inseridas.");
                    return responseDTO;
                }

                //FeatureXBaseVehicle featureXBaseVehicle = features.Select(x => new FeatureXBaseVehicle());

                var car = new Car()
                {
                    Brand = brand,
                    Model = model,
                    Odometer = carDTO.Odometer,
                    YearOfManufacture = carDTO.YearOfManufacture,
                    YearOfModel = carDTO.YearOfModel,
                    Fuel = fuel,
                    Gearbox = gearbox,
                    Color = color,
                    State = state,
                    City = city,
                    User = user
                };

                await carRepository.InsertAsync(car);

                await carRepository.SaveChangesAsync();
                Log.Information("Carro persistido id: {id}", car.Id);

                responseDTO.Object = car;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> Update(Guid id, BaseVehicleDTO carDTO)
        {
            throw new NotImplementedException();
            ResponseDTO responseDTO = new();
            try
            {
                var car = await carRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (car == null)
                {
                    responseDTO.SetBadInput($"O carro {id} não existe!");
                    return responseDTO;
                }

                car.SetUpdatedAt();

                await carRepository.SaveChangesAsync();
                Log.Information("Carro persistido id: {id}", car.Id);

                responseDTO.Object = car;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> Remove(Guid id)
        {
            throw new NotImplementedException();
            ResponseDTO responseDTO = new();
            try
            {
                var car = await carRepository.GetTrackedEntities().FirstOrDefaultAsync(c => c.Id == id);
                if (car == null)
                {
                    responseDTO.SetBadInput($"O carro com id: {id} não existe!");
                    return responseDTO;
                }
                carRepository.Delete(car);
                await carRepository.SaveChangesAsync();
                Log.Information("Carro removido id: {id}", car.Id);

                responseDTO.Object = car;
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> GetList()
        {
            ResponseDTO responseDTO = new();
            try
            {
                responseDTO.Object = await carRepository.GetEntities().ToListAsync();
            }
            catch (Exception ex)
            {
                responseDTO.SetError(ex);
            }
            return responseDTO;
        }
    }
}
