﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.Models.Request;
using Assignment1.Models.Response;

namespace Assignment1.Service
{
    public interface IReviewService
    {
        IList<ReviewResponse> Get();
        ReviewResponse Get(int id);
        void Create(ReviewRequest reviewRequest);
        void Update(int id,int movieId, ReviewRequest reviewRequest);
        void Delete(int id);
    }
}
