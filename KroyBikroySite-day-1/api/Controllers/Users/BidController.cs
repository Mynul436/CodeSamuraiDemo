using api.Dto;
using api.Extensions;
using AutoMapper;
using core.Entities;
using core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BidController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BidController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

    
    }
}