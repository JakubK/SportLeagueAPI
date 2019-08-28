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

namespace SportLeagueAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        private readonly LeagueDbContext _dbContext;
        private readonly MappedSchemaProvider<LeagueDbContext> _schemaProvider;

        public GraphQLController(LeagueDbContext dbContext, MappedSchemaProvider<LeagueDbContext> schemaProvider)
        {
            this._dbContext = dbContext;
            this._schemaProvider = schemaProvider;
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
                var data = _dbContext.QueryObject(query, _schemaProvider);
                return data;
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}