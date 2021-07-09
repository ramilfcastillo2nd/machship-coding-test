using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
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
        public async Task<IActionResult> RetrieveUsers([FromQuery]string[] usernames)
        {
            try
            {
                var lstUserInfo = new List<GithubUserInfo>();

                //Removed duplicate entry of usernames before calling github 
                foreach (var username in usernames.Distinct())
                {
                    var getUserInfoResponse = await _gitHubService.GetUserInfo(username);
                    if(getUserInfoResponse!=null)
                    {
                        lstUserInfo.Add(getUserInfoResponse);
                    }                 
                }

                var usersMapped = _mapper.Map<List<GithubUserInfo>, List<RetrieveUsersResponse>>(lstUserInfo);

                //Add average number of followers and Sort
                usersMapped = usersMapped.Select(s =>
                {
                    s.AverageNoOfFollowersPerRepo = s.NoOfFollowers / s.NoOfPublicRepo;
                    return s;
                }).OrderBy(s => s.name).ToList();

                return Ok(usersMapped);
            }
            catch (Exception x)
            {   
                return BadRequest(x.Message);
            }
        }
    }
}
