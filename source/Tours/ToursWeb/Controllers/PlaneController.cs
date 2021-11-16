using System;
using System.Collections.Generic;
using ToursWeb.ModelsDB;
using ToursWeb.Repositories;

namespace ToursWeb.Controllers
{
    public class PlaneController
    {
        private readonly IPlaneRepository _planeRepository;

        public PlaneController(IPlaneRepository planeRepository)
        {
            _planeRepository = planeRepository;
        }

        public List<Planeticket> GetAllPlanes()
        {
            return _planeRepository.FindAll();
        }

        public Planeticket GetPlaneByID(int planeID)
        {
            return _planeRepository.FindByID(planeID);
        }

        public void AddPlane(Planeticket nplane)
        {
            _planeRepository.Add(nplane);
        }

        public void UpdatePlane(Planeticket nplane)
        {
            _planeRepository.Update(nplane);
        }

        public void DeletePlaneByID(int planeID)
        {
            _planeRepository.DeleteByID(planeID);
        }
    }
}