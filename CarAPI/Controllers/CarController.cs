using CarAPI.Repositories;
using CarAPI.Services;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace CarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IMongoRepository<Car> _carRepository;
        private readonly ISyncService<Car> _carSyncService;

        public CarController(IMongoRepository<Car> carRepository, ISyncService<Car> carSyncService)
        {
            _carSyncService = carSyncService;
            _carRepository = carRepository;
        }
        [HttpGet]
        public List<Car> GetAllCars()
        {
            var records = _carRepository.GetAllRecords();
            return records;

        }
        [HttpGet("{id}")]
        public Car GetCarById(Guid id)
        {
            var result = _carRepository.GetRecordById(id);

            return result;
        }


        [HttpPost]
        public IActionResult Create(Car car)
        {
            car.LastChangedAt = DateTime.UtcNow;
           var result = _carRepository.InsertRecord(car);

            _carSyncService.Upsert(car);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Upsert(Car car)
        {
            if (car.Id == Guid.Empty)
                return BadRequest("Vi Kto Takie? Ia vas ne zval! Idite v Politeh");
            
            car.LastChangedAt = DateTime.UtcNow;
            _carRepository.UpsertRecord(car);

            _carSyncService.Upsert(car);
            return Ok(car);
        }

        [HttpPut("sync")]

        public IActionResult UpsertSync(Car car)
        {
            var existingCar = _carRepository.GetRecordById(car.Id);
            if (existingCar == null || car.LastChangedAt > existingCar.LastChangedAt)
            {
                _carRepository.UpsertRecord(car);

            }
            return Ok();
        }

        [HttpDelete("sync")]

        public IActionResult DeleteSync(Car car)
        {
            var existingCar = _carRepository.GetRecordById(car.Id);
            if (existingCar != null || car.LastChangedAt > existingCar.LastChangedAt)
            {
                _carRepository.DeleteRecord(car.Id);

            }
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var car = _carRepository.GetRecordById(id);
            if (car == null)
            {
                return BadRequest("Mashina not found");
            }
            _carRepository.DeleteRecord(id);

            car.LastChangedAt = DateTime.UtcNow;
            _carSyncService.Delete(car);

                return Ok("Deleted: " + id);
        }
    }
}