using DISERTATIE.FLACARA.CAPTURII.DTO.DomainsDTO;
using DISERTATIE.FLACARA.CAPTURII.DTO.EntityDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.SERVICES.Contracts;
public interface IFolderService : IBaseService<FolderDTO>
{
    Task<List<FolderWithPhotosDTO>> GetMyFoldersWithPhotos(int userId);
}