using Microsoft.AspNetCore.Mvc;


namespace League_of_Statistics.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiotAPIController : ControllerBase
    {
        // GET: api/<RiotAPIController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RiotAPIController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RiotAPIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RiotAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RiotAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
