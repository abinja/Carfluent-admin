using BusinessLogicLayer;
using BusinessLogicLayer.Interface;
using DataAccessLayer;
using GlobalEntityLayer.DTO;
using GlobalEntityLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;
using System.Web.Http.Controllers;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]



    public class CarController : ControllerBase
    {
        private readonly ICars _cars;
        public CarController(ICars cars)
        {
            _cars = cars;
        }
        // GET: api/<CarController>

        [HttpGet]
        [Route("Get")]
        [AllowAnonymous]
        public ActionResult<List<CarDetails>> Get()
        {
            return _cars.get();
        }


        [HttpGet]
        [Route("get/{CarID}")]
        [AllowAnonymous]
        public ActionResult<CarDetails> Get(string CarID)
        {
            return _cars.Get(CarID);
        }


        [HttpPost]
        [Route("Add")]

        public IActionResult post([FromBody] CarDetailsPhotoDTO carDetails)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not valid");
            _cars.post(carDetails);
            return Ok();

        }


        [HttpDelete]
        [Route("Delete/{CarID}")]
        public ActionResult Delete(string CarID)
        {
            _cars.Delete(CarID);


            return Ok();
        }


        [HttpPost]
        [Route("Update")]
        public IActionResult Update(int CarID, CarDetailsDTO carDetailsDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not valid");
            return Ok(_cars.Update(CarID, carDetailsDTO));

        }


        //public ActionResult<bool> IsRegNumAvailable(string regnumber)
        //{
        //    return _cars.IsRegNumAvailablevalidation(regnumber);
        //}

        //public CarController(CarDetailsDBcontext dbcontext)
        //{
        //    _dbContext = dbcontext;
        //}

        //public bool IsRegNumAvailble(CarDetails) 
        //{
        //    var temp = _dbContext.Carfluent_Cars.FirstOrDefault(i => i.CarDetails.RegistrationNumber == carDetails.registrationNumber);
        //    if (temp == null)
        //    {
        //        return false;

        //    }
        //    return true;

        //}
        [HttpPost]
        [AllowAnonymous]
        [Route("Add Image")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CarDetailsDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Create brand", Description = "Add Brand name and brand logo path")]

        public async Task<CarDetailsDTO> PhotoPost([FromForm] int CarID, IFormFile photo)
        {
            CarDetailsDTO cars = new CarDetailsDTO();
            //cars.CategoryId = CarID;
            cars.Photo = photo;
            //_cars.post(cars);
            if (!ModelState.IsValid)
                return (cars);
            var n=_cars.PhotoPost(cars,CarID);
            return cars;
        }
    }
}

    

