using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using EntityGraphQL;
using EntityGraphQL.Schema;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SportLeagueAPI.Context;
using SportLeagueAPI.Models;
using SportLeagueAPI.Services;

namespace SportLeagueAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        private readonly LeagueDbContext _dbContext;
        private readonly MappedSchemaProvider<LeagueDbContext> _schemaProvider;

        private IPathsProvider _authService;

        public GraphQLController(LeagueDbContext dbContext, MappedSchemaProvider<LeagueDbContext> schemaProvider, IPathsProvider authService)
        {
            this._dbContext = dbContext;
            this._schemaProvider = schemaProvider;
            
            this._authService = authService;
        }

        [HttpPost]
        public IActionResult Post([FromBody]QueryRequest query)
        {
            return Ok(RunDataQuery(query));
        }

        private object RunDataQuery(QueryRequest query)
        {
            try
            {
                var data = _dbContext.QueryObject(query, _schemaProvider, _authService);
                return data;
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}