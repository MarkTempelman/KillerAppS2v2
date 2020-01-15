using System;
using System.Collections.Generic;
using System.Text;
using Data.DTO;

namespace Data.Interfaces
{
    public interface IRatingContext
    {
        List<RatingDTO> GetAllRatingsFromMediaId(int id);
    }
}
