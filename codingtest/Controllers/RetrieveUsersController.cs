using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace codingtest.Controllers
{
    [Route("retrieveUsers")]
    [ApiController]
    public class RetrieveUsersController : ControllerBase
    {
        private readonly IGitHubService _gitHubService;
        private readonly IMapper _mapper;
        public RetrieveUsersController(IGitHubService gitHubService, IMapper mapper)
        {
            _gitHubService = gitHubService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieve Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> RetrieveUsers()
        {
            try
            {
                var getUsersResponse = await _gitHubService.GetUsers();
                var getUsersInfoResponse = await _gitHubService.GetUsersInfo(getUsersResponse);
                var usersMapped = _mapper.Map<List<GithubUserInfo>, List<RetrieveUsersResponse>>(getUsersInfoResponse);
                return Ok(usersMapped);
            }
            catch (Exception x)
            {
                return BadRequest(x.Message);
            }
        }
    }
}
