using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTO;
using DataAccessLayer.Context;
using DataAccessLayer.Entity;

namespace BusinessLogicLayer.Service.Implementation
{
    public class PhotoService : BaseService, IPhotoService
    {
        public PhotoService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task Create(PhotoDTO photo)
        {
            await _unitOfWork.Photos.Create(new Photo
            {
                Image = photo.Image,
                Project = _unitOfWork.Projects.ReadById(photo.Project.Id).Result
            });
        }

        public async Task<PhotoDTO> DeleteById(int id)
        {
            Photo deletedPhoto = _unitOfWork.Photos.DeleteById(id).Result;
            PhotoDTO photo = new PhotoDTO
            {
                Id = deletedPhoto.Id,
                Image = deletedPhoto.Image,
                Project = new ProjectService(_unitOfWork).ReadById(deletedPhoto.Project.Id).Result
            };

            return await Task.Run(() => photo);
        }

        public async Task<ICollection<PhotoDTO>> ReadAll()
        {
            ICollection<PhotoDTO> photos = new List<PhotoDTO>();

            foreach (Photo readablePhoto in _unitOfWork.Photos.ReadAll().Result)
            {
                photos.Add(new PhotoDTO {
                    Id = readablePhoto.Id,
                    Image = readablePhoto.Image,
                    Project = new ProjectService(_unitOfWork).ReadById(readablePhoto.Project.Id).Result
                });
            }
            return await Task.Run(() => photos);
        }

        public async Task<PhotoDTO> ReadById(int id)
        {
            Photo readablePhoto = _unitOfWork.Photos.ReadById(id).Result;

            return await Task.Run(() => new PhotoDTO
            {
                Id = readablePhoto.Id,
                Image = readablePhoto.Image,
                Project = new ProjectService(_unitOfWork).ReadById(readablePhoto.Project.Id).Result
            });
        }

        public async Task Update(PhotoDTO photo)
        {
            await _unitOfWork.Photos.Update(new Photo
            {
                Id = photo.Id,
                Image = photo.Image,
                Project = _unitOfWork.Projects.ReadById(photo.Project.Id).Result
            });
        }
    }
}
