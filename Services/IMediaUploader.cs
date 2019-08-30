using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SportLeagueAPI.Services
{
    public interface IMediaUploader
    {
         string UploadMedia(IFormFile file);
         Task<string> UploadMediaAsync(IFormFile file);
    }
}