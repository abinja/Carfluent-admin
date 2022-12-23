﻿using GlobalEntityLayer.DTO;
using GlobalEntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interface
{
    public interface ICars
    {
        List<CarDetails> get();
        CarDetails Get(string username);
        void post(CarDetailsPhotoDTO carDetails);
        public  ApiResponse<bool> PhotoPost(CarDetailsDTO photo,int carID);
        public ApiResponse<bool> Update(int CarID, CarDetailsDTO carDetailsDTO);
        //void Edit(CarDetails editDetails);
        void Delete(string username);
        //public ApiResponse<bool> Delete(int CarID);
        //bool IsRegNumAvailablevalidation(string username);

    }


}

