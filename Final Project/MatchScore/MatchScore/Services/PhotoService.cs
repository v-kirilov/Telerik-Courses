using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MatchScore.Data;
using MatchScore.Helpers;
using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Services
{
    public class PhotoService : IPhotoService
    {
        private const string PlayerDefaultPhoto = "https://res.cloudinary.com/diukesejh/image/upload/v1670010620/football-player-default_e6a4wy.png";

        private readonly IOptions<CloudinaryConfig> cloudinaryConfig;
        private Cloudinary cloudinary;
        private readonly ApplicationContext context;

        public PhotoService(IOptions<CloudinaryConfig> cloudinaryConfig,
            ApplicationContext context)
        {
            this.context = context;
            this.cloudinaryConfig = cloudinaryConfig;

            Account account = new Account(
                this.cloudinaryConfig.Value.CloudName,
                this.cloudinaryConfig.Value.ApiKey,
                this.cloudinaryConfig.Value.ApiSecret
            );

            cloudinary = new Cloudinary(account);
        }
        public async Task<PhotoForReturn> AddPhotoForPlayer(int playerId, PhotoForCreation photoDto)
        {
            var player = await context                                  
                        .Players
                        .Where(p => p.Id == playerId)
                        .FirstOrDefaultAsync();

            var file = photoDto.File;                                  

            var uploadResult = new ImageUploadResult();                 

            if (file.Length > 0)                                       
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation()           
                                        .Width(500).Height(500)
                                        .Crop("fill")
                                        .Gravity("face")
                    };

                    uploadResult = cloudinary.Upload(uploadParams);    
                }
            }

            photoDto.Url = uploadResult.Url.ToString();                 
            photoDto.PublicId = uploadResult.PublicId;                  

            var photo = new Photo                                       
            {
                Url = photoDto.Url,
                Description = "",
                DateAdded = photoDto.DateAdded,
                PublicId = photoDto.PublicId,
                Player = player
            };


            //if (!photo.Player.Photos.Any(m => m.IsMain))
            //     photo.IsMain = true;


            photo.IsMain = true;

            player.Photos.Add(photo);
            await SaveAll();                                            

            return new PhotoForReturn                                   
            {
                Id = photo.Id,
                Url = photo.Url,
                Description = photo.Description,
                DateAdded = photo.DateAdded,
                IsMain = photo.IsMain,
                PublicId = photo.PublicId,
            };
        }

        public string GetPhotoUrl(int id)
        {
            var photo = this.context.Photos.OrderBy(p=>p.Id).LastOrDefault(p => p.PlayerId== id && p.IsMain == true);

            //var photoForReturn = new PhotoForReturn
            //{
            //    Id = photo.Id,
            //    Url = photo.Url,
            //    Description = photo.Description,
            //    DateAdded = photo.DateAdded,
            //    IsMain = photo.IsMain,
            //    PublicId = photo.PublicId,
            //};
            var photoUrl = string.Empty;

            if (photo != null)
            {
                photoUrl = photo.Url;
            }
            else
            {
                photoUrl = PlayerDefaultPhoto;
            }

            return photoUrl;
        }

        public async Task SaveAll()
        {
            await context.SaveChangesAsync();
        }
    }
}
