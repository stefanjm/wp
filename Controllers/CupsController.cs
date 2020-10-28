using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WoodPeckerAngular.Core.Interfaces;
using WoodPeckerAngular.Core.Models;
using WoodPeckerAngular.Data;

namespace WoodPeckerAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CupsController : ControllerBase
    {
        private ICupCalcService _cupCalcService;
        private ICupRepository _cupRepository;


        public CupsController(WoodPeckerAngularContext context, ICupCalcService cupCalcService, ICupRepository cupRepository)
        {
            _cupCalcService = cupCalcService;
            _cupRepository = cupRepository;
        }

        // GET: api/Cups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CupModel>>> GetCupModel()
        {
            var cups = await _cupRepository.GetAll();
            cups = cups.OrderBy(o => o.Id);
            return cups.ToList();
        }

        // GET: api/Cups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CupModel>> GetCupModel(int id)
        {
            var cupModel = await _cupRepository.GetById(id);

            if (cupModel == null)
            {
                return NotFound();
            }

            return cupModel;
        }

        // POST: api/Cups
        [HttpPost]
        public async Task<ActionResult<CupModel>> PostCupModel(CupModel cupModel)
        {
            await _cupRepository.Insert(cupModel);

            return CreatedAtAction("GetCupModel", new { id = cupModel.Id }, cupModel);
        }

        // DELETE: api/Cups/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CupModel>> DeleteCupModel(int id)
        {
            var cupModel = await _cupRepository.GetById(id);
            if (cupModel == null)
            {
                return NotFound();
            }

            await _cupRepository.Delete(cupModel);

            return cupModel;
        }

        /// <summary>
        /// Get all the cups in DB and fit/sort them for minimum volume
        /// </summary>
        /// <returns></returns>
        [HttpGet("sorted")]
        public async Task<ActionResult<IList<SortedCupsModel>>> GetSortedCups()
        {
            var sortedCups = await _cupCalcService.GetSortedCups();
            return sortedCups.ToList();
        }
    }
}
