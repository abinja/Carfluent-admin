using AutoMapper;
using BusinessLogicLayer.Interface;
using DataAccessLayer;
using GlobalEntityLayer.DTO;
using GlobalEntityLayer.Models;
using Microsoft.AspNetCore.Hosting;


using Microsoft.AspNetCore.Http;
using System.Web.Http;

namespace BusinessLogicLayer
{
    public class Cars : ICars
    {
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;
        readonly CarDetailsDBcontext dbcontext;
        public Cars(CarDetailsDBcontext carDetailsDBcontext, IWebHostEnvironment hostEnvironment,IMapper mapper)
        {
            dbcontext = carDetailsDBcontext;
            _mapper= mapper;
            this._hostEnvironment = hostEnvironment;
        }

        //public Cars(CarDetailsDBcontext carDetailsDBcontext)
        //{
        //    dbcontext = carDetailsDBcontext;
        //}

        public void Delete(string carID)
        {
            CarDetails carDelete = dbcontext.Carfluent_Cars.FirstOrDefault(i => i.CarID.ToString() == carID);
            if (carDelete != null)
            {
                dbcontext.Remove(carDelete);
                dbcontext.SaveChanges();
            }
        }
        //public ApiResponse<bool> Delete(int carID)
        //{
        //    CarDetails car = dbcontext.Carfluent_Cars.FirstOrDefault(i => i.CarID == carID);
        //    ApiResponse<bool> response = new ApiResponse<bool>();
        //    if (car != null)
        //    {
        //        if (car.Status == 0)
        //        {
        //            car.Status = 1;
        //            dbcontext.Carfluent_Cars.Update(car);
        //            dbcontext.SaveChanges();
        //            response.Success = true;
        //            response.Message = "Deleted";
        //            return response;
        //        }
        //        else
        //        {
        //            response.Success = false;
        //            response.Message = "Already Deleted";
        //            return response;
        //        }
        //    }

        //    response.Success = false;
        //    response.Message = "ID doesn't exist.";
        //    return response;
        //}

        public ApiResponse<bool> Update(int id, [FromBody] CarDetailsDTO carDetailsDTO)
        {
           
            var update = dbcontext.Carfluent_Cars.FirstOrDefault(e => e.CarID == id);
            var category = dbcontext.CategoryPicklist.FirstOrDefault(x => x.ID == carDetailsDTO.CategoryId);
            ApiResponse<bool> updateResponse = new ApiResponse<bool>();
            if (update == null)
            {
                updateResponse.Success = false;
                updateResponse.Message = "Car ID doesnt exist";
                return updateResponse;
            }
            else
            {
                updateResponse.Success = true;
                updateResponse.Message = "Car details updated";
                update.Make = carDetailsDTO.Make;
                update.Colour= carDetailsDTO.Colour;
                update.Availabilitystatus = carDetailsDTO.Availabilitystatus;
                update.Hub= carDetailsDTO.Hub;  
                update.Model=carDetailsDTO.Model;   
                update.Kilometers=carDetailsDTO.Kilometers; 
                update.CarType=carDetailsDTO.CarType;
                update.Price=carDetailsDTO.Price;   
                update.TransmissionType=carDetailsDTO.TransmissionType; 
                update.NumberOfSeats=carDetailsDTO.NumberOfSeats;
                update.CategoryPicklistID = category.ID;


                dbcontext.Update(update);
                dbcontext.SaveChanges();
                return updateResponse;
            }

        }
        public async Task<ApiResponse<bool>> Post(CarDetailsDTO carname)
        {
            var cars = _mapper.Map<CarDetailsDTO, CarDetails>(carname);
            ApiResponse<bool> addResponse = new ApiResponse<bool>();
            if (carname != null)
            {
                carname.CarPhoto = await SavePhoto(carname.Photo);
                var carDetails = new CarDetails()
                {
                    CarPhoto = carname.CarPhoto,
                    CarType = carname.CarType,
                    
                };
                dbcontext.Carfluent_Cars.Add(cars);
                dbcontext.SaveChanges();
                addResponse.Success = true;
                addResponse.Message = "New photo added";
                return addResponse;
            }
            else
            {
                addResponse.Success = false;
                addResponse.Message = "Not created";
                return addResponse;
            }

            }
            public async Task<string> SavePhoto(IFormFile logo)
        {

            string logoPath = new String(Path.GetFileNameWithoutExtension(logo.FileName).Take(10).ToArray()).Replace(' ', '-');
            logoPath = logoPath + DateTime.Now.ToString("yyyyMMddhhmmfff") + Path.GetExtension(logo.FileName);
            var path = Path.Combine(_hostEnvironment.ContentRootPath, "Images", logoPath);
            using (var filestream = new FileStream(path, FileMode.Create))
            {
                await logo.CopyToAsync(filestream);
            }
            return logoPath;
        }

        //public void Edit(CarDetails editDetails)
        //{
        //    CarDetails car = dbcontext.Carfluent_Cars.FirstOrDefault(i => i.CarID == editDetails.CarID);
        //    if (car != null)
        //    {
        //        car.Colour = editDetails.Colour;
        //        car.NumberOfSeats = editDetails.NumberOfSeats;
        //        car.RegistrationNumber = editDetails.RegistrationNumber;
        //        car.CategoryPicklist = editDetails.CategoryPicklist;
        //        car.CarType = editDetails.CarType;
        //        car.FuelType = editDetails.FuelType;
        //        car.Make = editDetails.Make;
        //        car.TransmissionType=editDetails.TransmissionType;
        //        car.Model = editDetails.Model;
        //        car.Price = editDetails.Price;
        //        car.Hub=editDetails.Hub;
        //        car.Availabilitystatus=editDetails.Availabilitystatus;  

        //        car.CategoryPicklistID = editDetails.CategoryPicklistID;
        //        dbcontext.Carfluent_Cars.Update(car);
        //        //dbcontext.Carfluent_Cars.Remove(car);
        //        //dbcontext.Carfluent_Cars.Add(editDetails);
        //        dbcontext.SaveChanges();
        //    }
        //}

        //public void Edit(CarDetails carID)
        //{
        //    CarDetails car = dbcontext.Carfluent_Cars.FirstOrDefault(i => i.CarID == carID.CarID);
        //    if (car != null)
        //    {
        //        dbcontext.Carfluent_Cars.Remove(car);
        //        dbcontext.Carfluent_Cars.Add(carID);
        //        dbcontext.SaveChanges();
        //    }
        //}


        public List<CarDetails> get()
        {
            return dbcontext.Carfluent_Cars.ToList();
        }

        public CarDetails Get(string carID)
        {
            
             return dbcontext.Carfluent_Cars.FirstOrDefault(i => i.CarID.ToString() == carID);
            
        }

        public async void post(CarDetailsPhotoDTO carDetails)
        {
            //carDetails.CarPhoto = await SavePhoto(carDetails.Photo);

            var vehicles = dbcontext.CategoryPicklist.Where(x => x.ID == carDetails.CategoryPicklistID).ToList();

            var dto = new CarDetails();
            dto.RegistrationNumber = carDetails.RegistrationNumber;
            dto.Make=carDetails.Make;    
            dto.Model=carDetails.Model;  
            dto.Colour=carDetails.Colour;
            dto.NumberOfSeats=carDetails.NumberOfSeats;
            dto.FuelType=carDetails.FuelType;   
            dto.TransmissionType=carDetails.TransmissionType;   
            dto.CarType=carDetails.CarType; 
            dto.Price=carDetails.Price; 
            dto.Availabilitystatus=carDetails.Availabilitystatus;   
            dto.Hub=carDetails.Hub; 
            dto.CategoryPicklist = null;
            dto.CategoryPicklistID = vehicles[0].ID;
            dbcontext.Carfluent_Cars.Add(dto);
            dbcontext.SaveChanges();
        }

        public bool IsRegNumAvailablevalidation(string username)
        {
            var temp = dbcontext.Carfluent_Cars.FirstOrDefault(i => i.CarID.ToString() == username);
            if(temp==null)
                return false;
            return true;
        }
        public string SaveLogo(IFormFile logo)
        {

            string logoPath = new String(Path.GetFileNameWithoutExtension(logo.FileName).Take(10).ToArray()).Replace(' ', '-');
            logoPath = logoPath + DateTime.Now.ToString("yyyyMMddhhmmfff") + Path.GetExtension(logo.FileName);
            var path = Path.Combine(_hostEnvironment.ContentRootPath, "Images", logoPath);

            using (var filestream = new FileStream(path, FileMode.Create))
            {
                 logo.CopyToAsync(filestream);
            }
            return logoPath;
        }
        public ApiResponse<bool> PhotoPost(CarDetailsDTO carDetails, int carID)
        {

            try
            {
                var brand1 = _mapper.Map<CarDetailsDTO, CarDetails>(carDetails);
                //brand1.CarID = carID;
                ApiResponse<bool> addResponse = new ApiResponse<bool>();
                var brand = dbcontext.Carfluent_Cars.FirstOrDefault(x => x.CarID == carID);
                if (brand != null)
                {
                    string carPhoto = SaveLogo(brand1.Photo);
                    brand.CarPhoto = carPhoto;

                    //dbcontext.Update(brand);
                    dbcontext.SaveChanges();
                    addResponse.Success = true;
                    addResponse.Message = "New Photo Added";
                    addResponse.Data = true;
                    return addResponse;
                }
                else
                {
                    addResponse.Success = false;
                    addResponse.Message = "Not created";
                    addResponse.Data = false;
                    return addResponse;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        //public void PhotoPost(CarDetails photo)
        //{
        //    throw new NotImplementedException();
        //}





        //public void Edit(CarDetails editDetails)
        //{
        //    CarDetails car = CarDetailsDBcontext.Carfluent_Cars.FirstOrDefault(i => i.Username == editDetails.Username);
        //    if (employ != null)
        //    {
        //        CarDetailsDBcontext.Carfluent_Cars.Remove(car);
        //        CarDetailsDBcontext.Carfluent_Cars.Add(editDetails);
        //        CarDetailsDBcontext.SaveChanges();
        //    }

        //}

        //public List<CarDetails> get()
        //{
        //    return CarDetailsDBcontext.Carfluent_Cars.ToList();
        //}
        //public CarDetails Get(string username)


        //public void post(CarDetails carDetails)
        //{
        //    dbcontext.Carfluent_Cars.Add(carDetails);
        //    dbcontext.SaveChanges();
    }
}
