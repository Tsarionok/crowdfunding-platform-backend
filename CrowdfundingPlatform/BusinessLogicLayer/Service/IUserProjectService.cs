﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTO;

namespace BusinessLogicLayer.Service
{
    public interface IUserProjectService : ICrudService<UserProjectDTO, int>
    {
        public Task Estimate(UserProjectDTO evaluation);

        public Task AddToFavourites(UserProjectDTO favourite);
    }
}
